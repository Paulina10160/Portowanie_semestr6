using System;
using UnityEngine;

#if UNITY_ANDROID
using GooglePlayGames.BasicApi;
#endif

public class GoogleManager : MonoBehaviour
{
#if UNITY_ANDROID

    private void Awake() 
    {
        GooglePlayGames.PlayGamesPlatform.Activate();
        Social.localUser.Authenticate(status => { Debug.Log($"Authenticate status: {status}"); });
    }
    
#endif
}