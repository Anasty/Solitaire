namespace Anasty.Solitaire.Core
{
    using UnityEngine;
    using Random = UnityEngine.Random;

    /// <summary>
    /// Генератор случайных комбинаций для поля карт
    /// </summary>
    public class CombinationsGenerator : AbstractCombinationsGenerator
    {
        [SerializeField]
        protected int maxCombinationsCount = 7;
        [SerializeField]
        protected int minCombinationsCount = 2;

        [SerializeField]
        protected float chanceToUpCombination = 0.65f;

        [SerializeField]
        protected float chanceToChangeDirection = 0.15f;

        protected int lastCost = 0;
        protected override void Generate()
        {
            while (freeCards > 0)
            {
                Combination newCombination = new Combination();
                bool isUpCombination = Random.value < chanceToUpCombination;
                bool needChangeDirection = Random.value < chanceToChangeDirection;

                int combinationLenght = Random.Range(minCombinationsCount, maxCombinationsCount + 1);
                if (freeCards < maxCombinationsCount)
                {
                    combinationLenght = freeCards;
                }

                freeCards -= combinationLenght;

                int changeDirectionIndex = 0;
                if (needChangeDirection)
                {
                    changeDirectionIndex = Random.Range(0, combinationLenght);
                }

                int starCombinationCost = Random.Range(CardData.MIN_CARD_COST, CardData.MAX_CARD_COST);
                int lastCost = starCombinationCost;
                newCombination.AddCostInCombination(starCombinationCost);

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

                    newCombination.AddCostInCombination(lastCost);
                }
                combinations.Add(newCombination);
            }
        }
    }
}