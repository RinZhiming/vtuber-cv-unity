using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using Utility;

public class MicrophonePermission : MonoBehaviour
{
    private PermissionCallbacks callbacksPermission;
    
    private void Awake()
    {
        callbacksPermission = new PermissionCallbacks();
        callbacksPermission.PermissionDenied += CallbacksPermissionOnPermissionDenied;
        callbacksPermission.PermissionGranted += CallbacksPermissionOnPermissionGranted;
    }

    private void Start()
    {
        RequestMicrophone();
    }
    
    private void RequestMicrophone()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
            Permission.RequestUserPermission(Permission.Microphone);
        else
            SceneManager.LoadScene(Scenes.SelectCharacterScene);
    }

    private void CallbacksPermissionOnPermissionGranted(string obj)
    {
        SceneManager.LoadScene(Scenes.SelectCharacterScene);
    }

    private void CallbacksPermissionOnPermissionDenied(string obj)
    {
        SceneManager.LoadScene(Scenes.SelectCharacterScene);
    }
}
