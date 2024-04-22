using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPoint : MonoBehaviour
{
    [SerializeField] private SpriteRenderer weaponSprite;
    [SerializeField] private Animator anim;

    private WeaponBase weapon;
    private StingWeapon stingWeapon = new StingWeapon();
    private ShootingWeapon shootingWeapon = new ShootingWeapon();
    private MowWeapon mowWeapon = new MowWeapon();

    public void Awake()
    {
    }

    public void Update()
    {
        weapon.UpdateWeapon();
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
        weapon.weapon = this.transform;
        weaponSprite.sprite = ItemManager.getInstance.GetWeaponSprite(_info.Uid);
        weapon.anim = anim;

        this.gameObject.SetActive(true);
    }

    public void UnEquipWeapon()
    {
        weapon = null;

        this.gameObject.SetActive(false);
    }

    //public void SetWeaponSprite(Sprite _sprite)
    //{
    //    weaponSprite.sprite = _sprite;
    //}

    public void SetAnim(WeaponType _type)
    {
        anim.SetInteger("index",(int)_type);
    }

    public Animator GetAnimator()
    {
        return anim;
    }
}
