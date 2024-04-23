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

    protected Transform target;
    protected float damage;
    protected float damageRate;
    protected float attackSpeed;
    protected float attackRange;
    protected float timer;


    public abstract void UpdateWeapon();
    protected abstract void LookAtEnemyInRange();

    public void SetTarget(Transform _target)
    {
        target = _target;
    }
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
    protected Vector2 oriWeaponPos;
    protected Vector2 targetPos;
    protected Vector2 dir;
    protected float distance;
    protected bool isAttack = false;
    protected bool isGo = true;
    //protected bool isSetAttackVector = false;

    public StingWeapon(Transform _weapon)
    {
        weapon = _weapon;
        oriWeaponPos = _weapon.position;
    }

    public override void UpdateWeapon()
    {
        LookAtEnemyInRange();

        if (isAttack)
        {
            //Attack();
        }
    }

    protected bool Attack(Vector2 _dir)
    {

        Vector2 curWeaponPos = weapon.position;

        if (isGo)
        {
            curWeaponPos.x += _dir.x * Time.deltaTime * 15;
            curWeaponPos.y += _dir.y * Time.deltaTime * 15;
            weapon.position = curWeaponPos;

            float distance = Vector2.Distance(curWeaponPos, oriWeaponPos);
            if (distance >= attackRange)
            {
                isGo = false;
            }
        }

        else if (!isGo)
        {
            curWeaponPos.x -= _dir.x * Time.deltaTime * 15;
            curWeaponPos.y -= _dir.y * Time.deltaTime * 15;
            weapon.position = curWeaponPos;

            float distance = Vector2.Distance(curWeaponPos, oriWeaponPos);
            if (distance <= 0.5f)
            {
                weapon.localPosition = Vector2.zero;
                isGo = true;
                return true;
            }
        }

        return false;
    }


    protected override void LookAtEnemyInRange()
    {
        // 일정거리에 들어왔을 때 타겟 바라보기

        //oriWeaponPos = weapon.position;
        //targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //float dir = Vector2.Distance(oriWeaponPos, targetPos);
        //if(dir < attackRange)
        //{
        //    Vector2 v2 = targetPos - oriWeaponPos;
        //    float angle = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;

        //    weapon.rotation = Quaternion.Euler(0, 0, angle);

        //    this.AttackCounter();
        //}

        // 무조건 바라보기
        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector2.Distance(oriWeaponPos, targetPos);

        if (!isAttack)
        {
            dir = targetPos - oriWeaponPos;
            //Vector2 v2 = targetPos - oriWeaponPos;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            weapon.localRotation = Quaternion.Euler(0, 0, angle);
        }

        if (distance < attackRange)
        {
            Debug.Log("공격범위 안");
            timer += Time.deltaTime;
            if (timer > attackSpeed)
            {
                isAttack = true;

                if (Attack(dir.normalized))
                {
                    isAttack = false;
                    timer = 0;
                }
            }
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

    protected override void LookAtEnemyInRange()
    {
        Vector2 startPpos = weapon.position;
        Vector2 endPos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float dir = Vector2.Distance(endPos, startPpos);
        if (dir < attackRange)
        {
            Vector2 v2 = endPos - startPpos;
            float angle = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;

            weapon.localRotation = Quaternion.Euler(0, 0, angle);

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

    protected override void LookAtEnemyInRange()
    {
        throw new System.NotImplementedException();
    }
}


