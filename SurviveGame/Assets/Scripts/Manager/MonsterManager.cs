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
            logic.Initialize(value, monsterCtrl.transform, moveBehavior, attackBehavior);

            monsterCtrl.SetMonsterBehavior(logic);

            monsterCtrl.transform.SetParent(aliveMonsterParent.transform);

            monsterCtrl.ShowMonster(ComputeMonsterRandomSpawnPos(), monsterSpriteDict[_uid]);


            node.Value = monsterCtrl;
            monsterCtrlList.AddLast(node);
        }
    }

    private Vector2 ComputeMonsterRandomSpawnPos()
    {
        float MinXpos = -10f;
        float MaxXpos = 10f;
        float MinYpos = -10f;
        float MaxYpos = 10;
        float PlayerNonSpwanArea = 3f;

        float xPos;
        float yPos;

        xPos = Random.Range(MinXpos, MaxXpos);

        Vector2 playerPos = PlayerManager.getInstance.GetPlayer().transform.position;

        Debug.Log("Random X Pos = " + xPos);

        if(playerPos.x - PlayerNonSpwanArea < xPos && xPos < playerPos.x + PlayerNonSpwanArea)
        {
            // xpos is in Player area
            //float threshold = 0.5f;

            //bool result = Random.Range(0f, 1f) > Mathf.Abs(playerPos.y) / 10f + threshold;

            //if (result)
            //{
            //    yPos = Random.Range(playerPos.y + PlayerNonSpwanArea, MaxYpos);
            //    Debug.Log(" + Random Y = " + yPos);

            //}
            //else
            //{
            //    yPos = Random.Range(MinYpos, playerPos.y - PlayerNonSpwanArea);
            //    Debug.Log(" - Random Y = " + yPos);

            //}

            int threshold = 50 + (int)(playerPos.y * 7);
            int randomNum = Random.Range(0,100);
            int aa = randomNum < threshold ? +1 : -1;

            yPos = Random.Range(playerPos.y + ((-1 * aa)*PlayerNonSpwanArea),
                (-1 * aa)*MaxYpos);

            //yPos *= aa;


        }
        else
        {
            yPos = Random.Range(MinYpos, MaxYpos);

            Debug.Log("Random Y = " + yPos);

        }

        Debug.Log("x = " + xPos + "  y = " + yPos);
        Debug.Log("========================================");
        return new Vector2(xPos, yPos);
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

            Sprite sprite = Resources.Load<Sprite>(info.monsterSpritePath);
            monsterSpriteDict.Add(uid, sprite);
        }
    }
}
