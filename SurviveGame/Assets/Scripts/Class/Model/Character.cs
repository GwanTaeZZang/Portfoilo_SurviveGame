using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    //CharacterStatus status;
    public CharacterStatus[] statusArr = new CharacterStatus[(int)StatusEffectType.End];
    public Job job;

    public Character()
    {
        int count = statusArr.Length;
        for(int i = 0; i < count; i++)
        {
            statusArr[i] = new CharacterStatus();
        }


        statusArr[(int)StatusEffectType.MaxHp].UpdataStatus(10f);
        statusArr[(int)StatusEffectType.Speed].UpdataStatus(5f);
    }


    public void UpdataStatus(StatusEffectType _type, int _amount)
    {
        statusArr[(int)_type].UpdataStatus(_amount);
    }
    public void UpdataRate(StatusEffectType _type, float _rate)
    {
        statusArr[(int)_type].UpdatsStatusRate(_rate);
    }
    public void SetAbillityRate(StatusEffectType _type, float _abillityRate)
    {
        statusArr[(int)_type].SetAbillityRate(_abillityRate);
    }
}

public class CharacterStatus
{
    public float status;
    private float statusRate;
    private float abillityRate;

    public CharacterStatus()
    {
        status = 0f;
        statusRate = 1f;
        abillityRate = 1f;
    }

    public void UpdataStatus(float _amount)
    {
        status += (statusRate * abillityRate * _amount);
    }
    public void UpdatsStatusRate(float _rate)
    {
        statusRate += _rate;
        status = (status * _rate) + status;
    }
    public void SetAbillityRate(float _abillityRate)
    {
        abillityRate = _abillityRate;
    }
}


[System.Serializable]
public class JobData
{
    public Job[] jobArr;
}

[System.Serializable]
public class Job
{
    public int Uid;
    public UniqueAbillity uniqueAbillity;
    public StatusEffect[] increaseStatus;
    public StatusEffect[] decreaseStatus;
    public int unLockId;
    public string stringKey;
    public string jobSpritePath;
    public string jobName;
}

[System.Serializable]
public class UniqueAbillity
{
    // 무기 타입이나 능력치 타입과 증감 퍼센트 받아서 처리
    // 무기 타입 이넘 정해지면 마무리
}

[System.Serializable]
public class StatusEffect
{
    public string stringKey;
    public StatusEffectType effectType;
    public int amount;
}

public enum StatusEffectType
{
    MaxHp,
    RecoveryHp,
    StealHp,
    Damage,
    ShortRangeDamage,
    LongRangeDamage,
    AttackSpeed,
    CriticalRate,
    AttackRange,
    Defence,
    EvasionRate,
    Speed,
    Luck,
    Yield,
    End

}


