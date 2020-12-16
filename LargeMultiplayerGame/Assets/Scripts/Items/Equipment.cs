using UnityEngine;

public class Equipment
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