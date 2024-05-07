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

    public void SetSelectedWeapon(WeaponItemInfo _selectedWeapon)
    {
        selectedWeapon = _selectedWeapon;
    }

    public WeaponItemInfo GetSelectedWeapon()
    {
        return selectedWeapon;
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
