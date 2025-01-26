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
        public event Action<InteractiveCardController> onSelectCard = delegate { };

        public InteractiveCardController ChildCard = default;
        public InteractiveCardController ParentCard = default;

        protected Button button = default;

        protected CardController mainCardStack = default;

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
            if (isOpen)
            {
                if (prevCost == mainCardStack.CurrentCardData.Cost || nextCost == mainCardStack.CurrentCardData.Cost)
                {
                    mainCardStack.CurrentCardData = currentCardData;
                    mainCardStack.OpenCard();
                    if (ParentCard != null)
                    {
                        ParentCard.OpenCard();
                    }
                    gameObject.SetActive(false);
                }
            }
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(OnSelectCard);
        }
    }
}