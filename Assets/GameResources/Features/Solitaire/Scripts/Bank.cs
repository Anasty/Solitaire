namespace Anasty.Solitaire.Core
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Отвечает за банк первых карт комбинаций
    /// </summary>
    public class Bank : MonoBehaviour
    {
        [SerializeField]
        protected CardController prefab = default;

        [SerializeField]
        protected CardController target = default;

        protected List<CardController> cardsInBank = new List<CardController>();

        public void AddInBank(CardData card)
        {
            CardController newCard = Instantiate(prefab, transform);

            newCard.currentCardData = card;
            cardsInBank.Add(newCard);
        }

        public void OpenBank()
        {
            target.currentCardData = cardsInBank[0].currentCardData;
            target.OpenCard();
            cardsInBank[0].gameObject.SetActive(false);
            cardsInBank.RemoveAt(0);
        }
    }
}