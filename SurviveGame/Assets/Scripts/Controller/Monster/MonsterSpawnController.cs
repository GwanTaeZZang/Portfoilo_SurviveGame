using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnController
{
    private MonsterManager monsterMgr;

    private Vector2 xStartVector = Vector2.zero;
    private Vector2 yStartVector = Vector2.zero;
    private Vector2 endVector = Vector2.zero;
    private Vector2 randomVector = Vector2.zero;
    private Vector2 randomWeightVector = Vector2.zero;
    private Vector2 playerRandomVector = Vector2.zero;
    private Vector2 playerPos = Vector2.zero;
    private Vector2 crossVector = Vector2.zero;

    private int max = 50;
    private int min = -50;
    private int weight = 30;
    private float playerNonSpwanArea = 3f;

    private GameObject aliveMonsterParent;
    private TileMapModel mapData;

    public void Initialized(TileMapModel _mapData)
    {
        monsterMgr = MonsterManager.getInstance;
        mapData = _mapData;
        playerPos = PlayerManager.getInstance.GetPlayer().transform.position;
        aliveMonsterParent = new GameObject();
        aliveMonsterParent.name = "Alive Monster Parent";

    }

    public void SpawnMonster(int _count, int _uid)
    {
        Vector2 randomPos = Vector2.zero;

        for (int i =0; i < _count; i++)
        {
            MonsterController monsterCtrl = monsterMgr.GetMonster();

            MonsterInfo info = monsterMgr.GetValueMonsterInfoDict(_uid);

            monsterCtrl.SetMonsterInfo(info);

            monsterCtrl.SetMonsterBehavior();
            //monsterCtrl.SetMonsterBehavior(logic);

            monsterCtrl.transform.SetParent(aliveMonsterParent.transform);

            randomPos = ComputeMonsterRandomVector();
            monsterCtrl.ShowMonster(randomPos, monsterMgr.GetMonsterSprite(_uid));

        }
    }

    public void SpawnBossMonster(int _uid, InGameCanvas _ingameCanvas)
    {
        BossMonsterController bossCtrl = monsterMgr.GetBossMonster();
        bossCtrl.Initialized();
        bossCtrl.ShowBossMonster(ComputeMonsterRandomVector(), _ingameCanvas);
    }

    //public void UpdateVecter()
    //{

    //    Debug.DrawLine(Vector3.zero, xStartVector, Color.blue);
    //    Debug.DrawLine(Vector3.zero, yStartVector, Color.green);
    //    Debug.DrawLine(Vector3.zero, endVector, Color.red);
    //    Debug.DrawLine(Vector3.zero, randomWeightVector, Color.black);
    //}

    private Vector2 ComputeMonsterRandomVector()
    {
        //int xpos = Random.Range(min, max);
        //int ypos = Random.Range(min, max);

        //randomVector = new Vector2(xpos, ypos).normalized;

        ////xStartVector = new Vector2((mapData.width * 0.5f) * (randomVector.x > 0 ? +1 : -1),
        ////    (mapData.height * 0.5f) * (randomVector.y < 0 ? +1 : -1));

        ////yStartVector = new Vector2((mapData.width * 0.5f) * (randomVector.x < 0 ? +1 : -1),
        ////    (mapData.height * 0.5f) * (randomVector.y > 0 ? +1 : -1));

        //xStartVector = new Vector2((mapData.width * 0.5f) * (randomVector.x > 0 ? +1 : -1),
        //    0);

        //yStartVector = new Vector2(0,
        //    (mapData.height * 0.5f) * (randomVector.y > 0 ? +1 : -1));


        //endVector = new Vector2(xStartVector.x, yStartVector.y);

        //ramdomWeightVector.x = randomVector.x * weight;
        //ramdomWeightVector.y = randomVector.y * weight;

        //Vector2 crossVector = Vector2.zero;
        //Vector2 playerPos = PlayerManager.getInstance.GetPlayer().transform.position;

        //bool result = CrossCheck2D(Vector3.zero, ramdomWeightVector, xStartVector, endVector);
        //if (result)
        //{
        //    crossVector = CrossCheck2DVector(Vector3.zero, ramdomWeightVector, xStartVector, endVector);
        //}
        //else
        //{
        //    crossVector = CrossCheck2DVector(Vector3.zero, ramdomWeightVector, yStartVector, endVector);
        //}

        //float distance = Vector2.Distance(playerPos, crossVector);
        //float randomDistance = Random.Range(playerNonSpwanArea, distance);

        //if (distance < playerNonSpwanArea)
        //{
        //    Vector2 monsterPos = new Vector2(-randomVector.x * randomDistance, -randomVector.y * randomDistance);
        //    return monsterPos;
        //}
        //else
        //{
        //    Vector2 monsterPos = new Vector2(randomVector.x * randomDistance, randomVector.y * randomDistance);
        //    return monsterPos;
        //}





        int xpos = Random.Range(min, max);
        int ypos = Random.Range(min, max);

        randomVector = new Vector2(xpos, ypos).normalized;

        Vector2 monsterPos = Vector2.zero;

        for (int i = 0; i < 4; i++)
        {
            Vector2 caculaterVecter = randomVector;
            caculaterVecter.x = randomVector.x * (1 - 2 * (i % 2));
            caculaterVecter.y = randomVector.y * (1 - 2 * ((i / 2) % 2));

            playerRandomVector.x = playerPos.x + caculaterVecter.x;
            playerRandomVector.y = playerPos.y + caculaterVecter.y;


            xStartVector = new Vector2((20 * 0.5f) * (caculaterVecter.x > 0 ? +1 : -1),
                (20 * 0.5f) * (caculaterVecter.y > 0 ? -1 : +1));

            yStartVector = new Vector2((20 * 0.5f) * (caculaterVecter.x > 0 ? -1 : +1),
                (20 * 0.5f) * (caculaterVecter.y > 0 ? +1 : -1));


            endVector = new Vector2(xStartVector.x, yStartVector.y);

            randomWeightVector.x = caculaterVecter.x * weight + playerPos.x;
            randomWeightVector.y = caculaterVecter.y * weight + playerPos.y;


            crossVector = GetCross2DVector(playerPos, randomWeightVector, xStartVector, yStartVector, endVector);

            float distance = Vector2.Distance(playerPos, crossVector);
            float randomDistance = Random.Range(playerNonSpwanArea, distance);


            if (distance > playerNonSpwanArea)
            {
                monsterPos.x = caculaterVecter.x * randomDistance + playerPos.x;
                monsterPos.y = caculaterVecter.y * randomDistance + playerPos.y;
                break;
            }
            else
            {
                Debug.Log("방향 변경");
            }

        }

        return monsterPos;


    }

    private Vector2 GetCross2DVector(Vector2 _playerVector, Vector2 _randomWeightVector, Vector2 _xStartVector, Vector2 _yStartVector, Vector2 _endVector)
    {

        Vector2 crossVector = Vector2.zero;

        bool result = CrossCheck2D(_playerVector, _randomWeightVector, _xStartVector, _endVector);
        if (result)
        {
            crossVector = CrossCheck2DVector(_playerVector, _randomWeightVector, _xStartVector, _endVector);
            //GameObject a = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //a.transform.position = crossVector;
            return crossVector;
        }
        else
        {
            crossVector = CrossCheck2DVector(_playerVector, _randomWeightVector, _yStartVector, _endVector);
            //GameObject a = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //a.transform.position = crossVector;
            return crossVector;

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

        if (playerPos.x - playerNonSpwanArea < xPos && xPos < playerPos.x + playerNonSpwanArea)
        {
            int threshold = 50 + (int)(playerPos.y * 7);
            int randomNum = Random.Range(0, 100);
            int aa = randomNum < threshold ? +1 : -1;

            yPos = Random.Range(playerPos.y + ((-1 * aa) * playerNonSpwanArea),
                (-1 * aa) * MaxYpos);

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


}
