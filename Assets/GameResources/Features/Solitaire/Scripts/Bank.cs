namespace Anasty.Solitaire.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Отвечает за банк первых карт комбинаций
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class Bank : MonoBehaviour
    {
        public event Action onOpenBank = delegate { };

        [SerializeField]
        protected CombinationsGenerator generator = default;

        [SerializeField]
        protected CardController prefab = default;

        [SerializeField]
        protected CardController mainCardStack = default;

        protected List<CardController> cardsInBank = new List<CardController>();
        protected Button button = default;

        public int BankIndex => bankIndex;
        protected int bankIndex = 0;

        protected virtual void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OpenBank);
            generator.onCompleteGeneration += InitBank;
        }

        /// <summary>
        /// Возвращает банк к началу игры
        /// </summary>
        public void ResetBank()
        {
            foreach (CardController card in cardsInBank)
            {
                card.gameObject.SetActive(true);
            }

            bankIndex = 0;

            OpenBank();
        }

        protected void InitBank()
        {
            for (int i = 0; i < generator.combinations.Count; i++)
            {
                AddInBank(generator.CardsData.GetDataByCost(generator.combinations[i].CardsCostInCombination[0]), generator.combinations[i]);
            }
            OpenBank();
        }

        public void AddInBank(CardData card, Combination combination)
        {
            CardController newCard = Instantiate(prefab, transform);

            card.Suit = generator.CardsData.GetRandomSuit();

            combination.AddCardInStartCombination(newCard);
            newCard.CurrentCardData = card;
            cardsInBank.Add(newCard);
        }

        public void OpenBank()
        {
            if (bankIndex < cardsInBank.Count)
            {
                mainCardStack.CurrentCardData = cardsInBank[bankIndex].CurrentCardData;
                mainCardStack.OpenCard();
                cardsInBank[bankIndex].gameObject.SetActive(false);

                bankIndex++;
                onOpenBank();
            }
        }

        public bool IsBankEmpty()
        {
            return bankIndex == cardsInBank.Count;
        }

        protected virtual void OnDestroy()
        {
            generator.onCompleteGeneration -= InitBank;
            button.onClick.RemoveListener(OpenBank);
        }
    }
}