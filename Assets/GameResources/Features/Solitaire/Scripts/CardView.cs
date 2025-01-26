namespace Anasty.Solitaire.Core
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Отображение карты на поле
    /// </summary>
    [RequireComponent(typeof(CardController))]
    public class CardView : MonoBehaviour
    {
        public CardController CurrentCard => currentCard;
        protected CardController currentCard = default;

        [SerializeField]
        protected Text costText = default;

        [SerializeField]
        protected List<Image> suitImages = new List<Image>();

        [SerializeField]
        protected Image mainCardImage = default;

        [SerializeField]
        protected Sprite faceSprite = default;
        [SerializeField]
        protected Sprite backSprite = default;


        protected virtual void Awake()
        {
            currentCard = GetComponent<CardController>();
            currentCard.onCardDataChanged += UpdateData;
            currentCard.onCardClose += UpdateView;

            currentCard.onCardOpen += UpdateView;

            mainCardImage.sprite = currentCard.IsOpen ? faceSprite : backSprite;

            suitImages.ForEach(x => x.gameObject.SetActive(currentCard.IsOpen));

            costText.gameObject.SetActive(currentCard.IsOpen);
        }

        public void UpdateData()
        {
            if (currentCard.CurrentCardData != null)
            {
                foreach (Image image in suitImages)
                {
                    image.sprite = currentCard.CurrentCardData.Suit.SuiteSprite;
                    costText.text = currentCard.CurrentCardData.View;
                    costText.color = currentCard.CurrentCardData.Suit.SuitColor;
                }
            }

        }

        public void UpdateView()
        {
            UpdateData();

            mainCardImage.sprite = currentCard.IsOpen ? faceSprite : backSprite;

            suitImages.ForEach(x => x.gameObject.SetActive(currentCard.IsOpen));

            costText.gameObject.SetActive(currentCard.IsOpen);

            //TODO: playAnimation
        }

        private void OnDestroy()
        {
            currentCard.onCardDataChanged -= UpdateData;
            currentCard.onCardOpen -= UpdateView;
            currentCard.onCardClose -= UpdateView;
        }
    }
}