using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AccountData
{
    public AccountData(string _accountName, CharacterData[] _characters, int _goldAmount)
    {
        accountName = _accountName;
        characters = _characters;
        gold = _goldAmount;
    }
    public string accountName;
    public CharacterData[] characters;
    public int gold;
}