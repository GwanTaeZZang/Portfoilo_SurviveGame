using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    //CharacterStatus status;
    public CharacterStatus[] statusArr = new CharacterStatus[(int)StatusEffectType.End];
    public Job job;

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

    public void UpdataStatus(int _amount)
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
    //float maxHp;
    //float recoveryHp;
    //float stealHp;
    //float damage;
    //float shortRangeDamage;
    //float longRangeDamage;
    //float attackSpeed;
    //float criticalRate;
    //float attackRange;
    //float defence;
    //float evasionRate;
    //float speed;
    //float luck;
    //float yield;
}

public class Job
{
    public UniqueAbillity uniqueAbillity;
    public StatusEffect[] increaseStatus;
    public StatusEffect[] decreaseStatus;
    public int unLockId;
    public int uniqueId;
    public string stringKey;
    public string spriteName;
}

public class UniqueAbillity
{
    // 무기 타입이나 능력치 타입과 증감 퍼센트 받아서 처리
    // 무기 타입 이넘 정해지면 마무리
}

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
