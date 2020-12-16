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

            //StopCoroutine(timeout);
            //StartCoroutine(timeout);
        }
        

        [ContextMenu("Leave server")]
        public void LeaveServer()
        {
            Client.instance.TimoutConnection();
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
            //StopCoroutine(timeout);
            
            // Confirm connection, request data via (RequestAccountDataFromServer).
            ClientSend.WelcomeReceived(username.text, password.text, Client.instance.sessionToken);
            
            // Start loading data during loading screen
            GameEvent.instance.StartLoadingScreen(new []{ "Session Token.","Account data", });
        }
        

        public void LoginSuccessful(int _sessionToken)
        {
            Client.instance.sessionToken = _sessionToken;
            
            // Specify what data is the client want
            ClientSend.RequestAccountDataFromServer(_sessionToken);

            // Display progress of loading it
            GameEvent.instance.LoadingItemCompleted();
        }

        public void AccountDataReceived()
        {
            // This runs once all account data has been received
            GameEvent.instance.StopLoadingScreen();
            
            // Disable loading screen and open character menu
            Debug.Log("Finished loading all account data.");

            Client.instance.account.LogData();
        }

    }

}