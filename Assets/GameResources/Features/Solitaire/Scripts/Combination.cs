namespace Anasty.Solitaire.Core
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Сгенерированная комбинация из карточной последовательности
    /// </summary>
    [Serializable]
    public class Combination
    {
        public IReadOnlyList<CardController> CardControllers => cardControllers;
        protected List<CardController> cardControllers = new List<CardController>();

        public IReadOnlyList<int> CardsCostInCombination => cardsCostInCombination;
        protected List<int> cardsCostInCombination = new List<int>();

        /// <summary>
        /// Добавить в комбинацию следующую цену карты
        /// </summary>
        /// <param name="cost"></param>
        public void AddCostInCombination(int cost) =>
            cardsCostInCombination.Add(cost);

        /// <summary>
        /// Добавить карту принадлежащую комбинации
        /// </summary>
        /// <param name="cardController"></param>
        public void AddCardInCombination(CardController cardController)
        {
            if (cardControllers.Count == 0)
            {
                cardControllers.Add(null);
            }

            cardControllers.Add(cardController);
        }

        /// <summary>
        /// Добавить карту в стартовую позицию списка
        /// </summary>
        /// <param name="cardController"></param>
        public void AddCardInStartCombination(CardController cardController)
        {
            if (cardControllers.Count == 0)
            {
                cardControllers.Add(cardController);
            }
            else
            {
                cardControllers[0] = cardController;
            }
        }
    }
}