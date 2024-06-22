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
    private string itemFileName = "ItemData";

    private JobArrJson jobData;
    private WeaponArrData weaponData;
    private MonsterInfoArrData monsterData;
    private MonsterGroupArrJson monsterGroupData;
    //private WaveGroupData waveGroupData;
    private StageArrJson stageGroupData;
    private AugmenterArrJson augmenterGroupData;
    private ItemInfoArrJson itemData;


    private void Awake()
    {
        //CreateCharacterJobJson();
        CreateWeaponJson();
        //CreateMonsterJson();
        //CreateMonsterSpwanJson();
        //CreateStageJson();
        //CreateAugmenterJson();
        CreateItemJson();
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
        effect.effectType = CharacterStatusType.P_Damage;
        effect.amount = +5;
        job1.increaseStatus[0] = effect;

        job1.decreaseStatus = new StatusEffect[2];

        effect = new StatusEffect();
        effect.stringKey = "???????? ????";
        effect.effectType = CharacterStatusType.P_AttackSpeed;
        effect.amount = -3;
        job1.decreaseStatus[0] = effect;

        effect = new StatusEffect();
        effect.stringKey = "???? ????";
        effect.effectType = CharacterStatusType.P_Speed;
        effect.amount = -2;
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
        effect.effectType = CharacterStatusType.P_AttackSpeed;
        effect.amount = +5;
        job2.increaseStatus[0] = effect;

        effect = new StatusEffect();
        effect.stringKey = "???? ????";
        effect.effectType = CharacterStatusType.P_Speed;
        effect.amount = +2;
        job2.increaseStatus[1] = effect;

        job2.decreaseStatus = new StatusEffect[1];

        effect = new StatusEffect();
        effect.stringKey = "?????? ????";
        effect.effectType = CharacterStatusType.P_Damage;
        effect.amount = -5;
        job2.decreaseStatus[0] = effect;

        job2.unLockId = 10004;

        job2.stringKey = "???? ????";

        job2.jobSpritePath = "Sprite/Job/Job_1";
        job2.jobName = "???? ????";

        jobArr[1] = job2;


        jobData = new JobArrJson();
        jobData.jobArr = jobArr;
    }

    private void CreateWeaponJson()
    {
        List<WeaponItemInfo> weaponInfoList = new List<WeaponItemInfo>();

        WeaponItemInfo info = new WeaponItemInfo();
        info.Uid = 2001;
        info.itemType = ItemType.WeaponType;
        info.level = 1;
        info.price = 10;
        info.penetrateCount = -1;
        info.damage = 10;
        info.damageRate = 0.5f;
        info.attackSpeed = 1f;
        info.attackRange = 3f;
        info.stringKey = "???";
        info.itemSpritePath = "Sprite/Weapon/Weapon_1_1";
        info.itemName = "Spear";
        info.weaponType = WeaponType.StingWeapon;
        weaponInfoList.Add(info);

        info = new WeaponItemInfo();
        info.Uid = 2002;
        info.itemType = ItemType.WeaponType;
        info.level = 2;
        info.price = 10;
        info.penetrateCount = -1;
        info.damage = 10;
        info.damageRate = 0.5f;
        info.attackSpeed = 0.7f;
        info.attackRange = 3f;
        info.stringKey = "???";
        info.itemSpritePath = "Sprite/Weapon/Weapon_1_2";
        info.itemName = "Spear";
        info.weaponType = WeaponType.StingWeapon;
        weaponInfoList.Add(info);

        info = new WeaponItemInfo();
        info.Uid = 2003;
        info.itemType = ItemType.WeaponType;
        info.level = 3;
        info.price = 10;
        info.penetrateCount = -1;
        info.damage = 10;
        info.damageRate = 0.5f;
        info.attackSpeed = 0.7f;
        info.attackRange = 5f;
        info.stringKey = "???";
        info.itemSpritePath = "Sprite/Weapon/Weapon_1_3";
        info.itemName = "Spear";
        info.weaponType = WeaponType.StingWeapon;
        weaponInfoList.Add(info);

        info = new WeaponItemInfo();
        info.Uid = 2004;
        info.itemType = ItemType.WeaponType;
        info.level = 4;
        info.price = 10;
        info.penetrateCount = -1;
        info.damage = 10;
        info.damageRate = 0.5f;
        info.attackSpeed = 0.5f;
        info.attackRange = 6f;
        info.stringKey = "???";
        info.itemSpritePath = "Sprite/Weapon/Weapon_1_4";
        info.itemName = "Spear";
        info.weaponType = WeaponType.StingWeapon;
        weaponInfoList.Add(info);





        info = new WeaponItemInfo();
        info.Uid = 2005;
        info.itemType = ItemType.WeaponType;
        info.level = 1;
        info.price = 10;
        info.penetrateCount = -1;
        info.damage = 7;
        info.damageRate = 0.5f;
        info.attackSpeed = 0.5f;
        info.attackRange = 3f;
        info.stringKey = "???";
        info.itemSpritePath = "Sprite/Weapon/Weapon_2_1";
        info.itemName = "Shovel";
        info.weaponType = WeaponType.StingWeapon;
        weaponInfoList.Add(info);

        info = new WeaponItemInfo();
        info.Uid = 2006;
        info.itemType = ItemType.WeaponType;
        info.level = 2;
        info.price = 10;
        info.penetrateCount = -1;
        info.damage = 7;
        info.damageRate = 0.5f;
        info.attackSpeed = 0.4f;
        info.attackRange = 3f;
        info.stringKey = "???";
        info.itemSpritePath = "Sprite/Weapon/Weapon_2_2";
        info.itemName = "Shovel";
        info.weaponType = WeaponType.StingWeapon;
        weaponInfoList.Add(info);

        info = new WeaponItemInfo();
        info.Uid = 2007;
        info.itemType = ItemType.WeaponType;
        info.level = 3;
        info.price = 10;
        info.penetrateCount = -1;
        info.damage = 8;
        info.damageRate = 0.5f;
        info.attackSpeed = 0.3f;
        info.attackRange = 4f;
        info.stringKey = "???";
        info.itemSpritePath = "Sprite/Weapon/Weapon_2_3";
        info.itemName = "Shovel";
        info.weaponType = WeaponType.StingWeapon;
        weaponInfoList.Add(info);

        info = new WeaponItemInfo();
        info.Uid = 2008;
        info.itemType = ItemType.WeaponType;
        info.level = 4;
        info.price = 10;
        info.penetrateCount = -1;
        info.damage = 9;
        info.damageRate = 0.5f;
        info.attackSpeed = 0.2f;
        info.attackRange = 4f;
        info.stringKey = "???";
        info.itemSpritePath = "Sprite/Weapon/Weapon_2_4";
        info.itemName = "Shovel";
        info.weaponType = WeaponType.StingWeapon;
        weaponInfoList.Add(info);






        info = new WeaponItemInfo();
        info.Uid = 2011;
        info.itemType = ItemType.WeaponType;
        info.level = 1;
        info.price = 10;
        info.penetrateCount = 3;
        info.damage = 7;
        info.damageRate = 0.3f;
        info.attackSpeed = 1f;
        info.attackRange = 6f;
        info.stringKey = "??";
        info.itemSpritePath = "Sprite/Weapon/Weapon_3_1";
        info.itemName = "Rifle";
        info.weaponType = WeaponType.ShoootingWeapon;
        weaponInfoList.Add(info);

        info = new WeaponItemInfo();
        info.Uid = 2012;
        info.itemType = ItemType.WeaponType;
        info.level = 2;
        info.price = 10;
        info.penetrateCount = 4;
        info.damage = 7;
        info.damageRate = 0.3f;
        info.attackSpeed = 0.7f;
        info.attackRange = 6f;
        info.stringKey = "??";
        info.itemSpritePath = "Sprite/Weapon/Weapon_3_2";
        info.itemName = "Rifle";
        info.weaponType = WeaponType.ShoootingWeapon;
        weaponInfoList.Add(info);

        info = new WeaponItemInfo();
        info.Uid = 2013;
        info.itemType = ItemType.WeaponType;
        info.level = 3;
        info.price = 10;
        info.penetrateCount = 5;
        info.damage = 7;
        info.damageRate = 0.3f;
        info.attackSpeed = 0.5f;
        info.attackRange = 6f;
        info.stringKey = "??";
        info.itemSpritePath = "Sprite/Weapon/Weapon_3_3";
        info.itemName = "Rifle";
        info.weaponType = WeaponType.ShoootingWeapon;
        weaponInfoList.Add(info);

        info = new WeaponItemInfo();
        info.Uid = 2014;
        info.itemType = ItemType.WeaponType;
        info.level = 4;
        info.price = 10;
        info.penetrateCount = 6;
        info.damage = 7;
        info.damageRate = 0.3f;
        info.attackSpeed = 0.3f;
        info.attackRange = 7f;
        info.stringKey = "??";
        info.itemSpritePath = "Sprite/Weapon/Weapon_3_4";
        info.itemName = "Rifle";
        info.weaponType = WeaponType.ShoootingWeapon;
        weaponInfoList.Add(info);






        info = new WeaponItemInfo();
        info.Uid = 2015;
        info.itemType = ItemType.WeaponType;
        info.level = 1;
        info.price = 10;
        info.penetrateCount = 0;
        info.damage = 2;
        info.damageRate = 0.3f;
        info.attackSpeed = 0.3f;
        info.attackRange = 7f;
        info.stringKey = "??";
        info.itemSpritePath = "Sprite/Weapon/Weapon_4_1";
        info.itemName = "Thompson Submachine Gun";
        info.weaponType = WeaponType.ShoootingWeapon;
        weaponInfoList.Add(info);

        info = new WeaponItemInfo();
        info.Uid = 2016;
        info.itemType = ItemType.WeaponType;
        info.level = 2;
        info.price = 10;
        info.penetrateCount = 1;
        info.damage = 3;
        info.damageRate = 0.3f;
        info.attackSpeed = 0.3f;
        info.attackRange = 7f;
        info.stringKey = "??";
        info.itemSpritePath = "Sprite/Weapon/Weapon_4_2";
        info.itemName = "Thompson Submachine Gun";
        info.weaponType = WeaponType.ShoootingWeapon;
        weaponInfoList.Add(info);

        info = new WeaponItemInfo();
        info.Uid = 2017;
        info.itemType = ItemType.WeaponType;
        info.level = 3;
        info.price = 10;
        info.penetrateCount = 1;
        info.damage = 4;
        info.damageRate = 0.3f;
        info.attackSpeed = 0.2f;
        info.attackRange = 7f;
        info.stringKey = "??";
        info.itemSpritePath = "Sprite/Weapon/Weapon_4_3";
        info.itemName = "Thompson Submachine Gun";
        info.weaponType = WeaponType.ShoootingWeapon;
        weaponInfoList.Add(info);

        info = new WeaponItemInfo();
        info.Uid = 2018;
        info.itemType = ItemType.WeaponType;
        info.level = 4;
        info.price = 10;
        info.penetrateCount = 2;
        info.damage = 5;
        info.damageRate = 0.3f;
        info.attackSpeed = 0.1f;
        info.attackRange = 7f;
        info.stringKey = "??";
        info.itemSpritePath = "Sprite/Weapon/Weapon_4_4";
        info.itemName = "Thompson Submachine Gun";
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


        weaponData = new WeaponArrData();
        weaponData.weaponArr = weaponInfoList.ToArray();
    }

    private void CreateMonsterJson()
    {
        MonsterInfo[] monsterInfoArr = new MonsterInfo[2];
        MonsterInfo info = new MonsterInfo();
        info.Uid = 3000;

        float[] statusArr = new float[(int)MonsterStatusType.End];
        statusArr[(int)MonsterStatusType.M_HP] = 10;
        statusArr[(int)MonsterStatusType.M_Damage] = 1;
        statusArr[(int)MonsterStatusType.M_Speed] = 2;
        statusArr[(int)MonsterStatusType.M_AttackSpeed] = 1;
        statusArr[(int)MonsterStatusType.M_AttackRange] = 1;

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

        statusArr = new float[(int)MonsterStatusType.End];
        statusArr[(int)MonsterStatusType.M_HP] = 10;
        statusArr[(int)MonsterStatusType.M_Damage] = 1;
        statusArr[(int)MonsterStatusType.M_Speed] = 1.5f;
        statusArr[(int)MonsterStatusType.M_AttackSpeed] = 3;
        statusArr[(int)MonsterStatusType.M_AttackRange] = 4;

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

        monsterData = new MonsterInfoArrData();
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
            4000,
            4001,
            4001,
            4002,
            4002,
            4003,
            4003,
            4004,
            4004,
            4005,
            4005,
            4006,
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
            4006,
            4005,
            4005,
            4004,
            4004,
            4003,
            4003,
            4002,
            4002,
            4001,
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
        data.groupId = 0;
        data.augmenterGrade = 1;
        data.augmenterName = "aaaa1";
        data.augmenterSpritePath = "Sprite/Augmenter/BronzeAugmenter";
        data.augmenterContent = "this augment increase aaaa + 1";
        data.isDuplicat = true;
        data.firstType = AugmenterType.MonsterSpawnTime;
        data.firstTypeValue = 1;
        data.secondType = AugmenterType.None;
        data.secondTypeValue = 0;

        augmenterDataList.Add(data);

        data = new AugmenterData();
        data.Uid = 5002;
        data.groupId = 0;
        data.augmenterGrade = 2;
        data.augmenterName = "aaaa2";
        data.augmenterSpritePath = "Sprite/Augmenter/SliverAugmenter";
        data.augmenterContent = "this augment increase aaaa + 2";
        data.isDuplicat = true;
        data.firstType = AugmenterType.MonsterSpawnTime;
        data.firstTypeValue = 2;
        data.secondType = AugmenterType.None;
        data.secondTypeValue = 0;

        augmenterDataList.Add(data);

        data = new AugmenterData();
        data.Uid = 5003;
        data.groupId = 0;
        data.augmenterGrade = 3;
        data.augmenterName = "aaaa3";
        data.augmenterSpritePath = "Sprite/Augmenter/GoldAugmenter";
        data.augmenterContent = "this augment increase aaaa + 3";
        data.isDuplicat = true;
        data.firstType = AugmenterType.MonsterSpawnTime;
        data.firstTypeValue = 3;
        data.secondType = AugmenterType.None;
        data.secondTypeValue = 0;

        augmenterDataList.Add(data);






        data = new AugmenterData();
        data.Uid = 5004;
        data.groupId = 1;
        data.augmenterGrade = 1;
        data.augmenterName = "bbbb1";
        data.augmenterSpritePath = "Sprite/Augmenter/BronzeAugmenter";
        data.augmenterContent = "this augment increase bbbb + 1";
        data.isDuplicat = true;
        data.firstType = AugmenterType.P_MaxHP;
        data.firstTypeValue = 1;
        data.secondType = AugmenterType.None;
        data.secondTypeValue = 0;

        augmenterDataList.Add(data);

        data = new AugmenterData();
        data.Uid = 5005;
        data.groupId = 1;
        data.augmenterGrade = 2;
        data.augmenterName = "bbbb2";
        data.augmenterSpritePath = "Sprite/Augmenter/SliverAugmenter";
        data.augmenterContent = "this augment increase bbbb + 2";
        data.isDuplicat = true;
        data.firstType = AugmenterType.P_MaxHP;
        data.firstTypeValue = 2;
        data.secondType = AugmenterType.None;
        data.secondTypeValue = 0;

        augmenterDataList.Add(data);

        data = new AugmenterData();
        data.Uid = 5006;
        data.groupId = 1;
        data.augmenterGrade = 3;
        data.augmenterName = "bbbb3";
        data.augmenterSpritePath = "Sprite/Augmenter/GoldAugmenter";
        data.augmenterContent = "this augment increase bbbb + 3";
        data.isDuplicat = true;
        data.firstType = AugmenterType.P_MaxHP;
        data.firstTypeValue = 3;
        data.secondType = AugmenterType.None;
        data.secondTypeValue = 0;

        augmenterDataList.Add(data);






        data = new AugmenterData();
        data.Uid = 5007;
        data.groupId = 2;
        data.augmenterGrade = 1;
        data.augmenterName = "cccc1";
        data.augmenterSpritePath = "Sprite/Augmenter/BronzeAugmenter";
        data.augmenterContent = "this augment increase cccc + 1";
        data.isDuplicat = true;
        data.firstType = AugmenterType.M_HP;
        data.firstTypeValue = 1;
        data.secondType = AugmenterType.None;
        data.secondTypeValue = 0;

        augmenterDataList.Add(data);

        data = new AugmenterData();
        data.Uid = 5008;
        data.groupId = 2;
        data.augmenterGrade = 2;
        data.augmenterName = "cccc2";
        data.augmenterSpritePath = "Sprite/Augmenter/SliverAugmenter";
        data.augmenterContent = "this augment increase cccc + 2";
        data.isDuplicat = true;
        data.firstType = AugmenterType.M_HP;
        data.firstTypeValue = 2;
        data.secondType = AugmenterType.None;
        data.secondTypeValue = 0;

        augmenterDataList.Add(data);

        data = new AugmenterData();
        data.Uid = 5009;
        data.groupId = 2;
        data.augmenterGrade = 3;
        data.augmenterName = "cccc3";
        data.augmenterSpritePath = "Sprite/Augmenter/GoldAugmenter";
        data.augmenterContent = "this augment increase cccc + 3";
        data.isDuplicat = true;
        data.firstType = AugmenterType.M_HP;
        data.firstTypeValue = 3;
        data.secondType = AugmenterType.None;
        data.secondTypeValue = 0;

        augmenterDataList.Add(data);




        data = new AugmenterData();
        data.Uid = 5010;
        data.groupId = 3;
        data.augmenterGrade = 1;
        data.augmenterName = "dddd1";
        data.augmenterSpritePath = "Sprite/Augmenter/BronzeAugmenter";
        data.augmenterContent = "this augment increase dddd + 1";
        data.isDuplicat = false;
        data.firstType = AugmenterType.M_HP;
        data.firstTypeValue = 1;
        data.secondType = AugmenterType.None;
        data.secondTypeValue = 0;

        augmenterDataList.Add(data);

        data = new AugmenterData();
        data.Uid = 5011;
        data.groupId = 3;
        data.augmenterGrade = 2;
        data.augmenterName = "dddd2";
        data.augmenterSpritePath = "Sprite/Augmenter/SliverAugmenter";
        data.augmenterContent = "this augment increase dddd + 2";
        data.isDuplicat = false;
        data.firstType = AugmenterType.M_HP;
        data.firstTypeValue = 2;
        data.secondType = AugmenterType.None;
        data.secondTypeValue = 0;

        augmenterDataList.Add(data);

        data = new AugmenterData();
        data.Uid = 5012;
        data.groupId = 3;
        data.augmenterGrade = 3;
        data.augmenterName = "dddd3";
        data.augmenterSpritePath = "Sprite/Augmenter/GoldAugmenter";
        data.augmenterContent = "this augment increase dddd + 3";
        data.isDuplicat = false;
        data.firstType = AugmenterType.M_HP;
        data.firstTypeValue = 3;
        data.secondType = AugmenterType.None;
        data.secondTypeValue = 0;

        augmenterDataList.Add(data);


        augmenterGroupData.augmenterArr = augmenterDataList.ToArray();
    }

    private void CreateItemJson()
    {
        List<PassiveItemInfo> itemInfoList = new List<PassiveItemInfo>();

        PassiveItemInfo info = new PassiveItemInfo();
        info.Uid = 6001;
        info.itemType = ItemType.PassiveType;
        info.price = 10;
        info.itemSpritePath = "Sprite/Item/Item_0";
        info.itemName = "HP";
        info.itemContent = "Increase player HP and Monster HP";

        List<PassiveItemEffect> itemEffectList = new List<PassiveItemEffect>();

        PassiveItemEffect effect = new PassiveItemEffect();
        effect.statusEffectType = PassiveItemEffectType.P_MaxHP;
        effect.amount = +3f;
        itemEffectList.Add(effect);

        effect = new PassiveItemEffect();
        effect.statusEffectType = PassiveItemEffectType.M_HP;
        effect.amount = +3f;
        itemEffectList.Add(effect);

        info.itemEffectArr = itemEffectList.ToArray();
        itemInfoList.Add(info);


        info = new PassiveItemInfo();
        info.Uid = 6002;
        info.itemType = ItemType.PassiveType;
        info.price = 10;
        info.itemSpritePath = "Sprite/Item/Item_1";
        info.itemName = "Up Attack Speed";
        info.itemContent = "Increase player Attack Speed and Decrease player Damage";

        itemEffectList = new List<PassiveItemEffect>();

        effect = new PassiveItemEffect();
        effect.statusEffectType = PassiveItemEffectType.P_AttackSpeed;
        effect.amount = +3f;
        itemEffectList.Add(effect);

        effect = new PassiveItemEffect();
        effect.statusEffectType = PassiveItemEffectType.P_Damage;
        effect.amount = -2f;
        itemEffectList.Add(effect);

        info.itemEffectArr = itemEffectList.ToArray();
        itemInfoList.Add(info);



        info = new PassiveItemInfo();
        info.Uid = 6003;
        info.itemType = ItemType.PassiveType;
        info.price = 10;
        info.itemSpritePath = "Sprite/Item/Item_2";
        info.itemName = "Up Damage";
        info.itemContent = "Increase player Damage and Decrease player Speed";

        itemEffectList = new List<PassiveItemEffect>();

        effect = new PassiveItemEffect();
        effect.statusEffectType = PassiveItemEffectType.P_Damage;
        effect.amount = +3f;
        itemEffectList.Add(effect);

        effect = new PassiveItemEffect();
        effect.statusEffectType = PassiveItemEffectType.P_Speed;
        effect.amount = -2f;
        itemEffectList.Add(effect);

        info.itemEffectArr = itemEffectList.ToArray();
        itemInfoList.Add(info);



        info = new PassiveItemInfo();
        info.Uid = 6004;
        info.itemType = ItemType.PassiveType;
        info.price = 10;
        info.itemSpritePath = "Sprite/Item/Item_3";
        info.itemName = "Up Range";
        info.itemContent = "Increase player Attack Range and Monster Damage";

        itemEffectList = new List<PassiveItemEffect>();

        effect = new PassiveItemEffect();
        effect.statusEffectType = PassiveItemEffectType.P_AttackRange;
        effect.amount = +1f;
        itemEffectList.Add(effect);

        effect = new PassiveItemEffect();
        effect.statusEffectType = PassiveItemEffectType.M_Damage;
        effect.amount = +2f;
        itemEffectList.Add(effect);

        info.itemEffectArr = itemEffectList.ToArray();
        itemInfoList.Add(info);



        info = new PassiveItemInfo();
        info.Uid = 6005;
        info.itemType = ItemType.PassiveType;
        info.price = 10;
        info.itemSpritePath = "Sprite/Item/Item_4";
        info.itemName = "Up Speed";
        info.itemContent = "Increase player Speed and Decrease player HP";

        itemEffectList = new List<PassiveItemEffect>();

        effect = new PassiveItemEffect();
        effect.statusEffectType = PassiveItemEffectType.P_Speed;
        effect.amount = +3f;
        itemEffectList.Add(effect);

        effect = new PassiveItemEffect();
        effect.statusEffectType = PassiveItemEffectType.P_MaxHP;
        effect.amount = -2f;
        itemEffectList.Add(effect);

        info.itemEffectArr = itemEffectList.ToArray();
        itemInfoList.Add(info);



        info = new PassiveItemInfo();
        info.Uid = 6006;
        info.itemType = ItemType.PassiveType;
        info.price = 10;
        info.itemSpritePath = "Sprite/Item/Item_5";
        info.itemName = "SuperUP Damage Attack Speed";
        info.itemContent = "Increase player Damage and Attack Speed";

        itemEffectList = new List<PassiveItemEffect>();

        effect = new PassiveItemEffect();
        effect.statusEffectType = PassiveItemEffectType.P_Damage;
        effect.amount = +1f;
        itemEffectList.Add(effect);

        effect = new PassiveItemEffect();
        effect.statusEffectType = PassiveItemEffectType.P_AttackSpeed;
        effect.amount = +1f;
        itemEffectList.Add(effect);

        info.itemEffectArr = itemEffectList.ToArray();
        itemInfoList.Add(info);



        info = new PassiveItemInfo();
        info.Uid = 6007;
        info.itemType = ItemType.PassiveType;
        info.price = 10;
        info.itemSpritePath = "Sprite/Item/Item_6";
        info.itemName = "SuperUp Speed HP";
        info.itemContent = "Increase player Speed and HP";

        itemEffectList = new List<PassiveItemEffect>();

        effect = new PassiveItemEffect();
        effect.statusEffectType = PassiveItemEffectType.P_Speed;
        effect.amount = +1f;
        itemEffectList.Add(effect);

        effect = new PassiveItemEffect();
        effect.statusEffectType = PassiveItemEffectType.P_MaxHP;
        effect.amount = +1f;
        itemEffectList.Add(effect);

        info.itemEffectArr = itemEffectList.ToArray();
        itemInfoList.Add(info);



        info = new PassiveItemInfo();
        info.Uid = 6008;
        info.itemType = ItemType.PassiveType;
        info.price = 10;
        info.itemSpritePath = "Sprite/Item/Item_7";
        info.itemName = "Up Attack Speed";
        info.itemContent = "Increase player Attack Speed and Monster Damage";

        itemEffectList = new List<PassiveItemEffect>();

        effect = new PassiveItemEffect();
        effect.statusEffectType = PassiveItemEffectType.P_AttackSpeed;
        effect.amount = +3f;
        itemEffectList.Add(effect);

        effect = new PassiveItemEffect();
        effect.statusEffectType = PassiveItemEffectType.M_Damage;
        effect.amount = +2;
        itemEffectList.Add(effect);

        info.itemEffectArr = itemEffectList.ToArray();
        itemInfoList.Add(info);



        info = new PassiveItemInfo();
        info.Uid = 6009;
        info.itemType = ItemType.PassiveType;
        info.price = 10;
        info.itemSpritePath = "Sprite/Item/Item_8";
        info.itemName = "Up HP";
        info.itemContent = "Increase player HP and Monster Damage";

        itemEffectList = new List<PassiveItemEffect>();

        effect = new PassiveItemEffect();
        effect.statusEffectType = PassiveItemEffectType.P_MaxHP;
        effect.amount = +3f;
        itemEffectList.Add(effect);

        effect = new PassiveItemEffect();
        effect.statusEffectType = PassiveItemEffectType.M_Damage;
        effect.amount = +2f;
        itemEffectList.Add(effect);

        info.itemEffectArr = itemEffectList.ToArray();
        itemInfoList.Add(info);



        info = new PassiveItemInfo();
        info.Uid = 6010;
        info.itemType = ItemType.PassiveType;
        info.price = 10;
        info.itemSpritePath = "Sprite/Item/Item_9";
        info.itemName = "Down Damage";
        info.itemContent = "Decrease player Damage and Decrease Monster HP";

        itemEffectList = new List<PassiveItemEffect>();

        effect = new PassiveItemEffect();
        effect.statusEffectType = PassiveItemEffectType.P_Damage;
        effect.amount = -3f;
        itemEffectList.Add(effect);

        effect = new PassiveItemEffect();
        effect.statusEffectType = PassiveItemEffectType.M_HP;
        effect.amount = -2f;
        itemEffectList.Add(effect);

        info.itemEffectArr = itemEffectList.ToArray();
        itemInfoList.Add(info);


        itemData.itemInfoArr = itemInfoList.ToArray();

    }




    [ContextMenu("To Json Character Job Data")]  // ???????? ?????? ???? ?????? ???????? To Json Data ???? ???????? ?????? 
    private void SaveToJsonCharacterJobData()
    {
        if (jobFileName == "")
        {
            Debug.Log("???? ???? ???? ");
            return;
        }
        JsonController.WriteJson<JobArrJson>(jobFileName, jobData);
    }


    [ContextMenu("To Json Weapon Data")]  // ???????? ?????? ???? ?????? ???????? To Json Data ???? ???????? ?????? 
    private void SaveToJsonWeaponData()
    {
        if (weaponFileName == "")
        {
            Debug.Log("???? ???? ???? ");
            return;
        }
        JsonController.WriteJson<WeaponArrData>(weaponFileName, weaponData);
    }

    [ContextMenu("To Json Monster Data")]

    private void SaveToJsonMonsterData()
    {
        if (monsterFileName == "")
        {
            Debug.Log("???? ???? ???? ");
            return;
        }
        JsonController.WriteJson<MonsterInfoArrData>(monsterFileName, monsterData);

    }

    [ContextMenu("To Json MonsterSpwan Data")]
    private void SaveToJsonMonsterSpwanData()
    {
        if(monsterSpwanFileName == "")
        {
            Debug.Log("File Name is null");
            return;
        }

        JsonController.WriteJson<MonsterGroupArrJson>(monsterSpwanFileName, monsterGroupData);
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

        JsonController.WriteJson<StageArrJson>(stageFileName, stageGroupData);
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

    [ContextMenu("To Json Item Data")]
    private void SaveToJsonItemData()
    {
        if (itemFileName == "")
        {
            Debug.Log("File Name is Null");
            return;
        }

        JsonController.WriteJson<ItemInfoArrJson>(itemFileName, itemData);
    }

}
