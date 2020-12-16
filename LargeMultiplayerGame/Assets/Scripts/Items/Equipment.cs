using UnityEngine;

public abstract class Equipment : ScriptableObject
{
    public Equipment(ItemSlot _itemSlot)
    {
        itemItemSlot = _itemSlot;
    }
    public ItemSlot itemItemSlot;
    public enum ItemSlot
    {
        Weapon,
        ChestPiece,
        Shoulder,
        Boots,
        Helmet,
    }
}