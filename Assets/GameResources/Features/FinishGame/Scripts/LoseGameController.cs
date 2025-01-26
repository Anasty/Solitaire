namespace Anasty.Solitaire.FinishGame
{
    using Anasty.Features.UserInterface;
    using Anasty.Solitaire.Core;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Отслеживает поражение в игре
    /// </summary>
    public class LoseGameController : MonoBehaviour
    {
        public event Action onLoseGame = delegate { };

        [SerializeField]
        protected WindowReference targetWindow = default;

        [SerializeField]
        protected Transform cardsParent = default;

        [SerializeField]
        protected Bank bank = default;

        protected List<InteractiveCardController> allCards = new List<InteractiveCardController>();

        protected InteractiveCardController card = default;

        protected virtual void Start()
        {
            allCards = new List<InteractiveCardController>(cardsParent.GetComponentsInChildren<InteractiveCardController>());

            foreach (InteractiveCardController card in allCards)
            {
                card.onCardOpen += CheckLose;
            }
            bank.onOpenBank += CheckLose;
        }

        protected void CheckLose()
        {
            if (bank.IsBankEmpty())
            {
                card = allCards.Find(x => x.CanSelectCard() == true);

                if (!card)
                {
                    UserInterfaceController.Instance.SetWindow(targetWindow, true);
                    onLoseGame();
                }
            }
        }

        protected virtual void OnDestroy()
        {
            foreach (InteractiveCardController card in allCards)
            {
                card.onCardOpen -= CheckLose;
            }
            bank.onOpenBank -= CheckLose;
        }
    }
}