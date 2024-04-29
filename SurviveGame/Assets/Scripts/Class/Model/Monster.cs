using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterBase
{
    public MonsterInfo monsterInfo;
    public Sprite monsterSprite;

    protected Transform monster;
    protected Transform target;

    


    public abstract void UpdateMonster();
}

[System.Serializable]
public class MonsterData
{
    public MonsterInfo[] monsterArr;
}

[System.Serializable]
public class MonsterInfo
{
    public int Uid;
    public int hp;
    public float damage;
    public float speed;
    public float attackSpeed;
    public float attackRange;
    public string stringKey;
    public string monsterSpritePath;
    public string monsterName;
    public MonsterType monsterType;
}

public enum MonsterType
{
    NormalMonster,
    ShootingMonster,
    RushMonster,
}

public class NormalMonster : MonsterBase
{
    public override void UpdateMonster()
    {
        throw new System.NotImplementedException();
    }
}
public class ShootingMonster : MonsterBase
{
    public override void UpdateMonster()
    {
        throw new System.NotImplementedException();
    }
}
public class RushMonster : MonsterBase
{
    public override void UpdateMonster()
    {
        throw new System.NotImplementedException();
    }
}