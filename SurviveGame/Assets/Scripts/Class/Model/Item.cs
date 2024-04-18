using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem 
{
    public int price;
    public int uid;
}


public abstract class WeaponItem : BaseItem
{
    WeaponItemInfo weaponItemInfo;
    public abstract void Fire();
}

public abstract class EquipItem : BaseItem
{
    public List<StatusEffect> buff;
    public List<StatusEffect> deBuff;
}

public class SetEffect
{

}

public enum WeaponType
{
    ShotRange,
    LongRange
}


public class ShotRangeWeapon : WeaponItem
{
    public override void Fire()
    {
        throw new System.NotImplementedException();
    }
}

public class LongRangeWeapon : WeaponItem
{
    public override void Fire()
    {
        throw new System.NotImplementedException();
    }
}

[System.Serializable]
public class WeaponItemInfo
{
    public int Uid;
    public int level;
    public float damage;
    public float damageRate;
    public float attackSpeed;
    public string stringKey;
    public string weaponSpritePath;
    public string weaponName;
    public WeaponType weaponType;
}

[System.Serializable]
public class WeaponData
{
    public WeaponItemInfo[] weaponArr;
}

