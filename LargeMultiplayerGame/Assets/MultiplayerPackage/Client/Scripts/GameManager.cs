using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SharedData;
using UnityEngine;
using UnityEngine.UI;

namespace ClientCode
{

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        private void Awake() { if (instance == null) { instance = this; } else if (instance != this) { Destroy(this); } }

        public InputField username;
        public InputField password;

        private void Start()
        {
            //GameEvent.instance.OnTryLogIn += ConnectToServer();
        }

        // Use static dictionary's to keep track of relevant object via ID sent from server on instantiation.
        // ---->  public static Dictionary<int, ComponentType> NAME = new Dictionary<int, ComponentType>();
        


        [ContextMenu("Connect to server.")]
        public void ConnectToServer()
        {
            // Connect to server
            Client.instance.ConnectToServer();
            // Check if connection has timed out

            StopCoroutine(timeout);
            StartCoroutine(timeout);
        }
        
        private IEnumerator timeout = ConnectToServerTimout(3f);
        private static IEnumerator ConnectToServerTimout(float _maxWaitDuration)
        {
            yield return new WaitForSecondsRealtime(_maxWaitDuration);
            Client.instance.TimoutConnection();
        }

        public void ConnectedToServerCallback()
        {
            // Stop timeout coroutine since connection was successful
            StopCoroutine(timeout);
            
            // Confirm connection, request data via (RequestAccountDataFromServer).
            // Start loading data during loading screen
            GameEvent.instance.StartLoadingScreen(new []{ "Account details", "Character data", "Character equipment", "Starting location" });
            StartCoroutine(load());
        }

        private IEnumerator load()
        {
            for (int i = 0; i < 4; i++)
            {
                yield return new WaitForSecondsRealtime(2f);
                GameEvent.instance.LoadingItemCompleted();
            }
        }

        private void RequestAccountDataFromServer()
        {
            // Specify what data is the client want
            // Display progress of loading it
        }

        public void AccountDataReceived()
        {
            // This runs once all account data has been received
            // Disable loading screen and open character menu
        }
        
    }

}