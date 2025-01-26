namespace Anasty.Features.UserInterface
{
    using UnityEngine;

    /// <summary>
    /// Кнопка открытия окна
    /// </summary>
    public class ButtonOpenWindow : AbstractButton
    {
        [SerializeField]
        protected WindowReference targetWindow = default;

        [SerializeField]
        protected bool needClosePrevWindow = true;

        public override void OnButtonClick() =>
            UserInterfaceController.Instance.SetWindow(targetWindow, needClosePrevWindow);
    }
}