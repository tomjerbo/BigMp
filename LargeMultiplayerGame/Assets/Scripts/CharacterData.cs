using System.Collections.Generic;
using UnityEngine;

public class CharacterData
{
    public string characterName = "Empty";
    public int characterLevel = 1;
    public int characterExperience = 0;
    public Vector3 characterPosition = Vector3.zero;
    public WorldLocation worldLocation = WorldLocation.Start;
    public List<Equipment> equipments = new List<Equipment>();
}

public enum WorldLocation
{
    Start,
    Cave,
    Wildlands,
}
