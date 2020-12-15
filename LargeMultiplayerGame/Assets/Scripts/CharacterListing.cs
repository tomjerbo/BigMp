using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterListing : MonoBehaviour
{
    [SerializeField] private Text name;
    [SerializeField] private Text level;
    [SerializeField] private Text location;
    [SerializeField] private Text gold;
    [SerializeField] private Image avatar;

    private void Setup(string _name, string _location, int _level, int _gold, int _avatarIndex)
    {
        name.text = _name;
        location.text = _location;
        level.text = $"LVL: {_level}";
        gold.text = $"Gold: {_gold}";
        //avatar.sprite = GameAsset.instance.AvatarIcon(_avatarIndex);
    }
}
