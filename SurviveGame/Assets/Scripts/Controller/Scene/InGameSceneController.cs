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
    //private WaveData waveData;
    private List<MonsterSpawnData> MosnterSpwanDataList = new List<MonsterSpawnData>();
    private float waveTime;
    private bool isWave = false;

    private Tile[,] tiles;

    private float testSpwanTime = 4f;
    private float testSpwanTimer = 0f;

    private void Awake()
    {
        stageManager = StageManager.getInstance;
        //stageManager.Initialize();
        stageManager.OnWaveEvent = SetWaveMonster;

        playerController.Initialize();
        weaponController.Initialize();

        LoadMap();
    }

    private void Start()
    {
        stageData = stageManager.GetSelectedStage();
        //CreateMonster();

        stageManager.StartWave();
        //StartWave();
    }

    private void Update()
    {
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

    private void SetWaveMonster(List<MonsterSpawnData> _monsterSpwanDataList, float _waveTime)
    {
        //int waveUid = stageData.waveUidArr[stageData.curWaveIdx];
        //waveData = stageManager.GetWaveData(waveUid);

        //waveTime = waveData.waveTime;

        //int count = waveData.monsterSpwanUid.Length;
        //for(int i =0;i < count; i++)
        //{
        //    MonsterSpwanData data = stageManager.GetMonsterSpwanData(waveData.monsterSpwanUid[i]);
        //    MosnterSpwanDataList.Add(data);
        //}

        waveTime = _waveTime;
        MosnterSpwanDataList = _monsterSpwanDataList;


        int count = MosnterSpwanDataList.Count;
        Debug.Log("This Wave Monster Type Count  : " + count);
        for( int i =0; i < count; i++)
        {
            Debug.Log("This Wave Monster Type   : " + MosnterSpwanDataList[i].monsterId);
        }

        isWave = true;
    }

    //private void StartWave()
    //{
    //    int waveUid = stageData.waveUidArr[stageData.curWaveIdx];
    //    waveData = stageManager.GetWaveData(waveUid);

    //    waveTime = waveData.waveTime;

    //    int count = waveData.monsterSpwanUid.Length;
    //    for (int i = 0; i < count; i++)
    //    {
    //        MonsterSpwanData data = stageManager.GetMonsterSpwanData(waveData.monsterSpwanUid[i]);
    //        MosnterSpwanDataList.Add(data);
    //    }

    //}

    private void UpdateWave()
    {
        if (isWave)
        {
            waveTime -= Time.deltaTime;
            waveTimerText.text = ((int)waveTime).ToString();

            if (waveTime < 0)
            {
                stageManager.EndWave();
                Invoke("ShowShopCanvas", 1f);
                isWave = false;
            }

            int count = MosnterSpwanDataList.Count;
            for(int i = 0; i < count; i++)
            {
                MonsterSpawnData monster = MosnterSpwanDataList[i];
                monster.timer += Time.deltaTime;

                if(monster.timer > monster.reSpawnTime)
                {
                    MonsterManager.getInstance.SpawnMonster(monster.count, monster.monsterId);
                    monster.timer = 0;
                }
            }
        }
    }

    private void ShowShopCanvas()
    {
        UIManager.getInstance.Show<ShopCanvas>("Canvas/ShopCanvas");
    }

}
