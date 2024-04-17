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
    public int level;
    public float damage;
    public float damageRate;
    public float attackSpeed;
    public WeaponType type;

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

}


public class Knife : WeaponItem
{
    public override void Fire()
    {
        throw new System.NotImplementedException();
    }
}

public class bar : WeaponItem
{
    public override void Fire()
    {
        throw new System.NotImplementedException();
    }
}
