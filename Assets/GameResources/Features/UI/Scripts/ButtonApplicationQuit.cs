namespace Anasty.Features.UserInterface
{
    using UnityEngine;
#if UNITY_EDITOR
    using UnityEditor;
#endif

    /// <summary>
    /// Кнопка для выхода из игры
    /// </summary>
    public class ButtonApplicationQuit : AbstractButton
    {
        public override void OnButtonClick()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}