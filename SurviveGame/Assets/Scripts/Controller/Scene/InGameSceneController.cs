using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameSceneController : MonoBehaviour
{
    private const float HALF = 0.5f;

    [SerializeField] private Transform mapParent;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private WeaponController weaponController;
    [SerializeField] private Text waveTimerText;

    private StageManager stageManager;
    private StageData stageData;
    private WaveData waveData;
    private List<MonsterSpwanData> MosnterSpwanDataList = new List<MonsterSpwanData>();
    private float waveTime;
    private bool isWave = false;

    private Tile[,] tiles;

    private float testSpwanTime = 4f;
    private float testSpwanTimer = 0f;

    private void Awake()
    {
        LoadMap();
    }

    private void Start()
    {
        playerController.Initialize();
        weaponController.Initialize();
        stageManager = StageManager.getInstance;
        stageData = stageManager.GetSelectedStage();

        //CreateMonster();

        SetWaveMonster();
    }

    private void Update()
    {
        //TestMonsterSpwan();
        //TestEventMonsterSpwan();
        UpdateWave();
    }

    private void LoadMap()
    {
        TileMapModel mapData = JsonController.ReadJson<TileMapModel>("MapData");
        TileModel[] tileArr = mapData.tileModelArr;
        int width = mapData.width;
        int height = mapData.height;

        float startXPos = (width - 1) * -HALF;
        float startYPos = (height - 1) * -HALF;

        SpriteRenderer tile = Resources.Load<SpriteRenderer>("Prefabs/Tile");

        tiles = new Tile[width, height];

        int count = width * height;
        for (int i = 0; i < count; i++)
        {
            SpriteRenderer sr = Instantiate<SpriteRenderer>(tile,mapParent);
            int widthCount = i % width;
            int heightCount = i / height;

            sr.sprite = Resources.Load<Sprite>("Sprite/Map/" + tileArr[i].imageName);
            sr.transform.position = new Vector2(startXPos + widthCount, startYPos + heightCount);

            tiles[widthCount, heightCount] = new Tile(sr);
        }
    }

    private void SetWaveMonster()
    {
        int waveUid = stageData.waveUidArr[stageData.curWaveIdx];
        waveData = stageManager.GetWaveData(waveUid);

        waveTime = waveData.waveTime;

        int count = waveData.monsterSpwanUid.Length;
        for(int i =0;i < count; i++)
        {
            MonsterSpwanData data = stageManager.GetMonsterSpwanData(waveData.monsterSpwanUid[i]);
            MosnterSpwanDataList.Add(data);
        }

        isWave = true;
    }

    private void UpdateWave()
    {
        if (isWave)
        {
            waveTime -= Time.deltaTime;
            waveTimerText.text = ((int)waveTime).ToString();

            if (waveTime < 0)
            {
                MonsterManager.getInstance.EndWave();
                isWave = false;
            }

            int count = MosnterSpwanDataList.Count;
            for(int i = 0; i < count; i++)
            {
                MonsterSpwanData monster = MosnterSpwanDataList[i];
                monster.timer += Time.deltaTime;

                if(monster.timer > monster.reSpwanTime)
                {
                    MonsterManager.getInstance.SpawnMonster(monster.monsterCount, monster.monsterID);
                    monster.timer = 0;
                }
            }
        }
    }


    private void CreateMonster()
    {
        MonsterManager.getInstance.SpawnMonster(3, 3000);
        MonsterManager.getInstance.SpawnMonster(2, 3001);
    }

    private void TestMonsterSpwan()
    {
        testSpwanTimer += Time.deltaTime;

        if(testSpwanTimer > testSpwanTime)
        {
            MonsterManager.getInstance.SpawnMonster(3, 3000);
            MonsterManager.getInstance.SpawnMonster(1, 3001);
            testSpwanTimer = 0f;
        }

    }

    private void TestEventMonsterSpwan()
    {
        if (Input.GetKeyDown("space"))
        {
            MonsterManager.getInstance.SpawnMonster(1, 3000);
        }
    }
}
