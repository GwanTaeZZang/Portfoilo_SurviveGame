using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    private Dictionary<int, WeaponItemInfo> weaponItemDict = new Dictionary<int, WeaponItemInfo>();
    private Dictionary<int, Sprite> weaponItemSpriteDict = new Dictionary<int, Sprite>();
    private Dictionary<WeaponType, Queue<WeaponBase>> weaponInstanceDict = new Dictionary<WeaponType, Queue<WeaponBase>>();
    private List<WeaponItemInfo> weaponItemList = new List<WeaponItemInfo>();
    //private List<Sprite> weaponItemSpriteList = new List<Sprite>();
    private WeaponItemInfo selectedWeapon;

    private WeaponBase[] weaponBaseArr = new WeaponBase[(int)WeaponType.End];

    private WeaponItemInfo[] playerEquipWeaponArr = new WeaponItemInfo[6];

    public delegate void WeaponEquipEvent(WeaponItemInfo _weaponInfo, int _idx);
    public WeaponEquipEvent OnEquipWeapon;

    public delegate void WeaponUnEquipEvent(int _idx);
    public WeaponUnEquipEvent OnUnEquipWeapon;

    public override bool Initialize()
    {
        LoadWeaponData();
        SaveWeaponSprite();
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
        playerEquipWeaponArr[_idx] = DeepCopyWeaponItemInfo(_itemInfo);
        OnEquipWeapon?.Invoke(DeepCopyWeaponItemInfo(_itemInfo), _idx);
    }

    public void UnEquipWeapon(int _idx)
    {
        playerEquipWeaponArr[_idx] = null;
        OnUnEquipWeapon?.Invoke(_idx);
    }

    public Sprite GetWeaponSprite(int _Uid)
    {
        if (!weaponItemSpriteDict.ContainsKey(_Uid))
        {
            Debug.Log("have not key");
            return null;
        }

        return weaponItemSpriteDict[_Uid];
    }

    public void SetSelectedWeapon(WeaponItemInfo _selectedWeapon)
    {
        selectedWeapon = DeepCopyWeaponItemInfo(_selectedWeapon);
        playerEquipWeaponArr[0] = selectedWeapon;
    }

    public WeaponItemInfo GetSelectedWeapon()
    {
        return selectedWeapon;
    }

    public WeaponItemInfo[] GetEquipmentWeaponArr()
    {
        return playerEquipWeaponArr;
    }

    public WeaponItemInfo DeepCopyWeaponItemInfo(WeaponItemInfo _weaponItemInfo)
    {
        WeaponItemInfo newWeaponItemInfo = new WeaponItemInfo();

        newWeaponItemInfo.Uid = _weaponItemInfo.Uid;
        newWeaponItemInfo.level = _weaponItemInfo.level;
        newWeaponItemInfo.damage = _weaponItemInfo.damage;
        newWeaponItemInfo.damageRate = _weaponItemInfo.damageRate;
        newWeaponItemInfo.attackSpeed = _weaponItemInfo.attackSpeed;
        newWeaponItemInfo.attackRange = _weaponItemInfo.attackRange;
        newWeaponItemInfo.stringKey = _weaponItemInfo.stringKey;
        newWeaponItemInfo.weaponSpritePath = _weaponItemInfo.weaponSpritePath;
        newWeaponItemInfo.weaponName = _weaponItemInfo.weaponName;
        newWeaponItemInfo.weaponType = _weaponItemInfo.weaponType;

        //newWeaponItemInfo = _weaponItemInfo;

        return newWeaponItemInfo;
    }

    private void LoadWeaponData()
    {
        WeaponData weaponData = JsonController.ReadJson<WeaponData>("WeaponData");
        int count = weaponData.weaponArr.Length;
        for (int i = 0; i < count; i++)
        {
            WeaponItemInfo weapon = weaponData.weaponArr[i];
            weaponItemDict.Add(weapon.Uid, weapon);
            weaponItemList.Add(weapon);
        }
    }

    private void SaveWeaponSprite()
    {
        int count = weaponItemList.Count;

        for (int i = 0; i < count; i++)
        {
            //weaponItemSpriteList.Add(Resources.Load<Sprite>(weaponItemList[i].weaponSpritePath));
            Sprite sprite = Resources.Load<Sprite>(weaponItemList[i].weaponSpritePath);
            weaponItemSpriteDict.Add(weaponItemList[i].Uid, sprite);
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
