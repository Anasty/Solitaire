namespace Anasty.Solitaire.FinishGame
{
    /// <summary>
    /// Кнопка перезапуска текущего уровня
    /// </summary>
    public sealed class GameResetButton : AbstractButton
    {
        private GameReseter _reseter = default;

        private void Start()
        {
            _reseter = FindObjectOfType<GameReseter>();
        }

        public override void OnButtonClick()
        {
            _reseter.ResetGame();
        }
    }
}