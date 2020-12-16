using System.Linq;
using SharedData;
using TMPro;
using UnityEngine;

namespace ServerCode
{
    public class ServerSend
    {
        
        #region Generic packets
        /// <summary>Sends a packet to a client via TCP.</summary>
        /// <param name="_toClient">The client to send the packet the packet to.</param>
        /// <param name="_packet">The packet to send to the client.</param>
        private static void SendTCPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].tcp.SendData(_packet);
        }

        /// <summary>Sends a packet to a client via UDP.</summary>
        /// <param name="_toClient">The client to send the packet the packet to.</param>
        /// <param name="_packet">The packet to send to the client.</param>
        private static void SendUDPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].udp.SendData(_packet);
        }

        /// <summary>Sends a packet to all clients via TCP.</summary>
        /// <param name="_packet">The packet to send.</param>
        private static void SendTCPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].tcp.SendData(_packet);
            }
        }

        /// <summary>Sends a packet to all clients except one via TCP.</summary>
        /// <param name="_exceptClient">The client to NOT send the data to.</param>
        /// <param name="_packet">The packet to send.</param>
        private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].tcp.SendData(_packet);
                }
            }
        }

        /// <summary>Sends a packet to all clients via UDP.</summary>
        /// <param name="_packet">The packet to send.</param>
        private static void SendUDPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].udp.SendData(_packet);
            }
        }

        /// <summary>Sends a packet to all clients except one via UDP.</summary>
        /// <param name="_exceptClient">The client to NOT send the data to.</param>
        /// <param name="_packet">The packet to send.</param>
        private static void SendUDPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].udp.SendData(_packet);
                }
            }
        }

        #endregion
        
        
        #region Packets

        /// <summary>Sends a welcome message to the given client.</summary>
        /// <param name="_toClient">The client to send the packet to.</param>
        /// <param name="_msg">The message to send.</param>
        public static void Welcome(int _toClient, string _msg)
        {
            using (Packet _packet = new Packet((int) ServerPackets.welcome))
            {
                _packet.Write(_msg);
                _packet.Write(_toClient);

                SendTCPData(_toClient, _packet);
            }
        }
        
        public static void LoginSuccessful(int _toClient, int _sessionToken)
        {
            using (Packet _packet = new Packet((int) ServerPackets.LoginSuccessful))
            {
                _packet.Write(_sessionToken);
                
                SendTCPData(_toClient, _packet);
            }
        }

        
        
        public static void LoginFailed(int _toClient, string _message)
        {
            using (Packet _packet = new Packet((int) ServerPackets.LoginFailed))
            {
                _packet.Write(_message);
                
                SendTCPData(_toClient, _packet);
            }
        }


        
        
        
        public static void SendAccountData(int _toClient, ServerAccount _accountData)
        {
            using (Packet _packet = new Packet((int) ServerPackets.SendAccountData))
            {
                _packet.Write(_accountData.gold);
                _packet.Write(_accountData.characters.Count);
                foreach (var _character in _accountData.characters)
                {
                    _packet.Write(_character.characterName);
                    _packet.Write(_character.characterLevel);
                    _packet.Write(_character.characterExperience);
                    _packet.Write(_character.characterPosition);
                    _packet.Write((int)_character.worldLocation);
                    _packet.Write(_character.equipments.Count);
                    foreach (var _equipment in _character.equipments)
                    {
                        _packet.Write((int)_equipment.itemItemSlot);
                    }
                }
                
                SendTCPData(_toClient, _packet);
            }
        }

        public static void PlayerDisconnected(int _playerId)
        {
            using (Packet _packet = new Packet((int) ServerPackets.PlayerDisconnected))
            {
                _packet.Write(_playerId);

                SendTCPDataToAll(_packet);
            }
        }

        public static void StopConnection(int _playerId)
        {
            using (Packet _packet = new Packet((int) ServerPackets.StopConnection))
            {
                SendTCPData(_playerId, _packet);
            }
        }
        
        #endregion
    }
}