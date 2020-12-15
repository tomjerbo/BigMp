using System;
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
        public InputField usernameField;
        public InputField ipAdress;
        
        
        
        // Use static dictionary's to keep track of relevant object via ID sent from server on instantiation.
        // ---->  public static Dictionary<int, ComponentType> NAME = new Dictionary<int, ComponentType>();

        
        private void Awake() { if (instance == null) { instance = this; } else if (instance != this) { Destroy(this); } }
        
        /// <summary>Attempts to connect to the server.</summary>
        public void ConnectToServer() { Client.instance.ConnectToServer(ipAdress.text); }
        
    }

}