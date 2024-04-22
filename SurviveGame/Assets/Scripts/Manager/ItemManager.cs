using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    private Dictionary<int, WeaponItemInfo> weaponItemDict = new Dictionary<int, WeaponItemInfo>();
    private Dictionary<int, Sprite> weaponItemSpriteDict = new Dictionary<int, Sprite>();
    private List<WeaponItemInfo> weaponItemList = new List<WeaponItemInfo>();
    private List<Sprite> weaponItemSpriteList = new List<Sprite>();
    private WeaponItemInfo selectedWeapon;

    public override bool Initialize()
    {
        LoadWeaponData();
        SaveWeaponSprite();
        return true;
    }

    public WeaponItemInfo GetValue(int _key)
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

    public void SetSelectedWeapon(WeaponItemInfo _selectedWeapon)
    {
        selectedWeapon = _selectedWeapon;
        Debug.Log("?????? ????  = " + selectedWeapon.weaponName);
    }

    public WeaponItemInfo GetSelectedWeapon()
    {
        return selectedWeapon;
    }

    public Sprite GetWeaponSprite(int _Uid)
    {
        if (!weaponItemDict.ContainsKey(_Uid))
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

        for(int i=0; i < count; i++)
        {
            //weaponItemSpriteList.Add(Resources.Load<Sprite>(weaponItemList[i].weaponSpritePath));
            Sprite sprite = Resources.Load<Sprite>(weaponItemList[i].weaponSpritePath);
            weaponItemSpriteDict.Add(weaponItemList[i].Uid, sprite);
        }
    }

}
