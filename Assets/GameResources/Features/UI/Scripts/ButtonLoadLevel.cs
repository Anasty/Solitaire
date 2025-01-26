namespace Anasty.Features.UserInterface
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// Загружает уровень по названию
    /// </summary>
    public class ButtonLoadLevel : AbstractButton
    {
        [SerializeField]
        protected string levelName = string.Empty;

        public override void OnButtonClick()
        {
            SceneManager.LoadScene(levelName);
        }
    }
}