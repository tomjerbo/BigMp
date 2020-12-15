using System.Linq;
using SharedData;
using UnityEngine;

namespace ServerCode
{
    public class ServerHandle
    {
        public static void WelcomeReceived(int _fromClient, Packet _packet)
        {
            Debug.Log("Received welcome back from client.");

            int _clientIdCheck = _packet.ReadInt();
            string _username = _packet.ReadString();

            Debug.Log($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
            if (_fromClient != _clientIdCheck)
            {
                Debug.Log($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
                // Maybe worth disconnecting the player. Should only happen if tampered with client ID.
            }
        }

    }
}