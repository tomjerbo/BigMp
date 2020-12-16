using UnityEngine;

public struct CharacterData
{
    public CharacterData(string _characterName, int _characterLevel, int _characterExperience, Vector3 _characterPosition, WorldLocation _worldLocation, Equipment[] _equipments)
    {
        characterName = _characterName;
        characterLevel = _characterLevel;
        characterExperience = _characterExperience;
        characterPosition = _characterPosition;
        worldLocation = _worldLocation;
        equipments = _equipments;
    }

    public string characterName;
    public int characterLevel;
    public int characterExperience;
    public Vector3 characterPosition;
    public WorldLocation worldLocation;
    public Equipment[] equipments;
}

public enum WorldLocation
{
    Start,
    Cave,
    Wildlands,
}
