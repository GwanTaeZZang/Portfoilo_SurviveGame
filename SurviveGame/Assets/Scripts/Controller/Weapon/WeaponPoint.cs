using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPoint : MonoBehaviour
{
    [SerializeField] private SpriteRenderer weaponSprite;
    [SerializeField] private Transform weaponTransform;
    //[SerializeField] private Animator anim;

    private WeaponBase weapon;
    private StingWeapon stingWeapon;
    private ShootingWeapon shootingWeapon;
    private MowWeapon mowWeapon;

    private bool isEquip = false;

    public void Awake()
    {
        stingWeapon = new StingWeapon(weaponTransform);
        shootingWeapon = new ShootingWeapon(weaponTransform);
        mowWeapon = new MowWeapon(weaponTransform);
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

        if (_info.weaponType.Equals(WeaponType.StingWeapon))
        {
            stingWeapon.SetWeaponInfo(_info);
            weapon = stingWeapon;
        }
        if (_info.weaponType.Equals(WeaponType.ShoootingWeapon))
        {
            shootingWeapon.SetWeaponInfo(_info);
            weapon = shootingWeapon;
        }
        if (_info.weaponType.Equals(WeaponType.MowWeapon))
        {
            mowWeapon.SetWeaponInfo(_info);
            weapon = mowWeapon;
        }
        weaponSprite.sprite = ItemManager.getInstance.GetWeaponSprite(_info.Uid);
        //weapon.anim = anim;
        isEquip = true;
    }

    public void UnEquipWeapon()
    {
        weapon = null;
        weaponSprite.sprite = null;
        isEquip = false;
    }

    //public void SetWeaponSprite(Sprite _sprite)
    //{
    //    weaponSprite.sprite = _sprite;
    //}

    public void SetAnim(WeaponType _type)
    {
        //anim.SetInteger("index",(int)_type);
    }

}
