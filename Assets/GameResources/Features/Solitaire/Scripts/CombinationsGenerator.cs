namespace Anasty.Solitaire.Core
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Генератор случайных комбинаций для поля карт
    /// </summary>
    public class CombinationsGenerator : MonoBehaviour
    {
        [SerializeField]
        public List<Combination> combinations = new List<Combination>();

        protected int maxCombinationsCount = 7;
        protected int minCombinationsCount = 2;

        protected float chanceToUpCombination = 0.65f;

        protected float chanceToChangeDirection = 0.15f;

        int lastCost = 0;

        protected int freeCards = 32;

        private void Start()
        {
            // GenerateCombination();
        }


        public void GenerateCombinations()
        {
            while (freeCards > 0)
            {
                Combination newCombination = new Combination();
                newCombination.isUpCombination = Random.value;
                newCombination.needChangeDirection = Random.value;
                newCombination.combinationLenght = Random.Range(minCombinationsCount, maxCombinationsCount + 1);
                bool needChangeDirection = newCombination.needChangeDirection < chanceToChangeDirection;
                if (needChangeDirection)
                {
                    newCombination.changeDirectionIndex = Random.Range(0, newCombination.combinationLenght);
                }

                bool isUpCombination = newCombination.isUpCombination < chanceToUpCombination;



                int combinationLenght = newCombination.combinationLenght;

                if (freeCards < maxCombinationsCount)
                {
                    combinationLenght = freeCards;
                }

                freeCards -= combinationLenght;

                int changeDirectionIndex = 0;

                if (needChangeDirection)
                {
                    changeDirectionIndex = newCombination.changeDirectionIndex;
                }

                int starCombinationCost = Random.Range(CardData.MIN_CARD_COST, CardData.MAX_CARD_COST);
                lastCost = starCombinationCost;

                newCombination.cardsCostInCombination.Add(starCombinationCost);

                int direction = isUpCombination ? 1 : -1;

                for (int i = 0; i < combinationLenght; i++)
                {
                    if (needChangeDirection && changeDirectionIndex == i)
                    {
                        direction *= -1;
                    }
                    lastCost = lastCost + direction;

                    if (lastCost > CardData.MAX_CARD_COST)
                    {
                        lastCost = CardData.MIN_CARD_COST;
                    }
                    if (lastCost < CardData.MIN_CARD_COST)
                    {
                        lastCost = CardData.MAX_CARD_COST;
                    }

                    newCombination.cardsCostInCombination.Add(lastCost);
                }
                combinations.Add(newCombination);
            }

        }
    }
}