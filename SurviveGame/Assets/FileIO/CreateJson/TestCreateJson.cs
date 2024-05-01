using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCreateJson : MonoBehaviour
{
    private string jobFileName = "JobData";
    private string weaponFileName = "WeaponData";
    private string monsterFileName = "MonsterData";

    private JobData jobData;
    private WeaponData weaponData;
    private MonsterData monsterData;

    private void Awake()
    {
        //CreateCharacterJobJson();
        //CreateWeaponJson();
        //CreateMonsterJson();
    }

    private void CreateCharacterJobJson()
    {
        Job[] jobArr = new Job[2];

        Job job1 = new Job();
        job1.Uid = 1000;
        job1.uniqueAbillity = null;

        job1.increaseStatus = new StatusEffect[1];

        StatusEffect effect = new StatusEffect();
        effect.stringKey = "?????? ????";
        effect.effectType = StatusEffectType.Damage;
        effect.amount = +10;
        job1.increaseStatus[0] = effect;

        job1.decreaseStatus = new StatusEffect[2];

        effect = new StatusEffect();
        effect.stringKey = "???????? ????";
        effect.effectType = StatusEffectType.AttackSpeed;
        effect.amount = -3;
        job1.decreaseStatus[0] = effect;

        effect = new StatusEffect();
        effect.stringKey = "???? ????";
        effect.effectType = StatusEffectType.Speed;
        effect.amount = -3;
        job1.decreaseStatus[1] = effect;

        job1.unLockId = 10003;

        job1.stringKey = "?? ????";

        job1.jobSpritePath = "Sprite/Job/Job_0";
        job1.jobName = "?? ????";

        jobArr[0] = job1;

        Job job2 = new Job();
        job2.Uid = 1001;
        job2.uniqueAbillity = null;

        job2.increaseStatus = new StatusEffect[2];

        effect = new StatusEffect();
        effect.stringKey = "???????? ????";
        effect.effectType = StatusEffectType.AttackSpeed;
        effect.amount = +5;
        job2.increaseStatus[0] = effect;

        effect = new StatusEffect();
        effect.stringKey = "???? ????";
        effect.effectType = StatusEffectType.Speed;
        effect.amount = +5;
        job2.increaseStatus[1] = effect;

        job2.decreaseStatus = new StatusEffect[1];

        effect = new StatusEffect();
        effect.stringKey = "?????? ????";
        effect.effectType = StatusEffectType.Damage;
        effect.amount = -7;
        job2.decreaseStatus[0] = effect;

        job2.unLockId = 10004;

        job2.stringKey = "???? ????";

        job2.jobSpritePath = "Sprite/Job/Job_1";
        job2.jobName = "???? ????";

        jobArr[1] = job2;


        jobData = new JobData();
        jobData.jobArr = jobArr;
    }

    private void CreateWeaponJson()
    {
        WeaponItemInfo[] weaponInfoArr = new WeaponItemInfo[3];

        WeaponItemInfo info = new WeaponItemInfo();
        info.Uid = 2000;
        info.level = 1;
        info.damage = 10;
        info.damageRate = 0.5f;
        info.attackSpeed = 1f;
        info.attackRange = 2f;
        info.stringKey = "???";
        info.weaponSpritePath = "Sprite/Weapon/Weapon_1";
        info.weaponName = "???";
        info.weaponType = WeaponType.StingWeapon;
        weaponInfoArr[0] = info;

        info = new WeaponItemInfo();
        info.Uid = 2001;
        info.level = 1;
        info.damage = 5;
        info.damageRate = 0.3f;
        info.attackSpeed = 1f;
        info.attackRange = 5f;
        info.stringKey = "??";
        info.weaponSpritePath = "Sprite/Weapon/Weapon_3";
        info.weaponName = "??";
        info.weaponType = WeaponType.ShoootingWeapon;
        weaponInfoArr[1] = info;

        info = new WeaponItemInfo();
        info.Uid = 2002;
        info.level = 1;
        info.damage = 5;
        info.damageRate = 0.3f;
        info.attackSpeed = 1f;
        info.attackRange = 2f;
        info.stringKey = "?? ?";
        info.weaponSpritePath = "Sprite/Weapon/Weapon_2";
        info.weaponName = "?? ?";
        info.weaponType = WeaponType.MowWeapon;
        weaponInfoArr[2] = info;


        weaponData = new WeaponData();
        weaponData.weaponArr = weaponInfoArr;
    }

    private void CreateMonsterJson()
    {
        MonsterInfo[] monsterInfoArr = new MonsterInfo[2];
        MonsterInfo info = new MonsterInfo();
        info.Uid = 3000;
        info.hp = 10;
        info.damage = 5;
        info.speed = 3;
        info.attackSpeed = 1;
        info.attackRange = 1;
        info.stringKey = "근접 기본몬스터 ";
        info.monsterSpritePath = "Sprite/Monster/CommonMonster_0";
        info.monsterName = "근접 기본몬스터 ";
        info.logicType = BehaviorLogicType.LoopBehavior;
        info.moveType = MonsterMoveBehaviorType.ApproachToTarget;
        info.attackType = MonsterAttackBehaviorType.None;
        monsterInfoArr[0] = info;

        info = new MonsterInfo();
        info.Uid = 3001;
        info.hp = 10;
        info.damage = 3;
        info.speed = 3;
        info.attackSpeed = 1;
        info.attackRange = 1;
        info.stringKey = "원거리 기본몬스터 ";
        info.monsterSpritePath = "Sprite/Monster/ShootingMonster_0";
        info.monsterName = "원거리 기본몬스터 ";
        info.logicType = BehaviorLogicType.LoopBehavior;
        info.moveType = MonsterMoveBehaviorType.RunAwayFromTarget;
        info.attackType = MonsterAttackBehaviorType.Shooting;
        monsterInfoArr[1] = info;

        monsterData = new MonsterData();
        monsterData.monsterArr = monsterInfoArr;
    }

    [ContextMenu("To Json Character Job Data")]  // ???????? ?????? ???? ?????? ???????? To Json Data ???? ???????? ?????? 
    private void SaveToJsonCharacterJobData()
    {
        if (jobFileName == "")
        {
            Debug.Log("???? ???? ???? ");
            return;
        }
        JsonController.WriteJson<JobData>(jobFileName, jobData);
    }


    [ContextMenu("To Json Weapon Data")]  // ???????? ?????? ???? ?????? ???????? To Json Data ???? ???????? ?????? 
    private void SaveToJsonWeaponData()
    {
        if (weaponFileName == "")
        {
            Debug.Log("???? ???? ???? ");
            return;
        }
        JsonController.WriteJson<WeaponData>(weaponFileName, weaponData);
    }

    [ContextMenu("To Json Monster Data")]

    private void SaveToJsonMonsterData()
    {
        if (monsterFileName == "")
        {
            Debug.Log("???? ???? ???? ");
            return;
        }
        JsonController.WriteJson<MonsterData>(monsterFileName, monsterData);

    }


}
