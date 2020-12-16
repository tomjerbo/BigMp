using System.Collections.Generic;
using ServerCode;
using UnityEngine;

public class ServerAccountManager
{
    public static Dictionary<string[], ServerAccount> accounts = new Dictionary<string[], ServerAccount>();
    

    public static void AccountLogin(int _id, string[] _credentials, int _token)
    {
        if (accounts.ContainsKey(_credentials))
        {
            if (accounts[_credentials].ValidateSessionToken(_token))
            {
                ServerSend.LoginSuccessful(_id, accounts[_credentials]);
            }
            else
            {
                ServerSend.LoginFailed(_id, "Account already logged in!");
            }
        }
        else
        {
            ServerSend.LoginFailed(_id, "No matching account found.");
        }
    }

    public static void AccountLogout()
    {
        
    }
}

public struct ServerAccount
{
    public ServerAccount(string _username, string _password)
    {
        username = _username;
        password = _password;
        sessionToken = -1;
        characters = new List<CharacterData>();
    }

    public string username;
    public string password;
    private int sessionToken;
    
    public List<CharacterData> characters;


    public bool ValidateSessionToken(int _token)
    {
        return sessionToken == _token;
    }

    public int CreateSessionToken()
    {
        sessionToken = (int)(Time.realtimeSinceStartup * Time.deltaTime);
        return sessionToken;
    }

    public void RemoveSessionToken()
    {
        sessionToken = -1;
    }

}
