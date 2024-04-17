using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem 
{
    public int price;
    public int uid;
    public UniqueEffect uniqueEffect;
}

public abstract class WeaponItem : BaseItem
{
    int level;
    float damage;
    float damageRate;
    float attackSpeed;
    WeaponType type;
    // 세트 효과

    protected abstract void Fire();
}

public class EquipItem : BaseItem
{
    public List<StatusEffect> buff;
    public List<StatusEffect> deBuff;
}

public class UniqueEffect
{

}

public enum WeaponType
{

}
