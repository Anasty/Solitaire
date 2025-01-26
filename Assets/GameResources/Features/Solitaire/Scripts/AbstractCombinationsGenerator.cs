namespace Anasty.Solitaire.Core
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Абстракция для генераторов комбинаций карт
    /// </summary>
    public abstract class AbstractCombinationsGenerator : MonoBehaviour
    {
        public event Action onCompleteGeneration = delegate { };

        public CardsDataBase CardsData => cardsData;
        [SerializeField]
        protected CardsDataBase cardsData = default;

        [SerializeField]
        public List<Combination> combinations = new List<Combination>();

        protected int freeCards = 40;

        /// <summary>
        /// Запустить процесс генерации
        /// </summary>
        public void StartGenerateCombinations(int cardsCount)
        {
            freeCards = cardsCount;
            Generate();
            onCompleteGeneration();
        }

        /// <summary>
        /// Выполнить генерацию комбинаций
        /// </summary>
        protected abstract void Generate();
    }
}