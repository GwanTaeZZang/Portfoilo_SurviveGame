using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterData
{
    public MonsterInfo[] monsterArr;
}

[System.Serializable]
public class MonsterInfo
{
    public int Uid;
    public int hp;
    public float damage;
    public float speed;
    public float attackSpeed;
    public float attackRange;
    public string stringKey;
    public string monsterSpritePath;
    public string monsterName;
    public BehaviorLogicType logicType;
    public MonsterMoveBehaviorType moveType;
    public MonsterAttackBehaviorType attackType;
}

public class BehaviorLogicBase
{
    protected MonsterInfo info;
    protected MonsterBehavior moveBehavior;
    protected MonsterBehavior attackBehavior;
    protected Transform target;
    protected Transform monster;

    public virtual void Update()
    {

    }

    public virtual BehaviorLogicBase DeepCopy()
    {
        return new BehaviorLogicBase();
    }

    public virtual void Initialize(MonsterInfo _info, Transform _monster ,MonsterBehavior _move, MonsterBehavior _attack = null)
    {
        info = _info;
        moveBehavior = _move;
        attackBehavior = _attack;

        monster = _monster;
        target = PlayerManager.getInstance.GetPlayer();

        moveBehavior.Initialize(_info, target, monster);
        attackBehavior?.Initialize(_info, target, monster);
    }
}

public class SeqenceBehavior : BehaviorLogicBase
{ 
    //public SeqenceBehavior(params MonsterBehavior[] _monsterBehaviorArr)
    //{

    //}

    //public override void Initialize(Transform _monster, MonsterBehavior _move, MonsterBehavior _attack = null)
    //{
    //    moveBehavior = _move;
    //    attackBehavior = _attack;

    //    monster = _monster;
    //    target = PlayerManager.getInstance.GetPlayer();

    //    moveBehavior.Initialize(target, monster);
    //    attackBehavior?.Initialize(target, monster);

    //}


    public override void Update()
    {
        moveBehavior.Update();
        attackBehavior?.Update();
    }

    public override BehaviorLogicBase DeepCopy()
    {
        return new SeqenceBehavior();
    }
}

public class LoopBehavior : BehaviorLogicBase
{
    //public LoopBehavior(params MonsterBehavior[] _monsterBehaviorArr)
    //{

    //}
    //public override void Initialize(Transform _monster, MonsterBehavior _move, MonsterBehavior _attack = null)
    //{
    //    moveBehavior = _move;
    //    attackBehavior = _attack;

    //    monster = _monster;
    //    target = PlayerManager.getInstance.GetPlayer();

    //    moveBehavior.Initialize(target , monster);
    //    attackBehavior?.Initialize(target, monster);

    //}


    public override void Update()
    {
        moveBehavior.Update();
        attackBehavior?.Update();
    }

    public override BehaviorLogicBase DeepCopy()
    {
        return new LoopBehavior();
    }

}


public enum BehaviorLogicType
{
    SeqenceBehavior,
    LoopBehavior,
    End,
}

public enum MonsterMoveBehaviorType
{
    ApproachToTarget,
    RunAwayFromTarget,
    End,
}

public enum MonsterAttackBehaviorType
{
    Shooting,
    Rush,
    None,
    End,
}

public class MonsterBehavior
{
    protected MonsterInfo info;
    protected Transform target;
    protected Transform monster;


    public virtual void Update()
    {

    }

    public virtual void Initialize(MonsterInfo _info ,Transform _target, Transform _monster)
    {
        info = _info;
        target = _target;
        monster = _monster;
    }

    public virtual MonsterBehavior DeepCopy()
    {
        return new MonsterBehavior();
    }
}

public class Shooting : MonsterBehavior
{
    public override void Update()
    {

    }

    public override MonsterBehavior DeepCopy()
    {
        return new Shooting();
    }
}

public class Rush : MonsterBehavior
{
    public override void Update()
    {
        Show();
    }
    public void Show()
    {
        Debug.Log("Attack Rush");
    }

    public override MonsterBehavior DeepCopy()
    {
        return new Rush();
    }


}

public class ApproachToTarget : MonsterBehavior
{
    public override void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 monsterPos = monster.position;
        Vector2 targetPos = target.position;

        Vector2 dir = targetPos - monsterPos;

        monsterPos.x += dir.normalized.x * Time.deltaTime * info.speed;
        monsterPos.y += dir.normalized.y * Time.deltaTime * info.speed;

        monster.position = monsterPos;
    }


    public override MonsterBehavior DeepCopy()
    {
        return new ApproachToTarget();
    }


}

public class RunAwayFromTarget : MonsterBehavior
{
    public override void Update()
    {
        Move();
    }

    private void Move()
    {

        Vector2 monsterPos = monster.position;
        Vector2 targetPos = target.position;

        Vector2 dir = targetPos - monsterPos;
        float distance = Vector2.Distance(monsterPos, targetPos);

        if(distance > info.attackRange)
        {
            monsterPos.x += dir.normalized.x * Time.deltaTime * info.speed;
            monsterPos.y += dir.normalized.y * Time.deltaTime * info.speed;

            monster.position = monsterPos;
        }

        if(distance < info.attackRange * 0.8f)
        {
            monsterPos.x -= dir.normalized.x * Time.deltaTime * info.speed;
            monsterPos.y -= dir.normalized.y * Time.deltaTime * info.speed;

            monster.position = monsterPos;

        }
    }


    public override MonsterBehavior DeepCopy()
    {
        return new RunAwayFromTarget();
    }

}
