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
    private GameObject poolParent;

    private int max = 50;
    private int min = -50;
    private int weight = 30;
    private float playerNonSpwanArea = 3f;

    private Vector2 xStartVector = Vector2.zero;
    private Vector2 yStartVector = Vector2.zero;
    private Vector2 endVector = Vector2.zero;
    private Vector2 randomVector = Vector2.zero;
    private Vector2 ramdomWeightVector = Vector2.zero;



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

        Vector2 randomPos = Vector2.zero;

        for (int i =0; i<_count; i++)
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

            randomPos = ComputeMonsterRandomVector();
            monsterCtrl.ShowMonster(randomPos, monsterSpriteDict[_uid]);


            node.Value = monsterCtrl;
            monsterCtrlList.AddLast(node);

            //ComputeMonsterRandomVector();
        }

        //return randomPos;
    }

    public void DeadMonster(MonsterController _deadMonster)
    {
        LinkedListNode<MonsterController> node;

        node = monsterCtrlList.Find(_deadMonster);
        node.Value.transform.SetParent(poolParent.transform);
        deadMonsterQueue.Enqueue(node);
        monsterCtrlList.Remove(node);


        //foreach (MonsterController monster in monsterCtrlList)
        //{
        //    if(monster.monsterIdx == _deadMonster.monsterIdx)
        //    {

        //    }
        //}
    }


    private Vector2 ComputeMonsterRandomVector()
    {
        int xpos = Random.Range(min, max);
        int ypos = Random.Range(min, max);

        randomVector = new Vector2(xpos, ypos).normalized;

        xStartVector = new Vector2(10 * (randomVector.x > 0 ? +1 : -1), 0);
        yStartVector = new Vector2(0, 10 * (randomVector.y > 0 ? +1 : -1));
        endVector = new Vector2(xStartVector.x, yStartVector.y);

        //Debug.Log(xStartVector.x);
        //Debug.Log(yStartVector.y);
        //Debug.Log(endVector);

        ramdomWeightVector.x = randomVector.x * weight;
        ramdomWeightVector.y = randomVector.y * weight;

        Vector2 crossVector = Vector2.zero;
        Vector2 playerPos = PlayerManager.getInstance.GetPlayer().transform.position;

        bool result = CrossCheck2D(playerPos, ramdomWeightVector, xStartVector, endVector);
        if (result)
        {
            crossVector = CrossCheck2DVector(playerPos, ramdomWeightVector, xStartVector, endVector);
            //return crossVector;
            // 기존에 랜덤 백터에서 크로스 백터 사이의 랜덤 거리를 찾아 리턴
        }
        else
        {
            crossVector = CrossCheck2DVector(playerPos, ramdomWeightVector, yStartVector, endVector);
            //return crossVector;
            // 기존에 랜덤 백터에서 크로스 백터 사이의 랜덤 거리를 찾아 리턴
        }

        float distance = Vector2.Distance(playerPos, crossVector);
        float randomDistance = Random.Range(playerNonSpwanArea, distance);

        if (distance < playerNonSpwanArea + 1)
        {
            Vector2 monsterPos = new Vector2(-randomVector.x * randomDistance, -randomVector.y * randomDistance);
            return monsterPos;
        }
        else
        {
            Vector2 monsterPos = new Vector2(randomVector.x * randomDistance, randomVector.y * randomDistance);
            return monsterPos;
        }

    }
    private float ComputeRandomBetweenVector(Vector2 _startVector, Vector2 _endVector)
    {
        float distance = Vector2.Distance(_startVector, _endVector);
        float randomDistance = Random.Range(playerNonSpwanArea, distance);

        return randomDistance;
    }
    private bool CrossCheck2D(Vector2 aStart, Vector2 aEnd, Vector2 bStart, Vector2 bEnd)
    {
        float x1, x2, x3, x4, y1, y2, y3, y4, X, Y;

        x1 = aStart.x;
        x2 = aEnd.x;
        x3 = bStart.x;
        x4 = bEnd.x;

        y1 = aStart.y;
        y2 = aEnd.y;
        y3 = bStart.y;
        y4 = bEnd.y;

        float cross = ((x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4));

        if (cross == 0) return false;

        X = ((x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4)) / cross;
        Y = ((x1 * y2 - y1 * x2) * (y3 - y4) - (y1 - y2) * (x3 * y4 - y3 * x4)) / cross;

        //if(cross == 0)
        //{
        //    return false;
        //}
        //else
        //{
        //    return true;
        //}

        return CheckDotInLine(aStart, aEnd, new Vector2(X, Y)) && CheckDotInLine(bStart, bEnd, new Vector2(X, Y));
    }
    private Vector2 CrossCheck2DVector(Vector2 aStart, Vector2 aEnd, Vector2 bStart, Vector2 bEnd)
    {
        float x1, x2, x3, x4, y1, y2, y3, y4, X, Y;

        x1 = aStart.x;
        x2 = aEnd.x;
        x3 = bStart.x;
        x4 = bEnd.x;

        y1 = aStart.y;
        y2 = aEnd.y;
        y3 = bStart.y;
        y4 = bEnd.y;

        float cross = ((x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4));

        if (cross == 0)
        {
            return new Vector2(10000, 10000);
        }

        X = ((x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4)) / cross;
        Y = ((x1 * y2 - y1 * x2) * (y3 - y4) - (y1 - y2) * (x3 * y4 - y3 * x4)) / cross;

        return new Vector2(X, Y);
    }
    private bool CheckDotInLine(Vector2 a, Vector2 b, Vector2 dot)
    {
        float epsilon = 0.00001f;
        float dAB = Vector2.Distance(a, b);
        float dADot = Vector2.Distance(a, dot);
        float dbDot = Vector2.Distance(b, dot);

        return ((dAB + epsilon) >= (dADot + dbDot));

    }

    private Vector2 ComputeMonsterRandomSpawnPos()
    {
        float MinXpos = -10f;
        float MaxXpos = 10f;
        float MinYpos = -10f;
        float MaxYpos = 10;
        //float PlayerNonSpwanArea = 3f;

        float xPos;
        float yPos;

        xPos = Random.Range(MinXpos, MaxXpos);

        Vector2 playerPos = PlayerManager.getInstance.GetPlayer().transform.position;

        Debug.Log("Random X Pos = " + xPos);

        if(playerPos.x - playerNonSpwanArea < xPos && xPos < playerPos.x + playerNonSpwanArea)
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

            yPos = Random.Range(playerPos.y + ((-1 * aa)*playerNonSpwanArea),
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
        poolParent = new GameObject();
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
