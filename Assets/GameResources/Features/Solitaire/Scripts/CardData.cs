namespace Anasty.Solitaire.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Хранит информацию о карте
    /// </summary>
    [Serializable]
    public class CardData
    {
        [SerializeField, Min(MIN_CARD_COST)]
        private int _cost = 2;
        public int Cost => _cost;

        //TODO: заменить на картинку
        [SerializeField]
        private string view = string.Empty;
        public string View => view;

        public const int MAX_CARD_COST = 14;
        public const int MIN_CARD_COST = 2;
    }
}
