using System.Collections.Generic;
using System.Net;
using SharedData;
using UnityEngine;

namespace ClientCode
{
    public class ClientHandle : MonoBehaviour
    {
        public static void Welcome(Packet _packet)
        {
            string _msg = _packet.ReadString();
            int _myId = _packet.ReadInt();

            Debug.Log($"Message from server: {_msg}");
            Client.instance.myId = _myId;
            Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
            
            GameManager.instance.ConnectedToServerCallback(); // Maybe add a server message to this callback
        }


        public static void LoginSuccessful(Packet _packet)
        {
            int _sessionToken = _packet.ReadInt();
            Debug.Log($"Login successful! Token: {_sessionToken}");

            GameManager.instance.LoginSuccessful(_sessionToken);
        }

        public static void LoginFailed(Packet _packet)
        {
            string _reason = _packet.ReadString();
            
            Debug.Log(_reason);
            Client.instance.TimoutConnection();
            GameEvent.instance.StopLoadingScreen();
        }


        public static void SendAccountData(Packet _packet)
        {
            int _gold = _packet.ReadInt();
            int _characterCount = _packet.ReadInt();
            
            List<CharacterData> charList = new List<CharacterData>();
            
            for(int i = 0; i < _characterCount; i++)
            {
                CharacterData _char = new CharacterData();
                _char.characterName = _packet.ReadString();
                _char.characterLevel = _packet.ReadInt();
                _char.characterExperience = _packet.ReadInt();
                _char.characterPosition = _packet.ReadVector3();
                _char.worldLocation = (WorldLocation)_packet.ReadInt();

                int _equipmentCount = _packet.ReadInt();

                for (int j = 0; j < _equipmentCount; j++)
                {
                    _char.equipments.Add(new Equipment((Equipment.ItemSlot)_packet.ReadInt()));
                }
                charList.Add(_char);
            }

            Client.instance.account = new AccountData(charList, _gold);
            
            GameManager.instance.AccountDataReceived();
        }
    }
}