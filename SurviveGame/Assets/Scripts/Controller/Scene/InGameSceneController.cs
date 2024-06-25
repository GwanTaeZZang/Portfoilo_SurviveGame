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
    [SerializeField] private InGameCanvas inGameCanvas;

    private Tile[,] tiles;

    private StageController stageController;

    private void Awake()
    {
        stageController = new StageController();

        stageController.Initialized(inGameCanvas);
        playerController.Initialize();
        weaponController.Initialize();

        LoadMap();
    }

    private void Start()
    {
        //GameStart();
    }

    private void Update()
    {
        stageController.UpdateWave();
        TempShowAugmenterCanvas();
    }

    private void GameStart()
    {
        StageManager.getInstance.StartWave();
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

    private void TempShowAugmenterCanvas()
    {
        if (Input.GetKeyDown("space"))
        {
            stageController.TempSpawnBossMosnter();
        }
    }


}
