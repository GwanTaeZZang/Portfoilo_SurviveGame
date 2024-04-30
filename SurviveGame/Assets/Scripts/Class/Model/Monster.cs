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
    protected MonsterBehavior moveBehavior;
    protected MonsterBehavior attackBehavior;

}

public class SeqenceBehavior : BehaviorLogicBase
{ 
    //public SeqenceBehavior(params MonsterBehavior[] _monsterBehaviorArr)
    //{

    //}

    public void Initialize(MonsterBehavior _move, MonsterBehavior _attack = null)
    {
        moveBehavior = _move;
        attackBehavior = _attack;
    }


    public void Update()
    {
        moveBehavior.Update();
        attackBehavior?.Update();
    }

    public SeqenceBehavior DeepCopy()
    {
        return new SeqenceBehavior();
    }
}

public class LoopBehavior : BehaviorLogicBase
{
    //public LoopBehavior(params MonsterBehavior[] _monsterBehaviorArr)
    //{

    //}
    public void Initialize(MonsterBehavior _move, MonsterBehavior _attack = null)
    {
        moveBehavior = _move;
        attackBehavior = _attack;
    }


    public void Update()
    {
        moveBehavior.Update();
        attackBehavior?.Update();
    }

    public LoopBehavior DeepCopy()
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
    public virtual void Update()
    {
        Debug.Log("���� ���̽�");
    }
}

public class Shooting : MonsterBehavior
{
    public override void Update()
    {
        base.Update();
        Show();
    }
    public void Show()
    {
        Debug.Log("�����ൿ : �߻�");
    }
}

public class Rush : MonsterBehavior
{
    public override void Update()
    {
        base.Update();
        Show();
    }
    public void Show()
    {
        Debug.Log("�����ൿ : ����");
    }

}

public class ApproachToTarget : MonsterBehavior
{
    public override void Update()
    {
        base.Update();
        Show();
    }
    public void Show()
    {
        Debug.Log("�̵��ൿ : ����");
    }

}

public class RunAwayFromTarget : MonsterBehavior
{
    public override void Update()
    {
        base.Update();
        Show();
    }
    public void Show()
    {
        Debug.Log("�̵��ൿ : ����");
    }

}
