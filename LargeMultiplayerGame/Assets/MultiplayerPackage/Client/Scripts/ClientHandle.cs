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
            ClientSend.WelcomeReceived();
            Debug.Log("Sent welcome received.");
        }

    }
}