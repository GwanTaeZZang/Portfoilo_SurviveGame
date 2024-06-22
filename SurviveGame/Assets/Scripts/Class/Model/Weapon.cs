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
    //public int Uid;
    public int level;
    //public int price;
    public int penetrateCount;
    public float damage;
    public float damageRate;
    public float attackSpeed;
    public float attackRange;
    public string stringKey;
    //public string itemSpritePath;
    //public string itemName;
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
    protected Transform target = null;
    protected float timer;

    protected Vector2 oriWeaponPos;
    //protected Vector2 targetPos;
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
        distance = Vector2.Distance(weapon.position, target.position);

        //if (!isAttack)
        //{
        //    dir = (Vector2)target.position - (Vector2)parent.position;
        //    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //    //weapon.localRotation = Quaternion.Euler(0, 0, angle);
        //    parent.rotation = Quaternion.Euler(0, 0, angle);
        //}

        dir = (Vector2)target.position - (Vector2)parent.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //weapon.localRotation = Quaternion.Euler(0, 0, angle);
        parent.rotation = Quaternion.Euler(0, 0, angle);


        timer += Time.deltaTime;

        if (distance < weaponItemInfo.attackRange || isAttack)
        {
            if (timer > weaponItemInfo.attackSpeed)
            {
                isAttack = true;
                OnAttackEvent?.Invoke(isAttack);
                //if (Attack())
                //{
                //    isAttack = false;
                //    OnAttackEvent?.Invoke(isAttack);
                //    timer = 0;
                //}
            }
        }
    }


    protected virtual void FindTarget()
    {
        float compareDistance = float.MaxValue;

        LinkedList<MonsterController> targetList = MonsterManager.getInstance.GetMonsterList();

        if(targetList.Count == 0)
        {
            target = null;
        }

        foreach (MonsterController monster in targetList)
        {
            if (monster.IsCollision())
            {
                float distance = Vector2.Distance(weapon.position, monster.transform.position);

                if (compareDistance > distance)
                {
                    compareDistance = distance;
                    target = monster.transform;
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
            //curWeaponPos.x += _dir.x * Time.deltaTime * ATTACK_SPEED;
            //curWeaponPos.y += _dir.y * Time.deltaTime * ATTACK_SPEED;

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
            //curWeaponPos.x -= _dir.x * Time.deltaTime * ATTACK_SPEED;
            //curWeaponPos.y -= _dir.y * Time.deltaTime * ATTACK_SPEED;

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
        obj.OnDequeue();

        return true;
    }

    //protected override void LookAtEnemyInRange()
    //{
    //    if (target == null)
    //    {
    //        return;
    //    }

    //    distance = Vector2.Distance(weapon.position, target.position);

    //    if (!isAttack)
    //    {
    //        dir = (Vector2)target.position - (Vector2)weapon.localPosition;
    //        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    //        //weapon.localRotation = Quaternion.Euler(0, 0, angle);
    //        parent.rotation = Quaternion.Euler(0, 0, angle);
    //    }

    //    if (distance < weaponItemInfo.attackRange || isAttack)
    //    {
    //        timer += Time.deltaTime;
    //        if (timer > weaponItemInfo.attackSpeed)
    //        {
    //            isAttack = true;

    //            if (Shoot(dir.normalized))
    //            {
    //                isAttack = false;
    //                timer = 0;
    //            }
    //        }
    //    }

    //}

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


