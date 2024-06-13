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
    public int groupId;
    public int augmenterGrade;
    public string augmenterName;
    public string augmenterSpritePath;
    public string augmenterContent;

    public AugmenterType firstType;
    public float firstTypeValue;

    public AugmenterType secondType;
    public float secondTypeValue;

}


public enum AugmenterType
{
    MonsterSpawnTime = 100,

    P_MaxHP = 200,
    P_RecoveryHp,
    P_StealHp,
    P_Damage,
    P_ShortRangeDamage,
    P_LongRangeDamage,
    P_AttackSpeed,
    P_CriticalRate,
    P_AttackRange,
    P_Defence,
    P_EvasionRate,
    P_Speed,
    P_Luck,
    P_Yield,

    M_Hp = 300,
    M_Damege,
    M_Speed,
    M_AttackSpeed,
    M_AttackRange,


}