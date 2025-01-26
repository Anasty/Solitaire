namespace Anasty.Solitaire.Core
{
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
        [SerializeField]
        protected CombinationsGenerator generator = default;

        [SerializeField]
        protected CardController prefab = default;

        [SerializeField]
        protected CardController mainCardStack = default;

        protected List<CardController> cardsInBank = new List<CardController>();
        protected Button button = default;

        protected int bankIndex = 0;

        protected virtual void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OpenBank);
            generator.onCompleteGeneration += InitBank;
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
            }
        }

        protected virtual void OnDestroy()
        {
            generator.onCompleteGeneration -= InitBank;
            button.onClick.RemoveListener(OpenBank);
        }
    }
}