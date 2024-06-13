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
    private string augmentFileName = "AugmenterData";

    private JobData jobData;
    private WeaponData weaponData;
    private MonsterData monsterData;
    private MonsterGroupArrJsonModel monsterGroupData;
    //private WaveGroupData waveGroupData;
    private StageArrJsonModel stageGroupData;
    private AugmenterArrJson augmenterGroupData;


    private void Awake()
    {
        //CreateCharacterJobJson();
        //CreateWeaponJson();
        CreateMonsterJson();
        //CreateMonsterSpwanJson();
        //CreateStageJson();
        CreateAugmenterJson();
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
        List<WeaponItemInfo> weaponInfoList = new List<WeaponItemInfo>();

        WeaponItemInfo info = new WeaponItemInfo();
        info.Uid = 2001;
        info.level = 1;
        info.damage = 10;
        info.damageRate = 0.5f;
        info.attackSpeed = 1f;
        info.attackRange = 3f;
        info.stringKey = "???";
        info.weaponSpritePath = "Sprite/Weapon/Weapon_1_1";
        info.weaponName = "Spear";
        info.weaponType = WeaponType.StingWeapon;
        weaponInfoList.Add(info);

        info = new WeaponItemInfo();
        info.Uid = 2002;
        info.level = 2;
        info.damage = 10;
        info.damageRate = 0.5f;
        info.attackSpeed = 0.7f;
        info.attackRange = 3f;
        info.stringKey = "???";
        info.weaponSpritePath = "Sprite/Weapon/Weapon_1_2";
        info.weaponName = "Spear";
        info.weaponType = WeaponType.StingWeapon;
        weaponInfoList.Add(info);

        info = new WeaponItemInfo();
        info.Uid = 2003;
        info.level = 3;
        info.damage = 10;
        info.damageRate = 0.5f;
        info.attackSpeed = 0.7f;
        info.attackRange = 5f;
        info.stringKey = "???";
        info.weaponSpritePath = "Sprite/Weapon/Weapon_1_3";
        info.weaponName = "Spear";
        info.weaponType = WeaponType.StingWeapon;
        weaponInfoList.Add(info);

        info = new WeaponItemInfo();
        info.Uid = 2004;
        info.level = 4;
        info.damage = 10;
        info.damageRate = 0.5f;
        info.attackSpeed = 0.5f;
        info.attackRange = 6f;
        info.stringKey = "???";
        info.weaponSpritePath = "Sprite/Weapon/Weapon_1_4";
        info.weaponName = "Spear";
        info.weaponType = WeaponType.StingWeapon;
        weaponInfoList.Add(info);


        info = new WeaponItemInfo();
        info.Uid = 2011;
        info.level = 1;
        info.damage = 5;
        info.damageRate = 0.3f;
        info.attackSpeed = 1f;
        info.attackRange = 6f;
        info.stringKey = "??";
        info.weaponSpritePath = "Sprite/Weapon/Weapon_3_1";
        info.weaponName = "Rifle";
        info.weaponType = WeaponType.ShoootingWeapon;
        weaponInfoList.Add(info);

        info = new WeaponItemInfo();
        info.Uid = 2012;
        info.level = 2;
        info.damage = 5;
        info.damageRate = 0.3f;
        info.attackSpeed = 0.7f;
        info.attackRange = 6f;
        info.stringKey = "??";
        info.weaponSpritePath = "Sprite/Weapon/Weapon_3_2";
        info.weaponName = "Rifle";
        info.weaponType = WeaponType.ShoootingWeapon;
        weaponInfoList.Add(info);

        info = new WeaponItemInfo();
        info.Uid = 2013;
        info.level = 3;
        info.damage = 5;
        info.damageRate = 0.3f;
        info.attackSpeed = 0.5f;
        info.attackRange = 6f;
        info.stringKey = "??";
        info.weaponSpritePath = "Sprite/Weapon/Weapon_3_3";
        info.weaponName = "Rifle";
        info.weaponType = WeaponType.ShoootingWeapon;
        weaponInfoList.Add(info);

        info = new WeaponItemInfo();
        info.Uid = 2014;
        info.level = 4;
        info.damage = 5;
        info.damageRate = 0.3f;
        info.attackSpeed = 0.3f;
        info.attackRange = 8f;
        info.stringKey = "??";
        info.weaponSpritePath = "Sprite/Weapon/Weapon_3_4";
        info.weaponName = "Rifle";
        info.weaponType = WeaponType.ShoootingWeapon;
        weaponInfoList.Add(info);

        //info = new WeaponItemInfo();
        //info.Uid = 2002;
        //info.level = 1;
        //info.damage = 5;
        //info.damageRate = 0.3f;
        //info.attackSpeed = 1f;
        //info.attackRange = 2f;
        //info.stringKey = "?? ?";
        //info.weaponSpritePath = "Sprite/Weapon/Weapon_2";
        //info.weaponName = "?? ?";
        //info.weaponType = WeaponType.MowWeapon;
        //weaponInfoArr[2] = info;


        weaponData = new WeaponData();
        weaponData.weaponArr = weaponInfoList.ToArray();
    }

    private void CreateMonsterJson()
    {
        MonsterInfo[] monsterInfoArr = new MonsterInfo[2];
        MonsterInfo info = new MonsterInfo();
        info.Uid = 3000;

        float[] statusArr = new float[(int)MonsterStatus.End];
        statusArr[(int)MonsterStatus.M_Hp] = 10;
        statusArr[(int)MonsterStatus.M_Damage] = 5;
        statusArr[(int)MonsterStatus.M_Speed] = 3;
        statusArr[(int)MonsterStatus.M_AttackSpeed] = 1;
        statusArr[(int)MonsterStatus.M_AttackRange] = 1;

        info.status = statusArr;
        //info.hp = 10;
        //info.damage = 5;
        //info.speed = 3;
        //info.attackSpeed = 1;
        //info.attackRange = 1;
        info.stringKey = "근접 기본몬스터 ";
        info.monsterSpritePath = "Sprite/Monster/CommonMonster_0";
        info.monsterName = "근접 기본몬스터 ";
        info.logicType = BehaviorLogicType.LoopBehavior;
        info.moveType = MonsterMoveBehaviorType.ApproachToTarget;
        info.attackType = MonsterAttackBehaviorType.None;
        monsterInfoArr[0] = info;

        info = new MonsterInfo();
        info.Uid = 3001;

        statusArr = new float[(int)MonsterStatus.End];
        statusArr[(int)MonsterStatus.M_Hp] = 10;
        statusArr[(int)MonsterStatus.M_Damage] = 3;
        statusArr[(int)MonsterStatus.M_Speed] = 3;
        statusArr[(int)MonsterStatus.M_AttackSpeed] = 1;
        statusArr[(int)MonsterStatus.M_AttackRange] = 4;

        info.status = statusArr;

        //info.hp = 10;
        //info.damage = 3;
        //info.speed = 3;
        //info.attackSpeed = 1;
        //info.attackRange = 1;
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
        List<MonsterGroupJsonModel> modelList = new List<MonsterGroupJsonModel>();
        MonsterGroupJsonModel model = new MonsterGroupJsonModel();
        model.Uid = 4000;
        model.monsterId = 3000;
        model.count = 2;
        model.firstSpawnTime = 3f;
        model.reSpawnTime = 3f;
        model.endSpawnTime = 30f;

        modelList.Add(model);

        model = new MonsterGroupJsonModel();
        model.Uid = 4000;
        model.monsterId = 3000;
        model.count = 5;
        model.firstSpawnTime = 1f;
        model.reSpawnTime = 4f;
        model.endSpawnTime = 30f;

        modelList.Add(model);


        model = new MonsterGroupJsonModel();
        model.Uid = 4001;
        model.monsterId = 3000;
        model.count = 5;
        model.firstSpawnTime = 1f;
        model.reSpawnTime = 4f;
        model.endSpawnTime = 30f;

        modelList.Add(model);

        model = new MonsterGroupJsonModel();
        model.Uid = 4001;
        model.monsterId = 3000;
        model.count = 7;
        model.firstSpawnTime = 2f;
        model.reSpawnTime = 5f;
        model.endSpawnTime = 30f;

        modelList.Add(model);


        model = new MonsterGroupJsonModel();
        model.Uid = 4002;
        model.monsterId = 3000;
        model.count = 7;
        model.firstSpawnTime = 2f;
        model.reSpawnTime = 5f;
        model.endSpawnTime = 30f;

        modelList.Add(model);

        model = new MonsterGroupJsonModel();
        model.Uid = 4002;
        model.monsterId = 3001;
        model.count = 1;
        model.firstSpawnTime = 3f;
        model.reSpawnTime = 3f;
        model.endSpawnTime = 30f;

        modelList.Add(model);

        model = new MonsterGroupJsonModel();
        model.Uid = 4003;
        model.monsterId = 3000;
        model.count = 7;
        model.firstSpawnTime = 2f;
        model.reSpawnTime = 5f;
        model.endSpawnTime = 30f;

        modelList.Add(model);

        model = new MonsterGroupJsonModel();
        model.Uid = 4003;
        model.monsterId = 3001;
        model.count = 1;
        model.firstSpawnTime = 3f;
        model.reSpawnTime = 3f;
        model.endSpawnTime = 30f;

        modelList.Add(model);

        model = new MonsterGroupJsonModel();
        model.Uid = 4004;
        model.monsterId = 3000;
        model.count = 7;
        model.firstSpawnTime = 2f;
        model.reSpawnTime = 5f;
        model.endSpawnTime = 30f;

        modelList.Add(model);

        model = new MonsterGroupJsonModel();
        model.Uid = 4004;
        model.monsterId = 3001;
        model.count = 2;
        model.firstSpawnTime = 5f;
        model.reSpawnTime = 4f;
        model.endSpawnTime = 30f;

        modelList.Add(model);

        model = new MonsterGroupJsonModel();
        model.Uid = 4005;
        model.monsterId = 3000;
        model.count = 7;
        model.firstSpawnTime = 2f;
        model.reSpawnTime = 5f;
        model.endSpawnTime = 30f;

        modelList.Add(model);

        model = new MonsterGroupJsonModel();
        model.Uid = 4005;
        model.monsterId = 3001;
        model.count = 3;
        model.firstSpawnTime = 4f;
        model.reSpawnTime = 5f;
        model.endSpawnTime = 30f;

        modelList.Add(model);

        model = new MonsterGroupJsonModel();
        model.Uid = 4006;
        model.monsterId = 3000;
        model.count = 7;
        model.firstSpawnTime = 2f;
        model.reSpawnTime = 5f;
        model.endSpawnTime = 30f;

        modelList.Add(model);

        model = new MonsterGroupJsonModel();
        model.Uid = 4006;
        model.monsterId = 3001;
        model.count = 3;
        model.firstSpawnTime = 4f;
        model.reSpawnTime = 5f;
        model.endSpawnTime = 30f;

        modelList.Add(model);


        monsterGroupData.modelArr = modelList.ToArray();
    }

    private void CreateStageJson()
    {
        List<StageData> stageDataList = new List<StageData>();
        StageData model = new StageData();
        model.Uid = 4100;
        model.waveMonsterGroupId = new int[]
        {
            4000,
            4001,
            4002,
            4003,
            4004,
            4005,
            4006
        };
        model.stageInformation = 0;
        model.bossMonsterId = 0;

        stageDataList.Add(model);

        model = new StageData();
        model.Uid = 4101;
        model.waveMonsterGroupId = new int[]
        {
            4006,
            4005,
            4004,
            4003,
            4002,
            4001
        };
        model.stageInformation = 0;
        model.bossMonsterId = 0;

        stageDataList.Add(model);

        stageGroupData.stageDataArr = stageDataList.ToArray();
    }

    private void CreateAugmenterJson()
    {
        List<AugmenterData> augmenterDataList = new List<AugmenterData>();
        AugmenterData data = new AugmenterData();
        data.Uid = 5001;
        data.augmenterGrade = 1;
        data.augmenterName = "aaaa1";
        data.augmenterSpritePath = "Sprite/Augmenter/BronzeAugmenter";
        data.augmenterContent = "this augment increase aaaa + 1";

        augmenterDataList.Add(data);

        data = new AugmenterData();
        data.Uid = 5002;
        data.augmenterGrade = 2;
        data.augmenterName = "aaaa2";
        data.augmenterSpritePath = "Sprite/Augmenter/SliverAugmenter";
        data.augmenterContent = "this augment increase aaaa + 2";

        augmenterDataList.Add(data);

        data = new AugmenterData();
        data.Uid = 5003;
        data.augmenterGrade = 3;
        data.augmenterName = "aaaa3";
        data.augmenterSpritePath = "Sprite/Augmenter/GoldAugmenter";
        data.augmenterContent = "this augment increase aaaa + 3";

        augmenterDataList.Add(data);






        data = new AugmenterData();
        data.Uid = 5011;
        data.augmenterGrade = 1;
        data.augmenterName = "bbbb1";
        data.augmenterSpritePath = "Sprite/Augmenter/BronzeAugmenter";
        data.augmenterContent = "this augment increase bbbb + 1";

        augmenterDataList.Add(data);

        data = new AugmenterData();
        data.Uid = 5012;
        data.augmenterGrade = 2;
        data.augmenterName = "bbbb2";
        data.augmenterSpritePath = "Sprite/Augmenter/SliverAugmenter";
        data.augmenterContent = "this augment increase bbbb + 2";

        augmenterDataList.Add(data);

        data = new AugmenterData();
        data.Uid = 5013;
        data.augmenterGrade = 3;
        data.augmenterName = "bbbb3";
        data.augmenterSpritePath = "Sprite/Augmenter/GoldAugmenter";
        data.augmenterContent = "this augment increase bbbb + 3";

        augmenterDataList.Add(data);






        data = new AugmenterData();
        data.Uid = 5021;
        data.augmenterGrade = 1;
        data.augmenterName = "cccc1";
        data.augmenterSpritePath = "Sprite/Augmenter/BronzeAugmenter";
        data.augmenterContent = "this augment increase cccc + 1";

        augmenterDataList.Add(data);

        data = new AugmenterData();
        data.Uid = 5022;
        data.augmenterGrade = 2;
        data.augmenterName = "cccc2";
        data.augmenterSpritePath = "Sprite/Augmenter/SliverAugmenter";
        data.augmenterContent = "this augment increase cccc + 2";

        augmenterDataList.Add(data);

        data = new AugmenterData();
        data.Uid = 5023;
        data.augmenterGrade = 3;
        data.augmenterName = "cccc3";
        data.augmenterSpritePath = "Sprite/Augmenter/GoldAugmenter";
        data.augmenterContent = "this augment increase cccc + 3";

        augmenterDataList.Add(data);


        augmenterGroupData.augmenterArr = augmenterDataList.ToArray();
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

        JsonController.WriteJson<MonsterGroupArrJsonModel>(monsterSpwanFileName, monsterGroupData);
    }

    [ContextMenu("To Json Wave Data")]
    private void SaveToJsonWaveData()
    {
        if (waveFileName == "")
        {
            Debug.Log("File Name is null");
            return;
        }

        //JsonController.WriteJson<WaveGroupData>(waveFileName, waveGroupData);
    }

    [ContextMenu("To Json Stage Data")]
    private void SaveToJsonStageData()
    {
        if (stageFileName == "")
        {
            Debug.Log("File Name is null");
            return;
        }

        JsonController.WriteJson<StageArrJsonModel>(stageFileName, stageGroupData);
    }

    [ContextMenu("To Json Augmenter Data")]
    private void SaveToJsonAugmenterData()
    {
        if(augmentFileName == "")
        {
            Debug.Log("File Name is Null");
            return;
        }

        JsonController.WriteJson<AugmenterArrJson>(augmentFileName, augmenterGroupData);
    }

}
