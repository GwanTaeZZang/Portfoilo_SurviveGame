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
    public List<Effect> buff;
    public List<Effect> deBuff;
}

public class Effect
{
    public string stringKey;
    public EffectType effectType;
    public int amount;
}

public enum EffectType
{
    Damage,
    AtteckSpeed,
    Speed,
    Hp,
    Def,
    LongRangeDamege,
    ShortRangeDamege,
}
