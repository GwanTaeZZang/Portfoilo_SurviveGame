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

    private LinkedList<MonsterController> monsterCtrlList = new LinkedList<MonsterController>();
    private Queue<LinkedListNode<MonsterController>> deadMonsterQueue;

    private GameObject aliveMonsterParent;



    //private int showMonsterCounter = 0;

    public override bool Initialize()
    {
        LoadMonsterData();

        BindMonsterBehaviorInstance();

        CreateMonster();

        aliveMonsterParent = new GameObject();
        aliveMonsterParent.name = "Alive Monster Parent";

        return base.Initialize();
    }

    public LinkedList<MonsterController> GetMonsterList()
    {
        return monsterCtrlList;
    }

    public void SpawnMonster(int _count, int _uid)
    {
        for(int i =0; i<_count; i++)
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

            monsterInfoDict.TryGetValue(_uid, out MonsterInfo value);
            monsterCtrl.SetMonsterInfo(value);

            BehaviorLogicBase logic = behaviorLogicArr[(int)value.logicType].DeepCopy();
            MonsterBehavior moveBehavior = monsterMoveBehaviorArr[(int)value.moveType].DeepCopy();

            MonsterBehavior attackBehavior;
            attackBehavior = monsterAttackBehaviorArr[(int)value.attackType]?.DeepCopy();
            logic.Initialize(monsterCtrl.transform, moveBehavior, attackBehavior);

            monsterCtrl.SetMonsterBehavior(logic);

            monsterCtrl.ShowMonster();

            monsterCtrl.transform.SetParent(aliveMonsterParent.transform);

            monsterCtrl.ShowMonster();


            node.Value = monsterCtrl;
            monsterCtrlList.AddLast(node);
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



    private void CreateMonster()
    {
        deadMonsterQueue = new Queue<LinkedListNode<MonsterController>>();
        GameObject poolParent = new GameObject();
        poolParent.name = "DeadMonster";

        MonsterController res = Resources.Load<MonsterController>("Prefabs/Monster");

        for (int i =0; i < CAPICITY; i++)
        {

            MonsterController obj = GameObject.Instantiate<MonsterController>(res, poolParent.transform);
            obj.monsterIdx = i;

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
        }
    }
}
