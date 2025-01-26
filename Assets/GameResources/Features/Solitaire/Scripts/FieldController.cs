namespace Anasty.Solitaire.Core
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Random = UnityEngine.Random;

    /// <summary>
    /// Инициализирует карты на поле
    /// </summary>
    public class FieldController : MonoBehaviour
    {
        public event Action onAllCardsInited = delegate { };

        public IReadOnlyList<CardController> AllCards => allCards;
        protected List<InteractiveCardController> allCards = new List<InteractiveCardController>();

        public IReadOnlyList<InteractiveCardController> LastCardsInStacks => lastCardsInStacks;
        protected List<InteractiveCardController> lastCardsInStacks = new List<InteractiveCardController>();

        [SerializeField]
        protected CardController mainCardStack = default;

        [SerializeField]
        protected CombinationsGenerator generator = default;

        [SerializeField, Range(1f, 5f)]
        protected float gripRadiusMultiplayer = 2f;


        protected float minDistance = 0f;
        protected float tempDistance = 0f;

        protected virtual void Start()
        {
            allCards = new List<InteractiveCardController>(GetComponentsInChildren<InteractiveCardController>());

            foreach (InteractiveCardController card in allCards)
            {
                FindNeighbours(card);
                card.SetMainStack(mainCardStack);
            }

            generator.StartGenerateCombinations(allCards.Count);

            InitCombinations();
        }

        /// <summary>
        /// Возвращает поле в начальное состояние
        /// </summary>
        public void ResetField()
        {
            foreach (CardController card in allCards)
            {
                card.CloseCard();
                card.gameObject.SetActive(true);
            }
            foreach (CardController card in lastCardsInStacks)
            {
                card.OpenCard();
            }
        }

        public void InitCombinations()
        {
            List<InteractiveCardController> cardsStacks = new List<InteractiveCardController>(lastCardsInStacks);

            for (int j = 0; j < generator.combinations.Count; j++)
            {

                for (int i = 1; i < generator.combinations[j].CardsCostInCombination.Count; i++)
                {
                    int rnd = Random.Range(0, cardsStacks.Count);

                    CardData tempCardData = generator.CardsData.GetDataByCost(generator.combinations[j].CardsCostInCombination[i]);
                    tempCardData.Suit = generator.CardsData.GetRandomSuit();

                    cardsStacks[rnd].CurrentCardData = tempCardData;

                    generator.combinations[j].AddCardInCombination(cardsStacks[rnd]);
                    cardsStacks[rnd] = cardsStacks[rnd].ParentCard;

                    if (cardsStacks[rnd] == null)
                    {
                        cardsStacks.RemoveAt(rnd);
                    }
                }
            }

            foreach (CardController card in lastCardsInStacks)
            {
                card.OpenCard();
            }
            onAllCardsInited();
        }

        protected void FindNeighbours(InteractiveCardController card)
        {
            minDistance = card.GetComponent<RectTransform>().sizeDelta.y;

            InteractiveCardController parent = default;
            InteractiveCardController child = default;

            int index = allCards.FindIndex(x => x == card);

            child = FindNearestInList(index, allCards.Count, card);

            if (child != null && child.ParentCard == null)
            {
                child.ParentCard = card;
                card.ChildCard = child;
            }

            parent = FindNearestInList(0, index, card);


            if (parent != null && parent.ChildCard == null)
            {
                parent.ChildCard = card;
                card.ParentCard = parent;
            }

            if (card.ChildCard == null)
            {
                lastCardsInStacks.Add(card);
            }
        }

        protected InteractiveCardController FindNearestInList(int startIndex, int endIndex, InteractiveCardController card)
        {
            InteractiveCardController nearest = default;

            for (int i = startIndex; i < endIndex; i++)
            {
                if (allCards[i] != card)
                {
                    tempDistance = Vector3.Distance(card.GetComponent<RectTransform>().position, allCards[i].GetComponent<RectTransform>().position);

                    if (tempDistance < minDistance)
                    {
                        if (nearest)
                        {
                            if (allCards[i].transform.GetSiblingIndex() < nearest.transform.GetSiblingIndex())
                            {
                                minDistance = tempDistance;
                                nearest = allCards[i];
                            }
                        }
                        else
                        {
                            minDistance = tempDistance;
                            nearest = allCards[i];
                        }
                    }
                }
            }
            return nearest;
        }
    }
}