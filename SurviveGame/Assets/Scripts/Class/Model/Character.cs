using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    //CharacterStatus status;
    public CharacterStatus[] statusArr = new CharacterStatus[(int)CharacterStatusType.End];
    public Job job;

    public Character(Job _characterJob)
    {
        int count = statusArr.Length;
        for(int i = 0; i < count; i++)
        {
            statusArr[i] = new CharacterStatus();
        }


        statusArr[(int)CharacterStatusType.P_MaxHP].UpdataStatus(20f);
        statusArr[(int)CharacterStatusType.P_Speed].UpdataStatus(5f);

        job = _characterJob;

        count = job.increaseStatus.Length;
        for(int i = 0; i < count; i++)
        {
            UpdataStatus(job.increaseStatus[i].effectType, job.increaseStatus[i].amount);
        }

        count = job.decreaseStatus.Length;
        for(int i =0; i < count; i++)
        {
            UpdataStatus(job.decreaseStatus[i].effectType, job.decreaseStatus[i].amount);
        }

        count = statusArr.Length;
        for(int i =0; i < count; i++)
        {
            Debug.Log((CharacterStatusType)i + " =  " + statusArr[i].status);
        }
    }


    public void UpdataStatus(CharacterStatusType _type, int _amount)
    {
        statusArr[(int)_type].UpdataStatus(_amount);
    }
    public void UpdataRate(CharacterStatusType _type, float _rate)
    {
        statusArr[(int)_type].UpdatsStatusRate(_rate);
    }
    public void SetAbillityRate(CharacterStatusType _type, float _abillityRate)
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
public struct JobArrJson
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
    public CharacterStatusType effectType;
    public int amount;
}

public enum CharacterStatusType
{
    P_MaxHP,
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

    End,
}


