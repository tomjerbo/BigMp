using SharedData;
using ServerCode;
using UnityEngine;

namespace ClientCode
{
    public class ClientSend : MonoBehaviour
    {
        
        /// <summary>Sends a packet to the server via TCP.</summary>
        /// <param name="_packet">The packet to send to the sever.</param>
        private static void SendTCPData(Packet _packet)
        {
            _packet.WriteLength();
            Client.instance.tcp.SendData(_packet);
        }

        /// <summary>Sends a packet to the server via UDP.</summary>
        /// <param name="_packet">The packet to send to the sever.</param>
        private static void SendUDPData(Packet _packet)
        {
            _packet.WriteLength();
            Client.instance.udp.SendData(_packet);
        }

        
        
        #region Packets
        
        /// <summary>Lets the server know that the welcome message was received.</summary>
        public static void WelcomeReceived(string _username, string _password, int _sessionToken)
        {
            using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
            {
                _packet.Write(Client.instance.myId);
                _packet.Write(_username);
                _packet.Write(_password);
                _packet.Write(_sessionToken);

                SendTCPData(_packet);
            }
        }
        public static void RequestAccountDataFromServer(int _sessionToken)
        {
            using (Packet _packet = new Packet((int)ClientPackets.RequestAccountDataFromServer))
            {
                _packet.Write(_sessionToken);
                
                SendTCPData(_packet);
            }
        }

        #endregion
    }

}