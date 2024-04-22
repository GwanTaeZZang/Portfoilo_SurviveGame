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
    public Animator anim;

    protected float damage;
    protected float damageRate;
    protected float attackSpeed;
    protected float attackRange;

    protected float timer;


    public abstract void UpdateWeapon();
    protected abstract void Attack();
    protected abstract void LookAtEnemyInRange();


    public void SetWeaponInfo(WeaponItemInfo _info)
    {
        weaponItemInfo = _info;

        damage = _info.damage;
        damageRate = _info.damageRate;
        attackRange = _info.attackRange;
        attackSpeed = _info.attackSpeed;
    }
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

    public StingWeapon(Transform _weapon)
    {
        weapon = _weapon;
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
            //anim.Play("Weapon_0_Attack_Anim", -1, 0f);
            

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
            Vector2 v2 = endPos - startPpos;
            float angle = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;

            weapon.rotation = Quaternion.Euler(0, 0, angle);

            this.Attack();
        }
    }
}

public class ShootingWeapon : WeaponBase
{

    public ShootingWeapon(Transform _weapon)
    {
        weapon = _weapon;
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
            timer = 0;
        }
    }

    protected override void LookAtEnemyInRange()
    {
        Vector2 startPpos = weapon.position;
        Vector2 endPos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float dir = Vector2.Distance(endPos, startPpos);
        if (dir < attackRange)
        {
            Vector2 v2 = endPos - startPpos;
            float angle = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;

            weapon.rotation = Quaternion.Euler(0, 0, angle);

            Attack();
        }

    }
}

public class MowWeapon : WeaponBase
{
        public MowWeapon(Transform _weapon)
    {
        weapon = _weapon;
    }


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


