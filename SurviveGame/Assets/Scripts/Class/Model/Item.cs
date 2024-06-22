using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct ItemInfoArrJson
{
    public ItemInfo[] itemInfoArr;
}

[System.Serializable]
public class ItemInfo
{
    public int Uid;
    public int price;
    public string itemSpritePath;
    public string itemName;
    public string itemContent;
    public ItemEffect[] itemEffectArr;
}

[System.Serializable]
public class ItemEffect
{
    public ItemEffectType statusEffectType;
    public float amount;
}

public enum ItemEffectType
{
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

    M_HP = 300,
    M_Damage,
    M_Speed,
    M_AttackSpeed,
    M_AttackRange,

}
