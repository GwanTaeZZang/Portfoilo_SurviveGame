using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapCreator : MonoBehaviour
{
    [SerializeField] string fileName;
    [SerializeField] int width;
    [SerializeField] int height;

    [SerializeField] Sprite[] ranSprite;
    [SerializeField] Sprite isNotMoveSprite;

    TileMapModel mapData;

    private void Start()
    {
        SpriteRenderer tile = Resources.Load<SpriteRenderer>("Prefabs/Tile");
        TileModel[] tileModelArr = new TileModel[width * height];

        //Vector2 pos = Vector2.zero;
        float startXPos = (width -1) * -0.5f;
        float startYPos = (height -1) * -0.5f;

        int count = width * height;
        for(int i = 0; i < count; i++)
        {
            SpriteRenderer sr = Instantiate<SpriteRenderer>(tile);
            int widthCount = i % width;
            int heightCount = i / height;

            if (widthCount == 0 || widthCount == width - 1 || heightCount == 0 || heightCount == height - 1)
            {
                sr.sprite = isNotMoveSprite;
                sr.transform.position = new Vector2(startXPos + widthCount, startYPos + heightCount);
                tileModelArr[i] = new TileModel(sr.sprite, false);
            }
            else
            {
                sr.sprite = ranSprite[Random.Range(0, ranSprite.Length)];
                sr.transform.position = new Vector2(startXPos + widthCount, startYPos + heightCount);
                tileModelArr[i] = new TileModel(sr.sprite, true);
            }

        }

        mapData = new TileMapModel(tileModelArr, width, height);
    }


    [ContextMenu("To Json Data")]  // 컴포넌트 메뉴에 아래 함수를 호출하는 To Json Data 라는 명렁어가 생성됨 
    private void SaveToJsonMapData()
    {
        if(fileName == "")
        {
            Debug.Log("파일 이름 없음 ");
            return;
        }
        JsonController.WriteJson<TileMapModel>(fileName, mapData);
    }
}
