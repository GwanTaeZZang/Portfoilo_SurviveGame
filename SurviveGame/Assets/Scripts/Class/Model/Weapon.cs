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

    protected Vector2 oriWeaponPos;
    protected Vector2 targetPos;
    protected Vector2 dir;
    protected float distance;
    protected bool isAttack = false;


    public abstract void UpdateWeapon();
    protected abstract void LookAtEnemyInRange();
    public abstract WeaponBase DeepCopy();

    protected virtual void Initiailzed()
    {

    }

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
        Initiailzed();
    }
    public void SetWeapon(Transform _weapon)
    {
        weapon = _weapon;
        oriWeaponPos = _weapon.position;
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
    ShoootingWeapon,
    End,
}


public class StingWeapon : WeaponBase
{
    protected bool isGo = true;
    //protected bool isSetAttackVector = false;

    //public StingWeapon(Transform _weapon)
    //{
    //    weapon = _weapon;
    //    oriWeaponPos = _weapon.position;
    //}

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

    public override WeaponBase DeepCopy()
    {
        return new StingWeapon();
    }

    protected override void LookAtEnemyInRange()
    {
        // ?????????? ???????? ?? ???? ????????

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

        // ?????? ????????
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
    private ObjectPool<Bullet> bulletPool;
    private Queue<Bullet> bulletQueue = new Queue<Bullet>();

    //public ShootingWeapon(Transform _weapon)
    //{
    //    weapon = _weapon;
    //    oriWeaponPos = _weapon.position;

    //    bulletPool = ObjectPoolManager.getInstance.GetPool<Bullet>(10);
    //    bulletPool.SetModel(Resources.Load<Transform>("Prefabs/Bullet_3"));
    //}

    protected override void Initiailzed()
    {
        base.Initiailzed();
        bulletPool = ObjectPoolManager.getInstance.GetPool<Bullet>(10);
        bulletPool.SetModel(Resources.Load<Transform>("Prefabs/Bullet_3"));
    }

    public override void UpdateWeapon()
    {
        LookAtEnemyInRange();
    }

    protected bool Shoot(Vector2 _dir)
    {
        Bullet obj = bulletPool.Dequeue();
        bulletQueue.Enqueue(obj);
        obj.SetPosition(weapon.position);
        obj.SetDirection(_dir);
        obj.OnDequeue();
        //float angle = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;
        //obj.model.rotation = Quaternion.Euler(0, 0, angle);

        //Vector2 bulletPos = obj.model.position;
        //bulletPos.x += _dir.x * Time.deltaTime * 10;
        //bulletPos.y += _dir.y * Time.deltaTime * 10;
        //obj.model.position = bulletPos;

        return true;
    }

    protected override void LookAtEnemyInRange()
    {
        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector2.Distance(oriWeaponPos, targetPos);

        if (!isAttack)
        {
            dir = targetPos - oriWeaponPos;
            //Vector2 v2 = targetPos - oriWeaponPos;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            weapon.localRotation = Quaternion.Euler(0, 0, angle);
        }

        if (distance < attackRange - 3)
        {
            Debug.Log("? ?? ?? ??? ");
            timer += Time.deltaTime;
            if (timer > attackSpeed)
            {
                isAttack = true;

                if (Shoot(dir.normalized))
                {
                    isAttack = false;
                    timer = 0;
                }
            }
        }

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

    //    public MowWeapon(Transform _weapon)
    //{
    //    weapon = _weapon;
    //}


    public override void UpdateWeapon()
    {
        throw new System.NotImplementedException();
    }

    protected override void LookAtEnemyInRange()
    {
        throw new System.NotImplementedException();
    }
}


