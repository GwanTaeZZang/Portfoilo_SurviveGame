using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AugmenterArrJson
{
    public AugmenterData[] augmenterArr;
}

[System.Serializable]
public class AugmenterData
{
    public int Uid;
    public int augmenterGrade;
    public string augmenterName;
    public string augmenterSpritePath;
    public string augmenterContent;
}

public enum AugmenterType
{
    monsterSpawnInCrease,
    monsterSpawnDeCrease,
    playerAttackInCrease,
    playerAttackDeCrease,
    playerStatus,
    monsterStatus
}