namespace Anasty.Features.UserInterface
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Управляет Ui окнами на сцене
    /// </summary>
    public class UserInterfaceController : MonoBehaviour
    {
        public static UserInterfaceController Instance { get; protected set; } = default;

        public event Action onWindowChanged = delegate { };
        public Window CurrentWindow
        {
            get => currentWindow;
            set
            {
                currentWindow = value;
                onWindowChanged();
            }
        }
        private Window currentWindow = default;

        public WindowReference StartWindow => startWindow;
        [SerializeField]
        private WindowReference startWindow = default;

        private List<WindowReference> activeWindowReferences = new List<WindowReference>();
        private List<Window> spawnedWindows = new List<Window>();

        private Stack<Window> stackWindows = new Stack<Window>();



        private void Awake()
        {
            if (Instance)
            {
                Debug.Log("WindowAgregator уже есть на сцене!");
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }

            SetWindow(startWindow);
        }

        /// <summary>
        /// Открывает определенное окно
        /// </summary>
        /// <param name="targetWindow">окно которое нужно открыть</param>
        /// <param name="closePrev">нужно ли закрывать предыдущее окно</param>
        public void SetWindow(WindowReference targetWindow, bool closePrev = false)
        {
            if (activeWindowReferences.Count > 0 && targetWindow == activeWindowReferences[0])
            {
                stackWindows.Clear();
            }

            if (CurrentWindow)
            {
                stackWindows.Push(CurrentWindow);
                if (closePrev)
                {
                    CurrentWindow.gameObject.SetActive(false);
                }
            }

            OpenWindow(targetWindow);
        }

        /// <summary>
        /// Открывает предыдущее окно, если есть возможность
        /// </summary>
        public void OpenPreviousWindow(bool needClosePrev = true)
        {
            if (stackWindows.Peek() != null && stackWindows.Peek().name != CurrentWindow.name)
            {
                Window prevWin = stackWindows.Pop();
                WindowReference targetRef = activeWindowReferences.Find(x => x.WindowId == prevWin.name);

                if (targetRef == activeWindowReferences[0])
                {
                    stackWindows.Clear();
                }

                if (CurrentWindow && needClosePrev)
                {
                    CurrentWindow.gameObject.SetActive(false);
                }
                OpenWindow(targetRef);
            }
        }

        private void OpenWindow(WindowReference targetWindow)
        {
            if (activeWindowReferences.Exists(x => x.WindowId == targetWindow.WindowId))
            {
                //открыть окно
                Window window = spawnedWindows.Find(x => x.name == targetWindow.WindowId);
                window.transform.SetAsLastSibling();
                window.gameObject.SetActive(true);
                CurrentWindow = window;

            }
            else
            {
                //заспаунить новое окно
                Window window = Instantiate(targetWindow.Window, transform);
                activeWindowReferences.Add(targetWindow);
                spawnedWindows.Add(window);
                window.name = targetWindow.WindowId;
                CurrentWindow = window;
            }
        }

        private void OnDestroy() =>
            Instance = null;
    }

}
