using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    #region Singleton

    public static GameEvent instance;
    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;
        DontDestroyOnLoad(this);
    }

    #endregion



    public Action OnSelectCharacter;
    public Action OnConnectoToServer;
    public Action OnJoinGameWorld;
    public Action<string[]> OnStartLoadingScreen;
    public Action OnStopLoadingScreen;
    public Action OnLoadingItemCompleted;
    public Action OnLoadingAccountDataFinished;

    public void SelectCharacter() => OnSelectCharacter?.Invoke();
    public void ConnectoToServer() => OnConnectoToServer?.Invoke();
    public void JoinGameWorld() => OnJoinGameWorld?.Invoke();
    public void StartLoadingScreen(string[] _nameOfData) => OnStartLoadingScreen?.Invoke(_nameOfData);
    public void StopLoadingScreen() => OnStopLoadingScreen?.Invoke();
    public void LoadingItemCompleted() => OnLoadingItemCompleted?.Invoke();
    public void LoadingAccountDataFinished() => OnLoadingAccountDataFinished?.Invoke();

    
    
    
    
    
    
}

