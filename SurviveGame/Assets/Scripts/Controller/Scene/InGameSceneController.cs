using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSceneController : MonoBehaviour
{
    private const float HALF = 0.5f;

    [SerializeField] private Transform mapParent;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private WeaponController weaponController;

    private Tile[,] tiles;

    private float testSpwanTime = 3f;
    private float testSpwanTimer = 0f;

    private void Awake()
    {
        LoadMap();
    }

    private void Start()
    {
        playerController.Initialize();
        weaponController.Initialize();

        //CreateMonster();
    }

    private void Update()
    {
        TestMonsterSpwan();
        //TestEventMonsterSpwan();
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
