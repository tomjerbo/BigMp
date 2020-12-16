public class Weapon : Equipment
{
    public Weapon(WeaponType _weaponType, ItemSlot _itemSlot) : base(_itemSlot)
    {
        weaponType = _weaponType;
    }
    public WeaponType weaponType;
}

public enum WeaponType
{
    Sword,
    Axe,
    Branch,
}