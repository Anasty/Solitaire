namespace Anasty.Solitaire.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Контроллер карты на поле
    /// </summary>
    public class CardController : MonoBehaviour
    {
        public event Action onInitController = delegate { };

        public CardData currentCardData = default;

        public CardController childCard = default;
        public CardController parentCard = default;
        [SerializeField]
        protected CardController target = default;

        public Image mainCardImage = default;

        public Sprite faceSprite = default;

        public Text costText = default;

        /// <summary>
        /// Инициализируется карта в комбинации
        /// </summary>
        /// <param name="newCardData"></param>
        public void Init(CardData newCardData)
        {
            currentCardData = newCardData;
            onInitController();
            //TODO: это инициализация последовательности, до этого должно быть собрано положение текущей карты и указаны ее предки и потомки
        }
        int nextCost = 0;
        int prevCost = 0;
        /// <summary>
        /// Карта была выбрана пользователем
        /// </summary>
        public void OnSelectCard()
        {
            if (isOpened)
            {
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

                if (prevCost == target.currentCardData.Cost || nextCost == target.currentCardData.Cost)
                {
                    target.currentCardData = currentCardData;
                    target.OpenCard();
                    if (parentCard != null)
                    {
                        parentCard.OpenCard();
                    }
                    gameObject.SetActive(false);
                }
            }
        }

        private bool isOpened = false;

        public void OpenCard()
        {

            isOpened = true;
            mainCardImage.sprite = faceSprite;
            costText.text = currentCardData.View;
        }
    }
}