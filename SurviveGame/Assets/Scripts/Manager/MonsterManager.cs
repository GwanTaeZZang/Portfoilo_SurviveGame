using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : Singleton<MonsterManager>
{
    private const int CAPICITY = 100;

    private BehaviorLogicBase[] behaviorLogicArr;
    private MonsterBehavior[] monsterAttackBehaviorArr;
    private MonsterBehavior[] monsterMoveBehaviorArr;

    //private List<MonsterInfo> monsterInfoList = new List<MonsterInfo>();
    private Dictionary<int, MonsterInfo> monsterInfoDict = new Dictionary<int, MonsterInfo>();
    private Dictionary<int, Sprite> monsterSpriteDict = new Dictionary<int, Sprite>();

    private Dictionary<BehaviorLogicType, Queue<BehaviorLogicBase>> behaviorLogicDict = new Dictionary<BehaviorLogicType, Queue<BehaviorLogicBase>>();

    private Dictionary<MonsterMoveBehaviorType, Queue<MonsterBehavior>> moveBehaviorDict = new Dictionary<MonsterMoveBehaviorType, Queue<MonsterBehavior>>();

    private Dictionary<MonsterAttackBehaviorType, Queue<MonsterBehavior>> attackBehaviorDict = new Dictionary<MonsterAttackBehaviorType, Queue<MonsterBehavior>>();

    private BossMonsterController bossCtrl;

    private LinkedList<MonsterController> monsterCtrlList = new LinkedList<MonsterController>();
    private Queue<LinkedListNode<MonsterController>> deadMonsterQueue;

    private ITargetAble[] targetArr = new ITargetAble[101];

    private GameObject poolParent;
    //private MonsterInfo monsterInfoVariance;
    private MonsterStatusVariance monsterStatusVariance;


    public override bool Initialize()
    {
        LoadMonsterData();

        BindMonsterBehaviorInstance();

        InitBehviorDict();

        CreateMonster();

        CreateBossMonster();

        InitMonsterStatusVariance();

        return base.Initialize();
    }

    private void InitMonsterStatusVariance()
    {
        if(monsterStatusVariance == null)
        {
            monsterStatusVariance = new MonsterStatusVariance();
        }
        //int count = monsterInfoVariance.status.Length;
        //for(int i = 0; i < count; i++)
        //{
        //    monsterInfoVariance.status[i] = 0;
        //}
    }

    private void InitBehviorDict()
    {
        behaviorLogicDict[BehaviorLogicType.SeqenceBehavior] = new Queue<BehaviorLogicBase>();
        behaviorLogicDict[BehaviorLogicType.LoopBehavior] = new Queue<BehaviorLogicBase>();

        moveBehaviorDict[MonsterMoveBehaviorType.ApproachToTarget] = new Queue<MonsterBehavior>();
        moveBehaviorDict[MonsterMoveBehaviorType.RunAwayFromTarget] = new Queue<MonsterBehavior>();

        attackBehaviorDict[MonsterAttackBehaviorType.Shooting] = new Queue<MonsterBehavior>();
        attackBehaviorDict[MonsterAttackBehaviorType.Rush] = new Queue<MonsterBehavior>();
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

    public void UpdateMonsterStatus(MonsterStatusType _type, float _amount)
    {
        //monsterInfoVariance.status[(int)_type] += _amount;

        monsterStatusVariance.status[(int)_type] += _amount;


        //foreach (var dict in monsterInfoDict)
        //{
        //    dict.Value.status[(int)_type] += _amount;
        //}

        Debug.Log(_type + " status is updata  : " + _amount);
    }

    public MonsterStatusVariance GetMonsterStatusVariance()
    {
        return monsterStatusVariance;
    }

    public LinkedList<MonsterController> GetMonsterList()
    {
        return monsterCtrlList;
    }

    public ITargetAble[] GetTargetArr()
    {
        return targetArr;
    }

    public MonsterController GetMonster()
    {
        MonsterController monsterCtrl;
        LinkedListNode<MonsterController> node;

        if (monsterCtrlList.Count >= CAPICITY)
        {
            node = monsterCtrlList.First;
            monsterCtrlList.RemoveFirst();
        }
        else
        {
            node = deadMonsterQueue.Dequeue();
        }

        monsterCtrl = node.Value;

        //node.Value = monsterCtrl;
        monsterCtrlList.AddLast(node);

        return monsterCtrl;

        //return randomPos;
    }

    public BossMonsterController GetBossMonster()
    {
        return bossCtrl;
    }

    public void RemoveMonsterList(MonsterController _deadMonster)
    {
        LinkedListNode<MonsterController> node;

        node = monsterCtrlList.Find(_deadMonster);
        node.Value.transform.SetParent(poolParent.transform);
        deadMonsterQueue.Enqueue(node);
        monsterCtrlList.Remove(node);

    }

    public void EndWave()
    {

        int count = monsterCtrlList.Count;
        for(int i =0; i < count; i++)
        {
            monsterCtrlList.Last.Value.DeadMonster();
        }

    }

    public MonsterInfo GetValueMonsterInfoDict(int _uid)
    {
        monsterInfoDict.TryGetValue(_uid, out MonsterInfo value);
        if(value != null)
        {
            return value;
        }
        return null;
    }

    //public MonsterInfo GetCurrentMonsterInfo(int _uid)
    //{
    //    monsterInfoDict.TryGetValue(_uid, out MonsterInfo value);
    //    if (value != null)
    //    {
    //        return value;
    //    }
    //    return null;

    //}





    public BehaviorLogicBase GetMonsterbehaviorLogic(BehaviorLogicType _type)
    {
        if (behaviorLogicDict.ContainsKey(_type))
        {
            if(behaviorLogicDict[_type].Count <= 0)
            {
                behaviorLogicDict[_type].Enqueue(behaviorLogicArr[(int)_type].DeepCopy());
            }
            return behaviorLogicDict[_type].Dequeue();
        }

        return null;


        //return behaviorLogicArr[_idx].DeepCopy();
    }

    public MonsterBehavior GetMonsterMoveBehavior(MonsterMoveBehaviorType _type)
    {
        if (moveBehaviorDict.ContainsKey(_type))
        {
            if (moveBehaviorDict[_type].Count <= 0)
            {
                moveBehaviorDict[_type].Enqueue(monsterMoveBehaviorArr[(int)_type].DeepCopy());
            }
            return moveBehaviorDict[_type].Dequeue();
        }

        return null;


        //return monsterMoveBehaviorArr[_idx].DeepCopy();
    }
    public MonsterBehavior GetMonsterAttackBehavior(MonsterAttackBehaviorType _type)
    {
        if (attackBehaviorDict.ContainsKey(_type))
        {
            if (attackBehaviorDict[_type].Count <= 0)
            {
                attackBehaviorDict[_type].Enqueue(monsterAttackBehaviorArr[(int)_type].DeepCopy());
            }
            return attackBehaviorDict[_type].Dequeue();
        }

        return null;

        //return monsterAttackBehaviorArr[_idx]?.DeepCopy();
    }


    public void ReleaseMonsterLogicBehavior(BehaviorLogicBase _logicBehavior, BehaviorLogicType _type)
    {
        behaviorLogicDict[_type].Enqueue(_logicBehavior);
    }

    public void ReleaseMonsterMoveBehavior(MonsterBehavior _moveBehavior, MonsterMoveBehaviorType _type)
    {
        moveBehaviorDict[_type].Enqueue(_moveBehavior);
    }

    public void ReleaseMonsterAttackBehavior(MonsterBehavior _attackBehavior, MonsterAttackBehaviorType _type)
    {
        if(_type == MonsterAttackBehaviorType.None)
        {
            return;
        }
        attackBehaviorDict[_type].Enqueue(_attackBehavior);
    }



    public Sprite GetMonsterSprite(int _uid)
    {
        return monsterSpriteDict[_uid];
    }

    private void CreateMonster()
    {
        deadMonsterQueue = new Queue<LinkedListNode<MonsterController>>();
        poolParent = new GameObject();
        poolParent.name = "DeadMonster";

        MonsterController res = Resources.Load<MonsterController>("Prefabs/Monster");

        for (int i =0; i < CAPICITY; i++)
        {

            MonsterController obj = GameObject.Instantiate<MonsterController>(res, poolParent.transform);
            obj.monsterIdx = i;
            //monsterArr[i] = obj;
            targetArr[i] = obj;

            LinkedListNode<MonsterController> node = new LinkedListNode<MonsterController>(obj);
            deadMonsterQueue.Enqueue(node);
        }

    }

    private void CreateBossMonster()
    {
        BossMonsterController boss = GameObject.Instantiate<BossMonsterController>(Resources.Load<BossMonsterController>("Prefabs/TempBossMonster"));

        bossCtrl = boss;
        targetArr[CAPICITY] = boss;
    }

    private void LoadMonsterData()
    {
        MonsterInfoArrData monsterData = JsonController.ReadJson<MonsterInfoArrData>("MonsterData");
        int count = monsterData.monsterArr.Length;

        for(int i =0; i < count; i++)
        {
            MonsterInfo info = monsterData.monsterArr[i];
            //monsterInfoList.Add(info);

            int uid = info.Uid;
            monsterInfoDict.Add(uid, info);

            Sprite sprite = Resources.Load<Sprite>(info.monsterSpritePath);
            monsterSpriteDict.Add(uid, sprite);
        }
    }
}
