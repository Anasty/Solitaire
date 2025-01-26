namespace Anasty.Solitaire.Core
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Контроллер карты на поле
    /// </summary>
    public class CardController : MonoBehaviour
    {
        public event Action onCardDataChanged = delegate { };

        public event Action onCardOpen = delegate { };

        /// <summary>
        /// Текущая дата карты
        /// </summary>
        public CardData CurrentCardData
        {
            get => currentCardData;
            set
            {
                currentCardData = value;

                nextCost = currentCardData.Cost + 1;
                if (nextCost > CardData.MAX_CARD_COST)
                {
                    nextCost = CardData.MIN_CARD_COST;
                }
                prevCost = currentCardData.Cost - 1;
                if (prevCost < CardData.MIN_CARD_COST)
                {
                    prevCost = CardData.MAX_CARD_COST;
                }

                onCardDataChanged();
            }
        }
        protected CardData currentCardData = default;

        public bool IsOpen => isOpen;
        protected bool isOpen = false;

        public int NextCost => nextCost;
        protected int nextCost = 0;
        public int PrevCost => prevCost;
        protected int prevCost = 0;

        public virtual void OpenCard()
        {
            isOpen = true;
            onCardOpen();
        }
    }
}