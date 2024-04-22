using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private List<WeaponPoint> weaponPointList;

    //private List<WeaponBase> equipWeaponList = new List<WeaponBase>();
    private int weaponMountCount = 0;

    private void Awake()
    {

    }

    private void Start()
    {
        int count = weaponPointList.Count;
        for(int i = 0; i < count; i++)
        {
            weaponPointList[i].UnEquipWeapon();
        }
    }



    private void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            WeaponItemInfo weaponInfo = ItemManager.getInstance.GetWeaponList()[1];
            weaponPointList[weaponMountCount].EquipWeapon(weaponInfo);
            weaponMountCount++;
        }
        if (Input.GetKeyDown("w"))
        {
            weaponMountCount--;
            weaponPointList[weaponMountCount].UnEquipWeapon();
        }
        if (Input.GetKeyDown("e"))
        {
            WeaponItemInfo weaponInfo = ItemManager.getInstance.GetWeaponList()[0];
            weaponPointList[weaponMountCount].EquipWeapon(weaponInfo);
            weaponMountCount++;
        }
    }

    public void Initialize()
    {
        WeaponItemInfo weaponInfo = ItemManager.getInstance.GetWeaponList()[0];
        weaponPointList[weaponMountCount].EquipWeapon(weaponInfo);
        weaponMountCount++;
    }

}
