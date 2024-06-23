using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashToTarget : BossMoveBehaviour
{
    private float waitingTime = 2f;
    private float timer;
    private bool isDash = false;
    private bool isInRange = false;
    private bool isEndSeqence = false;

    private Vector2 targetPos;

    public override void Initialize()
    {
        isDash = false;
        isInRange = false;
        isEndSeqence = false;

        timer = 0f;
    }

    public override bool Update()
    {
        if (!isInRange)
        {
            if (MoveToTargetInDashRange())
            {
                isInRange = true;
            }
        }
        else if (isInRange)
        {
            if (Charge() && !isDash)
            {
                SetTarget();
                isDash = true;
            }
        }

        if (isDash)
        {
            Dash();
        }

        return isEndSeqence;
    }

    private void SetTarget()
    {
        direction = playerTransform.position - bossTransform.position;
        targetPos = playerTransform.position;
    }

    private bool Charge()
    {
        timer += Time.deltaTime;

        if(timer > waitingTime)
        {
            return true;
        }
        return false;
    }

    private void Dash()
    {
        bossPos = bossTransform.position;

        bossPos.x += Time.deltaTime * direction.normalized.x * (model.speed * 3);
        bossPos.y += Time.deltaTime * direction.normalized.y * (model.speed * 3);

        bossTransform.position = bossPos;

        float curDistanceToTarget = Vector2.Distance(targetPos, bossTransform.position);
        if(curDistanceToTarget < 0.1f)
        {
            isDash = false;
            isEndSeqence = true;
        }
    }

    private bool MoveToTargetInDashRange()
    {
        direction = playerTransform.position - bossTransform.position;

        bossPos = bossTransform.position;
        bossPos.x += Time.deltaTime * direction.normalized.x * model.speed;
        bossPos.y += Time.deltaTime * direction.normalized.y * model.speed;
        bossTransform.position = bossPos;

        distance = Vector2.Distance(bossTransform.position, playerTransform.position);
        if (distance < 5f)
        {
            return true;
        }
        return false;
    }

    public override void SetBossMonsterModel(BossMonsterModel _model)
    {
        model = _model;
    }
    public override void SetTransform(Transform _bossTransform)
    {
        bossTransform = _bossTransform;
        playerTransform = PlayerManager.getInstance.GetPlayer().transform;
    }
}

public class BossApproachToTarget : BossMoveBehaviour
{
    private float approachTime = 5f;
    private float timer = 0;

    private bool isEndSeqence = false;

    public override void Initialize()
    {
        isEndSeqence = false;
        base.Initialize();
    }

    public override bool Update()
    {
        timer += Time.deltaTime;
        if(timer > approachTime)
        {
            isEndSeqence = true;
        }
        else
        {
            MoveToTarget();
        }


        return isEndSeqence;
    }

    private void MoveToTarget()
    {
        bossPos = bossTransform.position;
        Vector2 targetPos = playerTransform.position;

        Vector2 dir = targetPos - bossPos;

        bossPos.x += dir.normalized.x * Time.deltaTime * model.speed;
        bossPos.y += dir.normalized.y * Time.deltaTime * model.speed;

        bossTransform.position = bossPos;

    }

    public override void SetBossMonsterModel(BossMonsterModel _model)
    {
        model = _model;
    }

    public override void SetTransform(Transform _bossTransform)
    {
        bossTransform = _bossTransform;
        playerTransform = PlayerManager.getInstance.GetPlayer().transform;
    }

}

public class RandomDash : BossMoveBehaviour
{

    public override bool Update()
    {
        throw new System.NotImplementedException();
    }

    public override void SetBossMonsterModel(BossMonsterModel _model)
    {
        throw new System.NotImplementedException();
    }

    public override void SetTransform(Transform _bossTransform)
    {
        throw new System.NotImplementedException();
    }

}

public class BackRush : BossMoveBehaviour
{
    public override bool Update()
    {
        return false;
    }

    public override void SetBossMonsterModel(BossMonsterModel _model)
    {
        model = _model;
    }
    public override void SetTransform(Transform _bossTransform)
    {
        bossTransform = _bossTransform;
        playerTransform = PlayerManager.getInstance.GetPlayer().transform;
    }

}








public class HexagonShoot : BossAttackBehaviour
{
    private ObjectPool<Bullet> bulletPool;
    private Transform playerTransfom;
    //private Vector2[] directionArr;
    //private float angleStep;
    private Vector2 direction;


    private bool isEndShoot = false;
    private bool isEndSeqence = false;

    private float behaviourWaitTime = 2f;
    private float timer;

    public override void Initialize()
    {
        bulletPool = ObjectPoolManager.getInstance.GetPool<Bullet>();
        //directionArr = new Vector2[6];
        //angleStep = 360f / 6f;

        isEndSeqence = false;
        timer = 0f;

        //SetSixDirection();
    }

    public override bool Update()
    {
        if (isEndShoot)
        {
            timer += Time.deltaTime;
            if(behaviourWaitTime < timer)
            {
                isEndSeqence = true;
                isEndShoot = false;
            }
        }
        else
        {
            Shoot();
        }

        return isEndSeqence;
    }

    //private void SetSixDirection()
    //{
    //    int count = directionArr.Length;
    //    for(int i =0; i < count; i++)
    //    {
    //        float angle = i * angleStep;
    //        directionArr[i] = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
    //    }
    //}

