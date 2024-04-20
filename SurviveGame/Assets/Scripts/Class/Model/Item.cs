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
    public Transform weapon;
    public abstract void UpdateWeapon();
    protected abstract void Attack();
    protected abstract void LookAtEnemyInRange();
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
        LookAtEnemyInRange();
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

    protected override void LookAtEnemyInRange()
    {
        Vector2 startPpos = weapon.position;
        Vector2 endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float dir = Vector2.Distance(endPos , startPpos);
        if(dir < attackRange)
        {
            Debug.Log("범위 안에 있음");
            Vector2 v2 = endPos - startPpos;
            float angle = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;

            weapon.rotation = Quaternion.Euler(0, 0, angle);

            Attack();
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
        LookAtEnemyInRange();
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

    protected override void LookAtEnemyInRange()
    {
        Vector2 startPpos = weapon.position;
        Vector2 endPos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float dir = Vector2.Distance(endPos, startPpos);
        if (dir < attackRange - 3)
        {
            Vector2 v2 = endPos - startPpos;
            float angle = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;

            weapon.rotation = Quaternion.Euler(0, 0, angle);

            Attack();
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

    protected override void LookAtEnemyInRange()
    {
        throw new System.NotImplementedException();
    }
}


