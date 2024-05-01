using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : Singleton<MonsterManager>
{
    private BehaviorLogicBase[] behaviorLogicArr;
    private MonsterBehavior[] monsterAttackBehaviorArr;
    private MonsterBehavior[] monsterMoveBehaviorArr;

    private List<MonsterInfo> monsterInfoList = new List<MonsterInfo>();
    private Dictionary<int, MonsterInfo> monsterInfoDict = new Dictionary<int, MonsterInfo>();

    private ObjectPool<MonsterController> monsterPool;
    //private Queue<MonsterController> monsterQueue = new Queue<MonsterController>();
    private Transform MonsterModel;


    public override bool Initialize()
    {
        LoadMonsterData();

        BindMonsterBehaviorInstance();

        CreateMonsterPool();

        //CreateMonster();

        return base.Initialize();
    }

    public void DequeueMonster(int _count, int _uid)
    {
        for(int i = 0; i < _count; i++)
        {
            MonsterController monsterCtrl =  monsterPool.Dequeue();
            monsterCtrl.OnDequeue();

            monsterInfoDict.TryGetValue(_uid, out MonsterInfo value);
            monsterCtrl.SetMonsterInfo(value);

            BehaviorLogicBase logic = behaviorLogicArr[(int)value.logicType].DeepCopy();
            MonsterBehavior moveBehavior = monsterMoveBehaviorArr[(int)value.moveType].DeepCopy();

            MonsterBehavior attackBehavior;
            if (value.attackType != MonsterAttackBehaviorType.None)
            {
                attackBehavior = monsterAttackBehaviorArr[(int)value.attackType].DeepCopy();
            }
            attackBehavior = null;
            logic.Initialize(moveBehavior, attackBehavior);
        }
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

    private void CreateMonsterPool()
    {
        GameObject poolParent = new GameObject();
        poolParent.name = "Obejct Pool";

        if (monsterPool == null)
        {
            monsterPool = ObjectPoolManager.getInstance.GetPool<MonsterController>(10);
            MonsterModel = Resources.Load<Transform>("Prefabs/Monster");
            monsterPool.SetModel(MonsterModel, poolParent.transform);
        }
    }

    private void CreateMonster()
    {
        SeqenceBehavior aa = new SeqenceBehavior();
        aa.Initialize(monsterMoveBehaviorArr[0]);
        aa.Update();
    }

    private void LoadMonsterData()
    {
        MonsterData monsterData = JsonController.ReadJson<MonsterData>("MonsterData");
        int count = monsterData.monsterArr.Length;

        for(int i =0; i < count; i++)
        {
            MonsterInfo info = monsterData.monsterArr[i];
            monsterInfoList.Add(info);

            int uid = info.Uid;
            monsterInfoDict.Add(uid, info);
        }
    }
}
