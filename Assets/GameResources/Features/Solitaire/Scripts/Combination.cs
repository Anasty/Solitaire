namespace Anasty.Solitaire.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Сгенерированная комбинация из карточной последовательности
    /// </summary>
    [Serializable]
    public class Combination
    {
        public List<int> cardsCostInCombination = new List<int>();
        public float isUpCombination = 0f;

        public float needChangeDirection = 0f;

        public int combinationLenght = 0;

        public int changeDirectionIndex = 0;
    }
}