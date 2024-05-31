using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCreateJson : MonoBehaviour
{
    private string jobFileName = "JobData";
    private string weaponFileName = "WeaponData";
    private string monsterFileName = "MonsterData";
    private string monsterSpwanFileName = "MonsterSpwanData";
    private string waveFileName = "WaveData";
    private string stageFileName = "StageData";

    private JobData jobData;
    private WeaponData weaponData;
    private MonsterData monsterData;
    private MonsterSpwanGroupData monsterSpwanGroupData;
    private WaveGroupData waveGroupData;
    private StageGroupData stageGroupData;

    private void Awake()
    {
        //CreateCharacterJobJson();
        //CreateWeaponJson();
        //CreateMonsterJson();
        //CreateMonsterSpwanJson();
        //CreateWaveData();
        CreateStageJson();
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

    private void CreateMonsterSpwanJson()
    {
        monsterSpwanGroupData = new MonsterSpwanGroupData();
        MonsterSpwanData[] monsterSpwanDataArr = new MonsterSpwanData[4];
        MonsterSpwanData monsterSpwanData = new MonsterSpwanData();
        monsterSpwanData.Uid = 4000;
        monsterSpwanData.monsterID = 3000;
        monsterSpwanData.reSpwanTime = 2f;
        monsterSpwanData.monsterCount = 3;
        monsterSpwanDataArr[0] = monsterSpwanData;

        monsterSpwanData = new MonsterSpwanData();
        monsterSpwanData.Uid = 4001;
        monsterSpwanData.monsterID = 3000;
        monsterSpwanData.reSpwanTime = 8f;
        monsterSpwanData.monsterCount = 7;
        monsterSpwanDataArr[1] = monsterSpwanData;

        monsterSpwanData = new MonsterSpwanData();
        monsterSpwanData.Uid = 4002;
        monsterSpwanData.monsterID = 3001;
        monsterSpwanData.reSpwanTime = 5f;
        monsterSpwanData.monsterCount = 2;
        monsterSpwanDataArr[2] = monsterSpwanData;

        monsterSpwanData = new MonsterSpwanData();
        monsterSpwanData.Uid = 4003;
        monsterSpwanData.monsterID = 3001;
        monsterSpwanData.reSpwanTime = 7f;
        monsterSpwanData.monsterCount = 3;
        monsterSpwanDataArr[3] = monsterSpwanData;

        monsterSpwanGroupData.monsterSpwanDataArr = monsterSpwanDataArr;

    }

    private void CreateWaveData()
    {
        waveGroupData = new WaveGroupData();
        WaveData[] waveDataArr = new WaveData[16];
        WaveData waveData = new WaveData();
        waveData.Uid = 4010;
        waveData.monsterSpwanUid = new int[] { 4000 };
        waveData.waveTime = 30f;
        waveDataArr[0] = waveData;

        waveData = new WaveData();
        waveData.Uid = 4011;
        waveData.monsterSpwanUid = new int[] { 4000 , 4000};
        waveData.waveTime = 30f;
        waveDataArr[1] = waveData;

        waveData = new WaveData();
        waveData.Uid = 4012;
        waveData.monsterSpwanUid = new int[] { 4000, 4001 };
        waveData.waveTime = 30f;
        waveDataArr[2] = waveData;

        waveData = new WaveData();
        waveData.Uid = 4013;
        waveData.monsterSpwanUid = new int[] { 4000, 4003 };
        waveData.waveTime = 30f;
        waveDataArr[3] = waveData;

        waveData = new WaveData();
        waveData.Uid = 4014;
        waveData.monsterSpwanUid = new int[] { 4000, 4000, 4001 };
        waveData.waveTime = 30f;
        waveDataArr[4] = waveData;

        waveData = new WaveData();
        waveData.Uid = 4015;
        waveData.monsterSpwanUid = new int[] { 4000, 4001, 4003 };
        waveData.waveTime = 30f;
        waveDataArr[5] = waveData;

        waveData = new WaveData();
        waveData.Uid = 4016;
        waveData.monsterSpwanUid = new int[] { 4000, 4000, 4001, 4003 };
        waveData.waveTime = 30f;
        waveDataArr[6] = waveData;

        waveData = new WaveData();
        waveData.Uid = 4017;
        waveData.monsterSpwanUid = new int[] { 4000, 4001, 4003, 4004 };
        waveData.waveTime = 30f;
        waveDataArr[7] = waveData;

        waveGroupData.waveDataArr = waveDataArr;
    }

    private void CreateStageJson()
    {
        stageGroupData = new StageGroupData();
        StageData[] stageDataArr = new StageData[2];
        StageData stageData = new StageData();
        stageData.Uid = 4100;
        stageData.waveUidArr = new int[] {
        4010,
        4010,
        4011,
        4011,
        4012,
        4012,
        4013,
        4013,
        4014,
        4014,
        4015,
        4015,
        4016,
        4016,
        4017,
        4017};
        stageData.curWaveIdx = 0;
        stageDataArr[0] = stageData;

        stageData = new StageData();
        stageData.Uid = 4101;
        stageData.waveUidArr = new int[] {
        4017,
        4017,
        4016,
        4016,
        4015,
        4015,
        4014,
        4014,
        4013,
        4013,
        4012,
        4012,
        4011,
        4011,
        4010,
        4010};
        stageData.curWaveIdx = 0;
        stageDataArr[1] = stageData;

        stageGroupData.stageDataArr = stageDataArr;

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

    [ContextMenu("To Json MonsterSpwan Data")]
    private void SaveToJsonMonsterSpwanData()
    {
        if(monsterSpwanFileName == "")
        {
            Debug.Log("File Name is null");
            return;
        }

        JsonController.WriteJson<MonsterSpwanGroupData>(monsterSpwanFileName, monsterSpwanGroupData);
    }

    [ContextMenu("To Json Wave Data")]
    private void SaveToJsonWaveData()
    {
        if (waveFileName == "")
        {
            Debug.Log("File Name is null");
            return;
        }

        JsonController.WriteJson<WaveGroupData>(waveFileName, waveGroupData);
    }

    [ContextMenu("To Json Stage Data")]
    private void SaveToJsonStageData()
    {
        if (stageFileName == "")
        {
            Debug.Log("File Name is null");
            return;
        }

        JsonController.WriteJson<StageGroupData>(stageFileName, stageGroupData);
    }

}
