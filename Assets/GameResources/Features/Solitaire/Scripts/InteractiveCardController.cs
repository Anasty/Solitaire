namespace Anasty.Solitaire.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Интерактивная карта на поле
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class InteractiveCardController : CardController
    {
        public event Action onSelectCard = delegate { };

        public InteractiveCardController ChildCard = default;
        public InteractiveCardController ParentCard = default;

        protected Button button = default;

        protected CardController mainCardStack = default;

        protected bool canSelect = default;

        protected virtual void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnSelectCard);
        }

        /// <summary>
        /// Устанавливает стопку в которую будем собирать карты
        /// </summary>
        /// <param name="cardController"></param>
        public void SetMainStack(CardController cardController) =>
            mainCardStack = cardController;

        /// <summary>
        /// Карта была выбрана пользователем
        /// </summary>
        public virtual void OnSelectCard()
        {
            if (CanSelectCard())
            {
                mainCardStack.CurrentCardData = CurrentCardData;
                mainCardStack.OpenCard();
                if (ParentCard != null)
                {
                    ParentCard.OpenCard();
                }
                isOpen = false;
                gameObject.SetActive(false);
                onSelectCard();
            }
        }

        public bool CanSelectCard()
        {
            return isOpen && (prevCost == mainCardStack.CurrentCardData.Cost || nextCost == mainCardStack.CurrentCardData.Cost);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(OnSelectCard);
        }
    }
}