using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SharedData;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ServerCode
{
    public class NetworkManager : MonoBehaviour
    {
        public static NetworkManager instance;
        public int maxPlayers = 16;
        public int serverPort = 26950;

        public string debugName;
        public string debugPass;

        [ContextMenu("Make debug account")]
        public void MakeDebugAccount()
        {
            ServerAccountManager.accounts.Add(debugName, new ServerAccount(debugName, debugPass));
        }

        [ContextMenu("Load file")]
        public void LoadFile()
        {
            ServerAccountManager.LoadServerAccounts();
        }

        [ContextMenu("Save file")]
        public void SaveFile()
        {
            ServerAccountManager.SaveServerAccounts();
        }
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Debug.Log("Instance already exists, destroying object!");
                Destroy(this);
            }
        }

        private void Start()
        {
            Server.Start(maxPlayers, serverPort);
        }

        private void OnApplicationQuit()
        {
            Server.Stop();
        }

    }
}