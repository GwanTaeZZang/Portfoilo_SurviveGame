using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItem 
{

}

[System.Serializable]
public class WeaponItemInfo
{
    public int Uid;
    public int level;
    public float damage;
    public float damageRate;
    public float attackSpeed;
    public float attackRange;
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


public abstract class WeaponBase
{
    public int price;
    public int uid;
    public WeaponItemInfo weaponItemInfo;
    public Sprite weaponSprite;
    public abstract void UpdateWeapon();
    protected abstract void Attack();
}

public abstract class EquipItem
{
    public int price;
    public int uid;
    public List<StatusEffect> buff;
    public List<StatusEffect> deBuff;
}

public class SetEffect
{

}

public enum WeaponType
{
    StingWeapon,
    MowWeapon,
    ShoootingWeapon
}


public class StingWeapon : WeaponBase
{
    private float damage;
    private float damageRate;
    private float attackSpeed;
    private float attackRange;

    private float timer;

    public StingWeapon(WeaponItemInfo _info)
    {
        weaponItemInfo = _info;

        damage = _info.damage;
        damageRate = _info.damageRate;
        attackRange = _info.attackRange;
        attackSpeed = _info.attackSpeed;
    }

    

    public override void UpdateWeapon()
    {

    }

    protected override void Attack()
    {
        timer += Time.deltaTime;
        if (timer > attackSpeed)
        {
            Debug.Log("찌르기 무기 공격");
            timer = 0;
        }
    }
}

public class ShoootingWeapon : WeaponBase
{
    private float damage;
    private float damageRate;
    private float attackSpeed;
    private float attackRange;

    private float timer;

    public ShoootingWeapon(WeaponItemInfo _info)
    {
        weaponItemInfo = _info;

        damage = _info.damage;
        damageRate = _info.damageRate;
        attackRange = _info.attackRange;
        attackSpeed = _info.attackSpeed;
    }

    public override void UpdateWeapon()
    {

    }

    protected override void Attack()
    {
        timer += Time.deltaTime;
        if (timer > attackSpeed)
        {
            Debug.Log("발사 무기 공격");
            timer = 0;
        }
    }

}

public class MowWeapon : WeaponBase
{
    public override void UpdateWeapon()
    {
        throw new System.NotImplementedException();
    }

    protected override void Attack()
    {
        throw new System.NotImplementedException();
    }
}


