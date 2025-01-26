namespace Anasty.Solitaire.Animation
{
    using Anasty.Solitaire.Core;
    using DG.Tweening;
    using UnityEngine;

    /// <summary>
    /// Отображение карты с анимацией
    /// </summary>
    public class CardViewWithAnimation : CardView
    {
        protected Sequence mySequence = default;
        public override void UpdateView()
        {
            UpdateData();

            if (currentCard.IsOpen)
            {
                Sequence mySequence = DOTween.Sequence();
                mySequence.Append(transform.DOScaleX(0, 0.5f));

                mySequence.AppendCallback(() =>
                {
                    mainCardImage.sprite = currentCard.IsOpen ? faceSprite : backSprite;

                    suitImages.ForEach(x => x.gameObject.SetActive(currentCard.IsOpen));

                    costText.gameObject.SetActive(currentCard.IsOpen);
                });
                mySequence.Append(transform.DOScaleX(1, 0.5f));
            }
            else
            {
                transform.localScale = Vector3.one;

                mainCardImage.sprite = currentCard.IsOpen ? faceSprite : backSprite;

                suitImages.ForEach(x => x.gameObject.SetActive(currentCard.IsOpen));

                costText.gameObject.SetActive(currentCard.IsOpen);

            }
        }
    }
}