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
    public WeaponItemInfo weaponItemInfo;
    public Sprite weaponSprite;
    public Transform weapon;

    protected Transform target;
    protected float timer;

    protected Vector2 oriWeaponPos;
    protected Vector2 targetPos;
    protected Vector2 dir;
    protected float distance;
    protected bool isAttack = false;
    protected bool attackCompelet = false;


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
        Initiailzed();
    }
    public void SetWeapon(Transform _weapon)
    {
        weapon = _weapon;
        oriWeaponPos = _weapon.localPosition;
    }
}

//public abstract class EquipItem
//{
//    public int price;
//    public int uid;
//    public List<StatusEffect> buff;
//    public List<StatusEffect> deBuff;
//}

//public class SetEffect
//{

//}

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
        FindTarget();
        LookAtEnemyInRange();

    }


    private void FindTarget()
    {
        float compareDistance = 100f;

        LinkedList<MonsterController> targetList = MonsterManager.getInstance.GetMonsterList();

        foreach (MonsterController monster in targetList)
        {
            targetPos = monster.transform.position;

            float distance = Vector2.Distance(weapon.position, targetPos);

            if (compareDistance > distance)
            {
                compareDistance = distance;
                targetPos = monster.transform.position;
            }

        }



    }

    protected bool Attack(Vector2 _dir)
    {

        Vector2 curWeaponPos = weapon.localPosition;

        if (isGo)
        {
            curWeaponPos.x += _dir.x * Time.deltaTime * 15;
            curWeaponPos.y += _dir.y * Time.deltaTime * 15;
            weapon.localPosition = curWeaponPos;

            float distance = Vector2.Distance(curWeaponPos, oriWeaponPos);
            if (distance >= weaponItemInfo.attackRange)
            {
                isGo = false;
            }
        }

        else if (!isGo)
        {
            curWeaponPos.x -= _dir.x * Time.deltaTime * 15;
            curWeaponPos.y -= _dir.y * Time.deltaTime * 15;
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
        //targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector2.Distance(weapon.position, targetPos);

        if (!isAttack)
        {
            dir = targetPos - (Vector2)weapon.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            weapon.localRotation = Quaternion.Euler(0, 0, angle);
        }

        timer += Time.deltaTime;

        if (distance < weaponItemInfo.attackRange)
        {
            if (timer > weaponItemInfo.attackSpeed)
            {
                isAttack = true;

                if (Attack(dir.normalized))
                {
                    isAttack = false;
                    timer = 0;
                    attackCompelet = false;
                }
            }
        }

        //if (isAttack)
        //{
        //    attackCompelet = Attack(dir.normalized);
        //}
    }
}

public class ShootingWeapon : WeaponBase
{
    private ObjectPool<Bullet> bulletPool;
    private Queue<Bullet> bulletQueue = new Queue<Bullet>();
    private GameObject parent;

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

        parent = new GameObject();
        parent.name = "BulletPoolParent";
        bulletPool = ObjectPoolManager.getInstance.GetPool<Bullet>(10);
        bulletPool.SetModel(Resources.Load<Transform>("Prefabs/Bullet_3"), parent.transform);
    }

    public override void UpdateWeapon()
    {
        FindTarget();
        LookAtEnemyInRange();
    }

    private void FindTarget()
    {
        float compareDistance = 100f;

        LinkedList<MonsterController> targetList = MonsterManager.getInstance.GetMonsterList();

        foreach(MonsterController monster in targetList)
        {
            targetPos = monster.transform.position;

            float distance = Vector2.Distance(weapon.position, targetPos);

            if(compareDistance > distance)
            {
                compareDistance = distance;
                targetPos = monster.transform.position;
            }

        }



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
        //targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector2.Distance(weapon.position, targetPos);

        if (!isAttack)
        {
            dir = targetPos - (Vector2)weapon.position;
            //Vector2 v2 = targetPos - oriWeaponPos;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            weapon.localRotation = Quaternion.Euler(0, 0, angle);
        }

        if (distance < weaponItemInfo.attackRange)
        {
            Debug.Log("? ?? ?? ??? ");
            timer += Time.deltaTime;
            if (timer > weaponItemInfo.attackSpeed)
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


