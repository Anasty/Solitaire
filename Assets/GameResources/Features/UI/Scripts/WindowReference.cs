namespace Anasty.Features.UserInterface
{
    using UnityEngine;

    /// <summary>
    /// Содержит ссылку на префаб и ид
    /// </summary>
    [CreateAssetMenu(fileName = "NewWindowAsset", menuName = "Anasty/UserInterface/WindowReference", order = 0)]
    public class WindowReference : ScriptableObject
    {
        [SerializeField]
        private string windowId = string.Empty;
        public string WindowId => windowId;

        [SerializeField]
        private Window window = default;
        public Window Window => window;

        private void OnValidate()
        {
            windowId = name;
        }
    }
}