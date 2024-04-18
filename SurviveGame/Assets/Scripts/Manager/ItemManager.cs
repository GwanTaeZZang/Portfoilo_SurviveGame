using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    private Dictionary<int, WeaponItemInfo> weaponItemDict = new Dictionary<int, WeaponItemInfo>();
    private List<WeaponItemInfo> weaponItmeList = new List<WeaponItemInfo>();
    private WeaponItemInfo selectedWeapon;

    public override bool Initialize()
    {
        LoadWeaponData();
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
        return weaponItmeList;
    }

    public void SetSelectedWeapon(WeaponItemInfo _selectedWeapon)
    {
        selectedWeapon = _selectedWeapon;
        Debug.Log("선택된 직업  = " + selectedWeapon.weaponName);
    }

    private void LoadWeaponData()
    {
        WeaponData weaponData = JsonController.ReadJson<WeaponData>("WeaponData");
        int count = weaponData.weaponArr.Length;
        for (int i = 0; i < count; i++)
        {
            WeaponItemInfo weapon = weaponData.weaponArr[i];
            weaponItemDict.Add(weapon.Uid, weapon);
            weaponItmeList.Add(weapon);
        }
    }

}
