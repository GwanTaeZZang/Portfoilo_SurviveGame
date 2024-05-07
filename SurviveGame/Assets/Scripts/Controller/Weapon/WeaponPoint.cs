using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPoint : MonoBehaviour
{
    [SerializeField] private SpriteRenderer weaponSprite;
    [SerializeField] private Transform weaponTransform;

    private WeaponBase weapon;
    private StingWeapon stingWeapon;
    private ShootingWeapon shootingWeapon;
    private MowWeapon mowWeapon;

    private bool isEquip = false;

    public void Awake()
    {
        stingWeapon = new StingWeapon();
        shootingWeapon = new ShootingWeapon();
        mowWeapon = new MowWeapon();
    }

    public void Update()
    {
        if (isEquip)
        {
            weapon.UpdateWeapon();
        }
    }

    public void EquipWeapon(WeaponItemInfo _info)
    {
        weapon = ItemManager.getInstance.GetWeaponInstance(_info.weaponType);
        weapon.SetWeaponInfo(_info);

        weaponTransform.localPosition = Vector2.zero;
        weapon.SetWeapon(weaponTransform);

        weaponSprite.sprite = ItemManager.getInstance.GetWeaponSprite(_info.Uid);
        //weapon.anim = anim;
        isEquip = true;

    }

    public void UnEquipWeapon()
    {
        ItemManager.getInstance.ReleaseWeaponInstance
            (weapon.weaponItemInfo.weaponType, weapon);

        weapon = null;
        weaponSprite.sprite = null;
        isEquip = false;
    }

    public void InitializeWeapon()
    {
        weapon = null;
        weaponSprite.sprite = null;
        isEquip = false;
    }
}
