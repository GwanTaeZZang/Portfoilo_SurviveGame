using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private List<WeaponPoint> weaponPointList;

    private List<BaseItem> weaponList = new List<BaseItem>();
    private int weaponMountCount = 0;

    private void Awake()
    {

    }

    public void Initialize()
    {
        WeaponItemInfo weaponInfo = ItemManager.getInstance.GetSelectedWeapon();
        if (weaponInfo.weaponType.Equals(WeaponType.ShotRange))
        {
            ShotRangeWeapon shotRangeWeapon = new ShotRangeWeapon();
            shotRangeWeapon.weaponItemInfo = weaponInfo;
            weaponList.Add(shotRangeWeapon);
        }
        else
        {
            LongRangeWeapon longRangeWeapon = new LongRangeWeapon();
            longRangeWeapon.weaponItemInfo = weaponInfo;
            weaponList.Add(longRangeWeapon);
        }

        SetMountingWeapon(Resources.Load<Sprite>(weaponInfo.weaponSpritePath));
    }

    public void SetMountingWeapon(Sprite _sprite)
    {
        weaponPointList[weaponMountCount].SetWeaponSprite(_sprite);
        weaponPointList[weaponMountCount].gameObject.SetActive(true);
        weaponMountCount++;
    }
}
