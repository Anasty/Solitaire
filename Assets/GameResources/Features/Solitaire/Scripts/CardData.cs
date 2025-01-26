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
        public event Action onSuitChanged = delegate { };

        /// <summary>
        /// Ценность карты
        /// </summary>
        public int Cost => _cost;
        [SerializeField, Min(MIN_CARD_COST)]
        private int _cost = 2;

        /// <summary>
        /// Обозначение карты
        /// </summary>
        public string View => _view;
        [SerializeField]
        private string _view = string.Empty;

        /// <summary>
        /// Масть карты
        /// </summary>
        public CardsSuits Suit
        {
            get => suit;
            set
            {
                suit = value;
                onSuitChanged();
            }
        }
        private CardsSuits suit = default;

        public const int MAX_CARD_COST = 14;
        public const int MIN_CARD_COST = 2;
    }
}
