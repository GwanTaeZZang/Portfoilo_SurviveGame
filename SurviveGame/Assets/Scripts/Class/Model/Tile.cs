using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileMapModel
{
    public TileModel[] tileModelArr;
    public int width;
    public int height;

    public TileMapModel(TileModel[] _tileModelArr, int _width, int _height)
    {
        tileModelArr = _tileModelArr;
        width = _width;
        height = _height;
    }
}

[System.Serializable]
public class TileModel
{
    public string imageName;
    public Sprite sprite;
    public bool isMove;

    public TileModel(Sprite _sprite, string _imageName, bool _isMove)
    {
        sprite = _sprite;
        imageName = _imageName;
        isMove = _isMove;
    }
}

public class Tile
{
    public SpriteRenderer tile;

    public Tile(SpriteRenderer _tile)
    {
        tile = _tile;
    }
}

