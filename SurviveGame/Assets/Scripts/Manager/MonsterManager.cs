using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : Singleton<MonsterManager>
{
    private BehaviorLogicBase[] behaviorLogicArr;
    private MonsterBehavior[] monsterAttackBehaviorArr;
    private MonsterBehavior[] monsterMoveBehaviorArr;

    private ObjectPool<MonsterController> monsterPool;
    private Queue<MonsterController> monsterQueue = new Queue<MonsterController>();
    private Transform MonsterModel;


    public override bool Initialize()
    {
        if(monsterPool == null)
        {
            monsterPool = ObjectPoolManager.getInstance.GetPool<MonsterController>(10);
            MonsterModel = Resources.Load<Transform>("Prefabs/Monster");
            monsterPool.SetModel(MonsterModel);
        }
        BindMonsterBehaviorInstance();


        //CreateMonster();

        return base.Initialize();
    }

    private void BindMonsterBehaviorInstance()
    {
        Shooting shooting = new Shooting();
        Rush rush = new Rush();

        monsterAttackBehaviorArr = new MonsterBehavior[(int)MonsterAttackBehaviorType.End]
        {
            shooting,
            rush,
            null
        };

        ApproachToTarget approachToTarget = new ApproachToTarget();
        RunAwayFromTarget runAwayFromTarget = new RunAwayFromTarget();

        monsterMoveBehaviorArr = new MonsterBehavior[(int)MonsterMoveBehaviorType.End]
        {
            approachToTarget,
            runAwayFromTarget
        };

        SeqenceBehavior seqenceBehavior = new SeqenceBehavior();
        LoopBehavior loopBehavior = new LoopBehavior();

        behaviorLogicArr = new BehaviorLogicBase[(int)BehaviorLogicType.End]
        {
            seqenceBehavior,
            loopBehavior
        };
    }

    private void CreateMonster()
    {
        SeqenceBehavior aa = new SeqenceBehavior();
        aa.Initialize(monsterMoveBehaviorArr[0]);
        aa.Update();
    }
}
