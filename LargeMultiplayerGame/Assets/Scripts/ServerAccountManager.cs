using System.Collections.Generic;
using ServerCode;
using UnityEngine;

public class ServerAccountManager
{

    public static Dictionary<string, ServerAccount> accounts= new Dictionary<string, ServerAccount>();
    
    
    public static void AccountLogin(int _id, string _username, string _password, int _token)
    {
        if (accounts.ContainsKey(_username) && accounts[_username].password == _password)
        {
            if (accounts[_username].ValidateSessionToken(_token))
            {
                Server.clients[_id].SetNewOwner(accounts[_username].username);
                ServerSend.LoginSuccessful(_id, accounts[_username].CreateSessionToken());
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

    public static void AccountLogout(int _id)
    {
        accounts[Server.clients[_id].owner].RemoveSessionToken();
    }



    public static void RequestAccountData(int _id, int _token)
    {
        if (Server.clients.TryGetValue(_id, out Client _client))
        {
            if (accounts.ContainsKey(_client.owner))
            {
                if (accounts[_client.owner].ValidateSessionToken(_token))
                {
                    ServerSend.SendAccountData(_id, accounts[_client.owner]);
                }
            }
        }
    }


    public static void CreateNewAccount()
    {
        
    }


    public static void LoadServerAccounts()
    {
        
    }
    
}








public class ServerAccount
{
    public ServerAccount(string _username, string _password)
    {
        username = _username;
        password = _password;
        characters.Add(new CharacterData());
    }

    public string username;
    public string password;
    public int gold = 0;
    private int sessionToken = -1;
    
    public List<CharacterData> characters = new List<CharacterData>();


    public bool ValidateSessionToken(int _token)
    {
        Debug.Log($"Checking token {_token} -> {sessionToken}");
        return sessionToken == _token;
    }

    public int CreateSessionToken()
    {
        sessionToken = (int)(Random.insideUnitSphere * Random.Range(1000,9999)).magnitude;
        return sessionToken;
    }

    public void RemoveSessionToken()
    {
        Debug.Log("Token reset");
        sessionToken = -1;
    }

}
