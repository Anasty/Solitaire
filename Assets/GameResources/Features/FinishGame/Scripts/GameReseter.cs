namespace Anasty.Solitaire.FinishGame
{
    using Anasty.Solitaire.Core;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Перезапускает тот же уровень с уже сгенерированными комбинациями
    /// </summary>
    public class GameReseter : MonoBehaviour
    {
        [SerializeField]
        protected Bank bank = default;

        [SerializeField]
        protected FieldController field = default;

        [SerializeField]
        protected WinGameController winGameController = default;

        public void ResetGame()
        {
            bank.ResetBank();
            winGameController.ResetController();
            field.ResetField();
        }
    }
}