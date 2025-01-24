namespace Anasty.Solitaire.Core
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// База с возможными картами
    /// </summary>
    [CreateAssetMenu(fileName = "CardsDataBase", menuName = "Solitaire/Core/CardsDatabase")]
    public class CardsDataBase : ScriptableObject
    {
        [SerializeField]
        public List<CardData> allCards = new List<CardData>();
    }
}