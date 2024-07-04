using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WeaponArrData
{
    public WeaponItemInfo[] weaponArr;
}


[System.Serializable]
public class WeaponItemInfo : BaseItemInfo
{
    public int level;
    public int penetrateCount;
    public float damage;
    public float damageRate;
    public float attackSpeed;
    public float attackRange;
    public string stringKey;
    public WeaponType weaponType;
}



public abstract class WeaponBase
{
    public WeaponItemInfo weaponItemInfo;
    public Sprite weaponSprite;
    public Transform weapon;
    public delegate void OnAttackDelegate(bool _isAttack);
    public OnAttackDelegate OnAttackEvent;

    protected Transform parent;
    protected BoxInfo target = null;
    protected float timer;

    protected Vector2 oriWeaponPos;
    protected Vector2 dir;
    protected float distance;
    protected bool isAttack = false;


    public abstract void UpdateWeapon();
    public abstract WeaponBase DeepCopy();

    public void SetParent(Transform _parent)
    {
        parent = _parent;
    }

    protected virtual void Initiailzed()
    {

    }

    protected virtual void LookAtEnemyInRange()
    {
        if (target == null || isAttack)
        {
            return;
        }
        distance = Vector2.Distance(weapon.position, target.center);


        dir = (Vector2)target.center - (Vector2)parent.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        parent.rotation = Quaternion.Euler(0, 0, angle);


        timer += Time.deltaTime;

        if (distance < weaponItemInfo.attackRange || isAttack)
        {
            if (timer > weaponItemInfo.attackSpeed)
            {
                isAttack = true;
                OnAttackEvent?.Invoke(isAttack);
            }
        }
    }


    protected virtual void FindTarget()
    {
        float compareDistance = float.MaxValue;
        ITargetAble[] tagetArr = MonsterManager.getInstance.GetTargetArr();
        int count = tagetArr.Length;

        for(int i =0;i < count; i++)
        {
            ITargetAble monster = tagetArr[i];
            if (monster.IsCollision())
            {
                break;
            }
            else if(i == count - 1)
            {
                target = null;
            }
        }

        for (int i = 0; i < count; i++)
        {
            ITargetAble monster = tagetArr[i];

            if (monster.IsCollision())
            {
                float distance = Vector2.Distance(weapon.position, monster.GetBoxInfo().center);

                if (compareDistance > distance)
                {
                    compareDistance = distance;
                    target = monster.GetBoxInfo();
                }
            }
        }
    }

    public void SetWeaponInfo(WeaponItemInfo _info)
    {
        weaponItemInfo = _info;
        Initiailzed();
    }
    public void SetWeapon(Transform _weapon)
    {
        weapon = _weapon;
        oriWeaponPos = _weapon.localPosition;
    }
}

public enum WeaponType
{
    StingWeapon,
    MowWeapon,
    ShoootingWeapon,
    End,
}


public class StingWeapon : WeaponBase
{
    private const int ATTACK_SPEED = 15;
    protected bool isGo = true;
    public override void UpdateWeapon()
    {
        base.FindTarget();
        LookAtEnemyInRange();

        if (isAttack)
        {
            if (Attack())
            {
                isAttack = false;
                OnAttackEvent?.Invoke(isAttack);
                timer = 0;
            }
        }
    }

    protected bool Attack()
    {

        Vector2 curWeaponPos = weapon.localPosition;

        if (isGo)
        {
            curWeaponPos.x += Time.deltaTime * ATTACK_SPEED;

            weapon.localPosition = curWeaponPos;

            float distance = Vector2.Distance(curWeaponPos, oriWeaponPos);
            if (distance >= weaponItemInfo.attackRange)
            {
                isGo = false;
            }
        }

        else if (!isGo)
        {
            curWeaponPos.x -= Time.deltaTime * ATTACK_SPEED;

            weapon.localPosition = curWeaponPos;

            float distance = Vector2.Distance(curWeaponPos, oriWeaponPos);
            if (distance <= 0.5f)
            {
                weapon.localPosition = oriWeaponPos;
                isGo = true;
                return true;
            }
        }

        return false;
    }

    public override WeaponBase DeepCopy()
    {
        return new StingWeapon();
    }

}

public class ShootingWeapon : WeaponBase
{
    private ObjectPool<Bullet> bulletPool;
    private Queue<Bullet> bulletQueue = new Queue<Bullet>();

    protected override void Initiailzed()
    {
        bulletPool = ObjectPoolManager.getInstance.GetPool<Bullet>();
    }

    public override void UpdateWeapon()
    {
        base.FindTarget();
        LookAtEnemyInRange();

        if (isAttack)
        {
            if (Shoot(dir.normalized))
            {
                isAttack = false;
                timer = 0;
            }
        }

    }

    protected bool Shoot(Vector2 _dir)
    {
        Bullet obj = bulletPool.Dequeue();
        bulletQueue.Enqueue(obj);
        obj.SetPosition(parent.position);
        obj.SetTarget(MonsterManager.getInstance.GetTargetArr());
        obj.SetDamage(weaponItemInfo.damage);
        obj.SetPenetrateCount(weaponItemInfo.penetrateCount);
        obj.SetDirection(_dir);
        obj.SetSpeed(10);
        obj.OnDequeue();

        return true;
    }

    public override WeaponBase DeepCopy()
    {
        return new ShootingWeapon();
    }
}

public class MowWeapon : WeaponBase
{
    public override WeaponBase DeepCopy()
    {
        return new MowWeapon();
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


