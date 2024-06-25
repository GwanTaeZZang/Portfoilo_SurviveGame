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
        timer = 0;

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

    private bool isEndShoot = false;
    private bool isEndSeqence = false;

    private float behaviourWaitTime = 2f;
    private float timer;

    public override void Initialize()
    {
        bulletPool = ObjectPoolManager.getInstance.GetPool<Bullet>();
        isEndSeqence = false;
        timer = 0f;
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

    private void Shoot()
    {
        float divideDegree = 360 / 6;

        float angle = 0f;

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
    }
}

public class ContinuousHexagonShoot : BossAttackBehaviour
{
    private ObjectPool<Bullet> bulletPool;
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

        isEndSeqence = false;
        timer = 0f;
        continuousCount = 5;

    }

    public void Shoot(float _angleOffset)
    {
        float divideDegree = 360 / 6;

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
    }

}

public class TurningShoot : BossAttackBehaviour
{
    private ObjectPool<Bullet> bulletPool;

    private bool isEndShoot = false;
    private bool isEndSeqence = false;

    private float behaviourWaitTime = 2f;
    private float shootIntervalTime = 0.2f;
    private float shootIntervalAngle = 20f;
    private float timer;
    private float angle;


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
        else if (!isEndSeqence)
        {
            timer += Time.deltaTime;
            if (timer > shootIntervalTime)
            {
                Shoot(shootIntervalAngle);
                timer = 0;
            }
        }

        return isEndSeqence;
    }

    public override void Initialize()
    {
        bulletPool = ObjectPoolManager.getInstance.GetPool<Bullet>();
        isEndSeqence = false;
        timer = 0f;
        angle = 0;

    }
    public void Shoot(float _angleOffset)
    {
         angle += _angleOffset;

        if(angle < 720f)
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


            projectileDirection.x = Mathf.Cos((angle + 180) * Mathf.Deg2Rad);
            projectileDirection.y = Mathf.Sin((angle + 180) * Mathf.Deg2Rad);

            Bullet obj2 = bulletPool.Dequeue();
            //bulletQueue.Enqueue(obj);
            obj2.SetPosition(bossTransform.position);
            obj2.SetTarget(PlayerManager.getInstance.GetTarget());
            obj2.SetDamage(model.damage);
            obj2.SetDirection(projectileDirection);
            obj2.SetSpeed(3);
            obj2.OnDequeue();

        }
        else
        {
            isEndSeqence = true;
        }
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

public class RampageShoot : BossAttackBehaviour
{
    private ObjectPool<Bullet> bulletPool;

    private bool isEndShoot = false;
    private bool isEndSeqence = false;

    private float behaviourWaitTime = 2f;
    private float shootIntervalTime = 0.2f;
    private float shootIntervalAngle = 20f;
    private float timer;
    private float angle;

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
        else if (!isEndSeqence)
        {
            timer += Time.deltaTime;
            if (timer > shootIntervalTime)
            {
                Shoot(Random.Range(10f, 20f));
                timer = 0;
            }
        }

        return isEndSeqence;
    }

    public override void Initialize()
    {
        bulletPool = ObjectPoolManager.getInstance.GetPool<Bullet>();
        isEndSeqence = false;
        timer = 0f;
        angle = 0;

    }
    public void Shoot(float _angleOffset)
    {
        angle += _angleOffset;

        if (angle < 720f)
        {
            Vector2 projectileDirection;

            projectileDirection.x = Mathf.Cos(angle + 180 * Mathf.Deg2Rad);
            projectileDirection.y = Mathf.Sin(angle + 180 * Mathf.Deg2Rad);

            Bullet obj = bulletPool.Dequeue();
            //bulletQueue.Enqueue(obj);
            obj.SetPosition(bossTransform.position);
            obj.SetTarget(PlayerManager.getInstance.GetTarget());
            obj.SetDamage(model.damage);
            obj.SetDirection(projectileDirection);
            obj.SetSpeed(3);
            obj.OnDequeue();


            projectileDirection.x = Mathf.Cos(angle + 360 * Mathf.Deg2Rad);
            projectileDirection.y = Mathf.Sin(angle + 360 * Mathf.Deg2Rad);

            Bullet obj2 = bulletPool.Dequeue();
            //bulletQueue.Enqueue(obj);
            obj2.SetPosition(bossTransform.position);
            obj2.SetTarget(PlayerManager.getInstance.GetTarget());
            obj2.SetDamage(model.damage);
            obj2.SetDirection(projectileDirection);
            obj2.SetSpeed(3);
            obj2.OnDequeue();

        }
        else
        {
            isEndSeqence = true;
        }
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

