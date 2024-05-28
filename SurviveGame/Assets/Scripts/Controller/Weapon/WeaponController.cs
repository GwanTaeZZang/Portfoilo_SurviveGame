using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private List<WeaponPoint> weaponPointList;

    private ObjectPool<Bullet> bulletPool;
    private GameObject parent;

    private int weaponMountCount = 0;

    public void Initialize()
    {
        CreatePool();
        InitWeapon();

        WeaponItemInfo weaponInfo = ItemManager.getInstance.GetSelectedWeapon();
        weaponPointList[weaponMountCount].EquipWeapon(weaponInfo);
        weaponMountCount++;
    }

    private void CreatePool()
    {
        parent = new GameObject();
        parent.name = "BulletPoolParent";
        bulletPool = ObjectPoolManager.getInstance.GetPool<Bullet>();
        bulletPool.SetModel(Resources.Load<Transform>("Prefabs/Bullet_3"), parent.transform);
    }

    private void InitWeapon()
    {
        int count = weaponPointList.Count;
        for (int i = 0; i < count; i++)
        {
            weaponPointList[i].InitializeWeapon();
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


}
