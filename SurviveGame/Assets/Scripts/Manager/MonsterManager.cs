using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : Singleton<MonsterManager>
{
    private const int CAPICITY = 100;

    private BehaviorLogicBase[] behaviorLogicArr;
    private MonsterBehavior[] monsterAttackBehaviorArr;
    private MonsterBehavior[] monsterMoveBehaviorArr;

    private List<MonsterInfo> monsterInfoList = new List<MonsterInfo>();
    private Dictionary<int, MonsterInfo> monsterInfoDict = new Dictionary<int, MonsterInfo>();
    private Dictionary<int, Sprite> monsterSpriteDict = new Dictionary<int, Sprite>();


    private LinkedList<MonsterController> monsterCtrlList = new LinkedList<MonsterController>();
    private Queue<LinkedListNode<MonsterController>> deadMonsterQueue;

    private ITargetAble[] targetArr = new ITargetAble[100];

    private GameObject poolParent;


    public override bool Initialize()
    {
        LoadMonsterData();

        BindMonsterBehaviorInstance();

        CreateMonster();

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

    public BehaviorLogicBase GetMonsterbehaviorLogic(int _idx)
    {
        return behaviorLogicArr[_idx].DeepCopy();
    }

    public MonsterBehavior GetMonsterMoveBehavior(int _idx)
    {
        return monsterMoveBehaviorArr[_idx].DeepCopy();
    }
    public MonsterBehavior GetMonsterAttackBehavior(int _idx)
    {
        return monsterAttackBehaviorArr[_idx]?.DeepCopy();
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

            Sprite sprite = Resources.Load<Sprite>(info.monsterSpritePath);
            monsterSpriteDict.Add(uid, sprite);
        }
    }
}
