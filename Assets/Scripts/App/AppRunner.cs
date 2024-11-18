using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility;

namespace App
{
    public class AppRunner : MonoBehaviour
    {
        private void Start()
        {
            try
            {
                DontDestroyOnLoad(gameObject);

                SceneManager.LoadScene(Scenes.SelectCharacterScene);
            }
            catch (Exception e)
            {
                Debug.Log(e);
                throw;
            }
        }
    }
}
