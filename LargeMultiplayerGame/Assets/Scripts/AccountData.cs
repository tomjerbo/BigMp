using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountData
{
    public AccountData(List<CharacterData> _characters, int _goldAmount)
    {
        characters = _characters;
        gold = _goldAmount;
    }
    
    public List<CharacterData> characters;
    public int gold;


    public void LogData()
    {
        Debug.Log($"Gold: {gold}");
        Debug.Log("Characters:");
        for (int i = 0; i < characters.Count; i++)
        {
            Debug.Log($"#{i}");
            Debug.Log("Name: "+characters[i].characterName);
            Debug.Log("Lvl: "+characters[i].characterLevel);
            Debug.Log("Exp: "+characters[i].characterExperience);
            Debug.Log("W/Loc: "+characters[i].worldLocation);
            Debug.Log("Pos: "+characters[i].characterPosition);

            for (int j = 0; j < characters[i].equipments.Count; j++)
            {
                Debug.Log($"Item#{j}, slot: "+characters[i].equipments[j].itemItemSlot);
            }
        }

        Debug.Log("Done dumping account data.");
    }
}