using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Text loadingProgress;
    private int completedItems;
    private List<string> nameOfData = new List<string>();
    

    private void Start()
    {
        GameEvent.instance.OnStartLoadingScreen += StartLoadingScreen;
        GameEvent.instance.OnLoadingItemCompleted += CompletedLoadingItem;
        GameEvent.instance.OnStopLoadingScreen += StopLoadingScreen;
    }

    private void StartLoadingScreen(string[] _nameOfData)
    {
        loadingScreen.SetActive(true);
        nameOfData.AddRange(_nameOfData);
        loadingProgress.text = $"Loading: [{nameOfData[completedItems]}].. {completedItems} / {nameOfData.Count}";
    }

    private void CompletedLoadingItem()
    {
        completedItems++;
        loadingProgress.text = completedItems < nameOfData.Count 
            ? $"Loading: [{nameOfData[completedItems]}].. {completedItems} / {nameOfData.Count}"     
            : $"{completedItems} / {nameOfData.Count} Loaded. Finishing up..";
    }

    private void StopLoadingScreen()
    {
        nameOfData.Clear();
        completedItems = 0;
        loadingScreen.SetActive(false);
    }
    
}
