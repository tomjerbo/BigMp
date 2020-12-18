using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ServerCode;
using UnityEngine;
using Random = UnityEngine.Random;

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



    public static string savePath = @"C:\Users\tomje\Desktop\ReadThisFile.txt";
    public static void LoadServerAccounts()
    {
        StreamReader stream = new StreamReader(savePath);

        while(true)
        {
            string a = stream.ReadLine();
            if (a == null) break;
            Debug.Log(a);
        }
        

        stream.Close();
    }

    public static void SaveServerAccounts()
    {
        StreamWriter stream = new StreamWriter(savePath);
        
        stream.WriteLine("START");
        
        foreach (var _pair in accounts)
        {
            stream.Write($"Username[{_pair.Key}]Password[{_pair.Value.password}]Gold[{_pair.Value.gold}]CharacterCount[{_pair.Value.characters.Count}]");
            foreach (var _character in _pair.Value.characters)
            {
                stream.Write($"Name[{_character.characterName}]");
                stream.Write($"Level[{_character.characterLevel}]");
                stream.Write($"Exp[{_character.characterExperience}]");
                stream.Write($"Location[{_character.worldLocation}]");
                stream.Write($"Pos[{_character.characterPosition}]");
                stream.Write($"ItemCount[{_character.equipments.Count}]");
                foreach (var _eq in _character.equipments)
                {
                    stream.Write($"Item{_eq.itemItemSlot}");
                }
                stream.Write("\n");
            }
        }
        
        stream.WriteLine("END");
        stream.Close();
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
