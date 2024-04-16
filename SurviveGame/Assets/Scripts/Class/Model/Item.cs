using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public ItemInfo itemInfo;
}

public class ItemInfo
{
    public string itemName;
    public List<StatusEffect> buff;
    public List<StatusEffect> deBuff;
}

public class StatusEffect
{
    public string stringKey;
    public StatusEffectType effectType;
    public int amount;
}

public enum StatusEffectType
{
    Damage,
    AtteckSpeed,
    Speed,
    Hp,
    Def,
    LongRangeDamege,
    ShortRangeDamege,
    Lenght,
}
