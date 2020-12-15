public struct CharacterData
{

    public CharacterData(string _characterName, int _characterLevel, int _characterExperience)
    {
        characterName = _characterName;
        characterLevel = _characterLevel;
        characterExperience = _characterExperience;
    }

    public string characterName;
    public int characterLevel;
    public int characterExperience;
}