using System.Linq;
using SharedData;
using UnityEngine;

namespace ServerCode
{
    public class ServerHandle
    {
        public static void WelcomeReceived(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string _username = _packet.ReadString();
            string _password = _packet.ReadString();
            int _token = _packet.ReadInt();

            if (_fromClient != _clientIdCheck) ServerSend.StopConnection(_clientIdCheck);
            
            ServerAccountManager.AccountLogin(_fromClient, new []{_username,_password}, _token);

        }

        
        
    }
}