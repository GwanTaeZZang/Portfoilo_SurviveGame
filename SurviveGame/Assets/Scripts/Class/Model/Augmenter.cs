using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AugmenterArrJson
{
    AugmenterData[] augmenterArr;
}

[System.Serializable]
public class AugmenterData
{
    public int Uid;
    public int augmenterGrade;
    public string augmenterName;
    public string augmenterSpritePath;

}

public enum AugmenterType
{
    monsterSpawn,
    playerStatus,
    monsterStatus
}