namespace Anasty.Features.UserInterface
{
    using UnityEngine;

    /// <summary>
    /// Кнопка для закрытия текущего окна и перехода к предыдущему
    /// </summary>
    public class ButtonCloseWindow : AbstractButton
    {
        [SerializeField]
        protected bool needClosePrevWindow = true;

        public override void OnButtonClick() =>
            UserInterfaceController.Instance.OpenPreviousWindow(needClosePrevWindow);
    }
}