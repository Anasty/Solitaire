namespace Anasty.Solitaire.Core
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Инициализирует карты на поле
    /// </summary>
    public class FieldController : MonoBehaviour
    {
        [SerializeField]
        protected CardsDataBase cardsData = default;

        [SerializeField]
        protected CombinationsGenerator generator = default;

        [SerializeField]
        protected Bank bank = default;

        [SerializeField]
        protected List<CardController> allCards = new List<CardController>();

        [SerializeField, Range(0f, 2f)]
        private float gripRadiusMultiplayer = 2f;

        private float lastMinDistance = float.MaxValue;

        private List<CardController> combinationsCards = new List<CardController>();
        private List<CardController> openedCards = new List<CardController>();

        private void Start()
        {
            allCards = new List<CardController>(GetComponentsInChildren<CardController>());

            foreach (CardController card in allCards)
            {
                FindParent(card);
            }

            generator.GenerateCombinations();

            InitCombinations();
        }

        public void InitCombinations()
        {
            for (int j = 0; j < generator.combinations.Count; j++)
            {
                bank.AddInBank(cardsData.allCards.Find(x => x.Cost == generator.combinations[j].cardsCostInCombination[0]));
                for (int i = 1; i < generator.combinations[j].cardsCostInCombination.Count; i++)
                {
                    int rnd = Random.Range(0, combinationsCards.Count);

                    combinationsCards[rnd].currentCardData = cardsData.allCards.Find(x => x.Cost == generator.combinations[j].cardsCostInCombination[i]);
                    combinationsCards[rnd] = combinationsCards[rnd].parentCard;



                    if (combinationsCards[rnd] == null)
                    {
                        combinationsCards.RemoveAt(rnd);
                    }
                }
            }

            foreach (CardController card in openedCards)
            {
                card.OpenCard();
            }
            bank.OpenBank();
        }

        private void FindParent(CardController card)
        {
            float minDistance = lastMinDistance < float.MaxValue ? lastMinDistance * gripRadiusMultiplayer : lastMinDistance;
            float tempDistance = lastMinDistance * lastMinDistance;

            CardController parent = default;
            CardController child = default;

            int index = allCards.FindIndex(x => x == card);

            for (int i = index; i < allCards.Count; i++)
            {
                if (allCards[i] != card)
                {
                    tempDistance = Vector3.Distance(card.transform.position, allCards[i].transform.position);
                    if (tempDistance < minDistance)
                    {
                        minDistance = tempDistance;
                        child = allCards[i];
                    }
                }
            }
            if (child != null && child.parentCard == null)
            {
                child.parentCard = card;
                card.childCard = child;
            }

            for (int i = 0; i < index; i++)
            {
                if (allCards[i] != card)
                {
                    tempDistance = Vector3.Distance(card.transform.position, allCards[i].transform.position);
                    if (tempDistance < minDistance)
                    {
                        minDistance = tempDistance;
                        parent = allCards[i];
                    }
                }
            }

            if (minDistance < lastMinDistance)
            {
                lastMinDistance = minDistance;
            }

            if (parent != null && parent.childCard == null)
            {
                parent.childCard = card;
                card.parentCard = parent;
            }


            if (card.childCard == null)
            {
                openedCards.Add(card); combinationsCards.Add(card);
            }
        }
    }
}