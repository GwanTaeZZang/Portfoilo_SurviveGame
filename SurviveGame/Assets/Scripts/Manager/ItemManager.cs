using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    private const int PLAYER_TARGET_NUM = 2;
    private const int MONSTER_TARGET_NUM = 3;

    private Dictionary<int, BaseItemInfo> itemDict = new Dictionary<int, BaseItemInfo>();
    private Dictionary<int, WeaponItemInfo> weaponItemDict = new Dictionary<int, WeaponItemInfo>();
    private Dictionary<int, PassiveItemInfo> passiveItemDict = new Dictionary<int, PassiveItemInfo>();

    private Dictionary<int, Sprite> itemSpriteDict = new Dictionary<int, Sprite>();

    private Dictionary<WeaponType, Queue<WeaponBase>> weaponInstanceDict = new Dictionary<WeaponType, Queue<WeaponBase>>();

    private List<BaseItemInfo> itemInfoList = new List<BaseItemInfo>();
    private List<WeaponItemInfo> weaponItemList = new List<WeaponItemInfo>();
    private List<PassiveItemInfo> passiveItemList = new List<PassiveItemInfo>();
    //private List<Sprite> weaponItemSpriteList = new List<Sprite>();
    private WeaponItemInfo selectedWeapon;

    private WeaponBase[] weaponBaseArr = new WeaponBase[(int)WeaponType.End];

    private WeaponItemInfo[] playerEquipWeaponArr = new WeaponItemInfo[6];
    private List<PassiveItemInfo> playerEquipPassiveItemList = new List<PassiveItemInfo>();

    public delegate void WeaponEquipEvent(WeaponItemInfo _weaponInfo, int _idx);
    public WeaponEquipEvent OnEquipWeapon;

    public delegate void WeaponUnEquipEvent(int _idx);
    public WeaponUnEquipEvent OnUnEquipWeapon;

    public override bool Initialize()
    {
        LoadWeaponData();
        LoadPassiveItemData();
        SaveItemSprite();
        SetWeaponBaseArr();

        return true;
    }

    public WeaponItemInfo GetWeaponInfoValue(int _key)
    {
        if (!weaponItemDict.ContainsKey(_key))
        {
            Debug.Log("have not key");
            return null;
        }

        return weaponItemDict[_key];


        //if (jobDict.TryGetValue(_key, out Job value))
        //{
        //    return value;
        //}
    }

    public List<WeaponItemInfo> GetWeaponList()
    {
        return weaponItemList;
    }

    public List<BaseItemInfo> GetItemInfoList()
    {
        return itemInfoList;
    }

    public WeaponBase GetWeaponInstance(WeaponType _key)
    {
        WeaponBase instance;

        if (!weaponInstanceDict.ContainsKey(_key))
        {
            weaponInstanceDict.Add(_key, new Queue<WeaponBase>());
        }

        if (weaponInstanceDict.ContainsKey(_key))
        {
            if (weaponInstanceDict[_key].Count == 0)
            {
                instance = weaponBaseArr[(int)_key].DeepCopy();
                weaponInstanceDict[_key].Enqueue(instance);
            }
        }

        instance = weaponInstanceDict[_key].Dequeue();

        return instance;
    }

    public void ReleaseWeaponInstance(WeaponType _type, WeaponBase _weaponBase)
    {
        weaponInstanceDict[_type].Enqueue(_weaponBase);
    }


    public void EquipWeapon(WeaponItemInfo _itemInfo, int _idx)
    {
        playerEquipWeaponArr[_idx] = _itemInfo;
        OnEquipWeapon?.Invoke(_itemInfo, _idx);
    }

    public void UnEquipWeapon(int _idx)
    {
        playerEquipWeaponArr[_idx] = null;
        OnUnEquipWeapon?.Invoke(_idx);
    }

    public void EquipPassiveItem(PassiveItemInfo _itemInfo)
    {
        playerEquipPassiveItemList.Add(_itemInfo);

        int count = _itemInfo.itemEffectArr.Length;
        for(int i =0; i < count; i++)
        {
            PassiveItemEffect effect = _itemInfo.itemEffectArr[i];
            int effectType = (int)effect.statusEffectType;

            int targetNum = effectType / 100;
            int targetStatus = effectType % (targetNum * 100);

            if(targetNum == PLAYER_TARGET_NUM)
            {
                PlayerManager.getInstance.UpdateCharacterStatus((CharacterStatusType)targetStatus, effect.amount);
            }
            else if(targetNum == MONSTER_TARGET_NUM)
            {
                MonsterManager.getInstance.UpdateMonsterStatus((MonsterStatusType)targetStatus, effect.amount);
            }
        }
    }

    public Sprite GetItemSprite(int _Uid)
    {
        if (!itemSpriteDict.ContainsKey(_Uid))
        {
            Debug.Log("have not key");
            return null;
        }

        return itemSpriteDict[_Uid];
    }

    public void SetSelectedWeapon(WeaponItemInfo _selectedWeapon)
    {
        selectedWeapon = _selectedWeapon;
        playerEquipWeaponArr[0] = _selectedWeapon;
    }

    public WeaponItemInfo GetSelectedWeapon()
    {
        return selectedWeapon;
    }

    public WeaponItemInfo[] GetEquipmentWeaponArr()
    {
        return playerEquipWeaponArr;
    }

    public List<PassiveItemInfo> GetEquipPassiveItemList()
    {
        return playerEquipPassiveItemList;
    }

    //public WeaponItemInfo DeepCopyWeaponItemInfo(WeaponItemInfo _weaponItemInfo)
    //{
    //    WeaponItemInfo newWeaponItemInfo = new WeaponItemInfo();

    //    newWeaponItemInfo.Uid = _weaponItemInfo.Uid;
    //    newWeaponItemInfo.level = _weaponItemInfo.level;
    //    newWeaponItemInfo.damage = _weaponItemInfo.damage;
    //    newWeaponItemInfo.damageRate = _weaponItemInfo.damageRate;
    //    newWeaponItemInfo.attackSpeed = _weaponItemInfo.attackSpeed;
    //    newWeaponItemInfo.attackRange = _weaponItemInfo.attackRange;
    //    newWeaponItemInfo.stringKey = _weaponItemInfo.stringKey;
    //    newWeaponItemInfo.weaponSpritePath = _weaponItemInfo.weaponSpritePath;
    //    newWeaponItemInfo.weaponName = _weaponItemInfo.weaponName;
    //    newWeaponItemInfo.weaponType = _weaponItemInfo.weaponType;

    //    //newWeaponItemInfo = _weaponItemInfo;

    //    return newWeaponItemInfo;
    //}

    private void LoadWeaponData()
    {
        WeaponArrData weaponData = JsonController.ReadJson<WeaponArrData>("WeaponData");
        int count = weaponData.weaponArr.Length;
        for (int i = 0; i < count; i++)
        {
            WeaponItemInfo weapon = weaponData.weaponArr[i];
            weaponItemDict.Add(weapon.Uid, weapon);
            weaponItemList.Add(weapon);

            itemDict.Add(weapon.Uid, weapon);
            itemInfoList.Add(weapon);
        }
    }

    private void LoadPassiveItemData()
    {
        ItemInfoArrJson passiveItemData = JsonController.ReadJson<ItemInfoArrJson>("ItemData");
        int count = passiveItemData.itemInfoArr.Length;
        for(int i = 0; i < count; i++)
        {
            PassiveItemInfo passiveItem = passiveItemData.itemInfoArr[i];
            passiveItemDict.Add(passiveItem.Uid, passiveItem);
            passiveItemList.Add(passiveItem);

            itemDict.Add(passiveItem.Uid, passiveItem);
            itemInfoList.Add(passiveItem);
        }
    }

    private void SaveItemSprite()
    {
        int count = weaponItemList.Count;

        for (int i = 0; i < count; i++)
        {
            //weaponItemSpriteList.Add(Resources.Load<Sprite>(weaponItemList[i].weaponSpritePath));
            Sprite sprite = Resources.Load<Sprite>(weaponItemList[i].itemSpritePath);
            itemSpriteDict.Add(weaponItemList[i].Uid, sprite);
        }

        count = passiveItemList.Count;

        for(int i = 0; i < count; i++)
        {
            Sprite sprite = Resources.Load<Sprite>(passiveItemList[i].itemSpritePath);
            itemSpriteDict.Add(passiveItemList[i].Uid, sprite);

        }
    }

    private void SetWeaponBaseArr()
    {
        StingWeapon stingWeapon = new StingWeapon();
        ShootingWeapon shootingWeapon = new ShootingWeapon();
        MowWeapon mowWeapon = new MowWeapon();

        weaponBaseArr[(int)WeaponType.StingWeapon] = stingWeapon;
        weaponBaseArr[(int)WeaponType.MowWeapon] = mowWeapon;
        weaponBaseArr[(int)WeaponType.ShoootingWeapon] = shootingWeapon;
    }

}
