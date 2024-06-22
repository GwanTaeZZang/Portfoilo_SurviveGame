using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItemInfo
{
    public int Uid;
    public int price;
    public string itemName;
    public string itemSpritePath;
    public ItemType itemType;
}

public enum ItemType
{
    WeaponType,
    PassiveType,
}