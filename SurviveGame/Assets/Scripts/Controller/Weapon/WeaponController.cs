using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private List<WeaponPoint> weaponPointList;

    private List<WeaponBase> equipWeaponList = new List<WeaponBase>();
    private int weaponMountCount = 0;

    private void Awake()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            WeaponItemInfo weaponInfo = ItemManager.getInstance.GetWeaponList()[1];
            EquipWeapon(weaponInfo);
        }
        if (Input.GetKeyDown("w"))
        {
            WeaponItemInfo weaponInfo = ItemManager.getInstance.GetWeaponList()[1];
            UnEquipWeapon(weaponInfo);
        }
        if (Input.GetKeyDown("e"))
        {
            WeaponItemInfo weaponInfo = ItemManager.getInstance.GetWeaponList()[0];
            EquipWeapon(weaponInfo);
        }


        UpdateWeaponAttack();
    }

    private void UpdateWeaponAttack()
    {
        for(int i =0; i < weaponMountCount; i++)
        {
            equipWeaponList[i].UpdateWeapon();
        }
    }

    public void Initialize()
    {
        //WeaponItemInfo weaponInfo = ItemManager.getInstance.GetSelectedWeapon();
        WeaponItemInfo weaponInfo = ItemManager.getInstance.GetWeaponList()[1];
        EquipWeapon(weaponInfo);
    }

    private void SetWeaponSprite(Sprite _sprite)
    {
        weaponPointList[weaponMountCount].SetWeaponSprite(_sprite);
        weaponPointList[weaponMountCount].gameObject.SetActive(true);
        weaponMountCount++;
    }

    public void EquipWeapon(WeaponItemInfo _info)
    {
        if(weaponMountCount > 5)
        {
            return;
        }

        if (_info.weaponType.Equals(WeaponType.StingWeapon))
        {
            StingWeapon stingWeapon = new StingWeapon(_info);
            stingWeapon.weaponSprite = Resources.Load<Sprite>(_info.weaponSpritePath);
            equipWeaponList.Add(stingWeapon);
            SetWeaponSprite(stingWeapon.weaponSprite);
        }
        else if(_info.weaponType.Equals(WeaponType.ShoootingWeapon))
        {
            ShoootingWeapon ShootingWeapon = new ShoootingWeapon(_info);
            ShootingWeapon.weaponSprite = Resources.Load<Sprite>(_info.weaponSpritePath);
            equipWeaponList.Add(ShootingWeapon);
            SetWeaponSprite(ShootingWeapon.weaponSprite);
        }
        else if (_info.weaponType.Equals(WeaponType.MowWeapon))
        {
            MowWeapon mowWeapon = new MowWeapon();
            mowWeapon.weaponItemInfo = _info;
            mowWeapon.weaponSprite = Resources.Load<Sprite>(_info.weaponSpritePath);
            equipWeaponList.Add(mowWeapon);
            SetWeaponSprite(mowWeapon.weaponSprite);
        }
    }

    private void UnEquipWeapon(WeaponItemInfo _info)
    {
        weaponMountCount--;
        weaponPointList[weaponMountCount].gameObject.SetActive(false);
    }
}