    //private void Shoot()
    //{
    //    int count = directionArr.Length;
    //    for(int i =0; i < count; i++)
    //    {
    //        Bullet obj = bulletPool.Dequeue();
    //        //bulletQueue.Enqueue(obj);
    //        obj.SetPosition(bossTransform.position);
    //        obj.SetTarget(PlayerManager.getInstance.GetTarget());
    //        obj.SetDamage(model.damage);
    //        obj.SetDirection(directionArr[i].normalized);
    //        obj.SetSpeed(3);
    //        obj.OnDequeue();
    //    }

    //    isEndShoot = true;
    //}

    private void Shoot()
    {
        float divideDegree = 360 / 6;

        direction = (playerTransfom.position - bossTransform.position).normalized;

        float firstAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        float angle = firstAngle;

        for (int i = 0; i < 6; i++)
        {
            Vector2 projectileDirection;

            projectileDirection.x = Mathf.Cos(angle * Mathf.Deg2Rad);
            projectileDirection.y = Mathf.Sin(angle * Mathf.Deg2Rad);

            Bullet obj = bulletPool.Dequeue();
            //bulletQueue.Enqueue(obj);
            obj.SetPosition(bossTransform.position);
            obj.SetTarget(PlayerManager.getInstance.GetTarget());
            obj.SetDamage(model.damage);
            obj.SetDirection(projectileDirection);
            obj.SetSpeed(3);
            obj.OnDequeue();

            angle += divideDegree;
        }

        isEndShoot = true;
    }

    public override void SetBossMonsterModel(BossMonsterModel _model)
    {
        model = _model;
    }

    public override void SetTransform(Transform _bossTransform)
    {
        bossTransform = _bossTransform;
        playerTransfom = PlayerManager.getInstance.GetPlayer().transform;
    }
}

public class ContinuousHexagonShoot : BossAttackBehaviour
{
    private ObjectPool<Bullet> bulletPool;
    private Transform playerTransfom;
    //private Vector2[] directionArr;
    //private float angleStep;
    private int continuousCount;

    private bool isEndShoot = false;
    private bool isEndSeqence = false;

    private float behaviourWaitTime = 2f;
    private float shootIntervalTime = 0.5f;
    private float timer;

    private Vector2 direction;

    public override bool Update()
    {
        if (isEndShoot)
        {
            timer += Time.deltaTime;
            if (behaviourWaitTime < timer)
            {
                isEndSeqence = true;
                isEndShoot = false;
            }
        }
        else if(continuousCount != 0)
        {
            timer += Time.deltaTime;
            if(timer > shootIntervalTime)
            {
                Shoot(continuousCount);
                timer = 0;
            }
        }
        else
        {
            isEndShoot = true;
            timer = 0;
        }

        return isEndSeqence;
    }

    public override void Initialize()
    {
        bulletPool = ObjectPoolManager.getInstance.GetPool<Bullet>();
        //directionArr = new Vector2[6];
        //angleStep = 360f / 6f;

        isEndSeqence = false;
        timer = 0f;
        continuousCount = 5;

        //SetSixDirection();

    }

    //private void Shoot()
    //{
    //    int count = directionArr.Length;
    //    for (int i = 0; i < count; i++)
    //    {
    //        Bullet obj = bulletPool.Dequeue();
    //        //bulletQueue.Enqueue(obj);
    //        obj.SetPosition(bossTransform.position);
    //        obj.SetTarget(PlayerManager.getInstance.GetTarget());
    //        obj.SetDamage(model.damage);
    //        obj.SetDirection(directionArr[i].normalized);
    //        obj.SetSpeed(3);
    //        obj.OnDequeue();
    //    }

    //    isEndShoot = true;
    //}

    //private void SetSixDirection()
    //{
    //    int count = directionArr.Length;
    //    for (int i = 0; i < count; i++)
    //    {
    //        float angle = i * angleStep;
    //        directionArr[i] = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
    //    }
    //}

    public void Shoot(float _angleOffset)
    {
        float divideDegree = 360 / 6;

        //direction = (playerTransfom.position - bossTransform.position).normalized;

        //float firstAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        //float angle = firstAngle + _angleOffset;

        float angle = _angleOffset * 20;

        for (int i = 0; i < 6; i++)
        {
            Vector2 projectileDirection;

            projectileDirection.x = Mathf.Cos(angle * Mathf.Deg2Rad);
            projectileDirection.y = Mathf.Sin(angle * Mathf.Deg2Rad);

            Bullet obj = bulletPool.Dequeue();
            //bulletQueue.Enqueue(obj);
            obj.SetPosition(bossTransform.position);
            obj.SetTarget(PlayerManager.getInstance.GetTarget());
            obj.SetDamage(model.damage);
            obj.SetDirection(projectileDirection);
            obj.SetSpeed(3);
            obj.OnDequeue();

            angle += divideDegree;
        }
        continuousCount--;
    }

    public override void SetBossMonsterModel(BossMonsterModel _model)
    {
        model = _model;
    }

    public override void SetTransform(Transform _bossTransform)
    {
        bossTransform = _bossTransform;
        playerTransfom = PlayerManager.getInstance.GetPlayer().transform;
    }

}

public class TurningShoot : BossAttackBehaviour
{
    private ObjectPool<Bullet> bulletPool;


    public override bool Update()
    {
        return false;
    }

    public override void Initialize()
    {
        bulletPool = ObjectPoolManager.getInstance.GetPool<Bullet>();
    }

    public override void SetBossMonsterModel(BossMonsterModel _model)
    {
        model = _model;
    }

    public override void SetTransform(Transform _bossTransform)
    {
        bossTransform = _bossTransform;
    }

}
