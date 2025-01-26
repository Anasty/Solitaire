namespace Anasty.Solitaire.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Random = UnityEngine.Random;

    /// <summary>
    /// База с возможными картами
    /// </summary>
    [CreateAssetMenu(fileName = "CardsDataBase", menuName = "Solitaire/Core/CardsDatabase")]
    public sealed class CardsDataBase : ScriptableObject
    {
        /// <summary>
        /// Список возможных вариантов карт
        /// </summary>
        public IReadOnlyList<CardData> AllCards => _allCards;

        [SerializeField]
        private List<CardData> _allCards = new List<CardData>();

        /// <summary>
        /// Список возможных мастей
        /// </summary>
        public IReadOnlyList<CardsSuits> AllSuits => _allSuits;

        [SerializeField]
        private List<CardsSuits> _allSuits = new List<CardsSuits>();

        private void OnValidate()
        {
            foreach (var element in _allCards)
            {
                if (_allCards.FindAll(x => x.Cost == element.Cost).Count > 1)
                {
                    Debug.LogError("В списке карт найдены дубликаты, проверьте корректность списка");
                }
            }
        }

        /// <summary>
        /// Получить случайную масть
        /// </summary>
        /// <returns></returns>
        public CardsSuits GetRandomSuit()
        {
            return _allSuits[Random.Range(0, _allSuits.Count)];
        }

        /// <summary>
        ///  Возвращает подходящую дату по запрошенной ценности
        /// </summary>
        /// <param name="cost"></param>
        /// <returns></returns>
        public CardData GetDataByCost(int cost) =>
            _allCards.Find(x => x.Cost == cost);
    }
    [Serializable]
    public class CardsSuits
    {
        public Sprite SuiteSprite => suitSprite;
        [SerializeField]
        protected Sprite suitSprite = default;

        public Color SuitColor => suitColor;
        [SerializeField]
        protected Color suitColor = Color.red;
    }
}