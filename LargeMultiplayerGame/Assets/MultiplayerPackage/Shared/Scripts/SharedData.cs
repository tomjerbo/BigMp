namespace SharedData
{
    /// <summary>Sent from server -> client.</summary>
    public enum ServerPackets
    {
        welcome = 1,
        LoginSuccessful,
        LoginFailed,
        PlayerDisconnected,
        StopConnection,
        SendAccountData,
    }

    /// <summary>Sent from client -> server.</summary>
    public enum ClientPackets
    {
        welcomeReceived = 1,
        RequestAccountDataFromServer,
    }
    
}
