namespace Anasty.Solitaire.FinishGame
{
    using Anasty.Features.UserInterface;
    using Anasty.Solitaire.Core;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Отслеживает побежу в игре
    /// </summary>
    public class WinGameController : MonoBehaviour
    {
        public event Action onWinGame = delegate { };

        [SerializeField]
        protected WindowReference targetWindow = default;

        [SerializeField]
        protected Transform cardsParent = default;

        protected List<InteractiveCardController> allCards = new List<InteractiveCardController>();
        protected int openCardsCount = 0;

        protected virtual void Awake()
        {
            allCards = new List<InteractiveCardController>(cardsParent.GetComponentsInChildren<InteractiveCardController>());

            foreach (InteractiveCardController card in allCards)
            {
                card.onSelectCard += OnOpenAnyCard;
            }
        }

        public void ResetController()
        {
            openCardsCount = 0;
        }

        protected void OnOpenAnyCard()
        {
            openCardsCount++;
            if (openCardsCount == allCards.Count)
            {
                UserInterfaceController.Instance.SetWindow(targetWindow, true);
                onWinGame();
            }
        }

        protected virtual void OnDestroy()
        {
            foreach (InteractiveCardController card in allCards)
            {
                card.onSelectCard -= OnOpenAnyCard;
            }
        }
    }
}