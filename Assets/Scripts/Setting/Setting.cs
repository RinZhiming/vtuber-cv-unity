using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utility;

namespace Setting
{
    public class Setting : MonoBehaviour
    {
        [SerializeField] private Button quit, exit;
        [SerializeField] private CanvasGroup group;

        private void Awake()
        {
            quit.onClick.AddListener(QuitButton);
            exit.onClick.AddListener(ExitButton);
        }

        private void ExitButton()
        {
            SceneManager.LoadScene(Scenes.SelectCharacterScene);
        }

        private void QuitButton()
        {
            group.SetActive(false);
        }
    }
}
