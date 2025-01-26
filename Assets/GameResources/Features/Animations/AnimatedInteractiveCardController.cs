namespace Anasty.Solitaire.Animation
{
    using Anasty.Solitaire.Core;
    using DG.Tweening;
    using UnityEngine;

    /// <summary>
    /// Контероллер карты с анимацей
    /// </summary>
    public class AnimatedInteractiveCardController : InteractiveCardController
    {
        protected Sequence mySequence = default;

        protected Vector3 posiotion = default;

        protected override void Awake()
        {
            base.Awake();
            posiotion = transform.localPosition;
        }

        protected override void OnSuccessSelect()
        {
            mainCardStack.CurrentCardData = CurrentCardData;
            if (ParentCard != null)
            {
                ParentCard.OpenCard();
            }

            mySequence = DOTween.Sequence();

            mySequence.Append(transform.DOMove(mainCardStack.transform.position, 0.5f));

            mySequence.AppendCallback(() =>
            {
                mainCardStack.OpenCard();
                isOpen = false;
                gameObject.SetActive(false);
            });

            mySequence.Insert(0, transform.DOScale(mainCardStack.transform.localScale, mySequence.Duration()));
        }

        public override void CloseCard()
        {
            base.CloseCard();
            transform.localPosition = posiotion;
        }
    }
}