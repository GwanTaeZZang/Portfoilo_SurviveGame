using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPoint : MonoBehaviour
{
    [SerializeField] private SpriteRenderer weaponSprite;
    [SerializeField] private Transform weaponTransform;
    [SerializeField] private OBBCollision obbCollision;

    private Vector2 originLocalPos;
    private WeaponBase weapon;

    private float damage;
    private bool isEquip = false;

    public void Awake()
    {
        originLocalPos = new Vector2(0.4f, 0);
        obbCollision.OnOBBCollisionEvent = OnOBBCollision;
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
        weapon.SetParent(this.transform);

        weaponTransform.localPosition = new Vector2(0.4f, 0);
        weapon.SetWeapon(weaponTransform);

        damage = _info.damage;

        weaponSprite.sprite = ItemManager.getInstance.GetWeaponSprite(_info.Uid);
        //weapon.anim = anim;
        isEquip = true;

        obbCollision.SetObejctSize();
        if (_info.weaponType == WeaponType.ShoootingWeapon)
        {
            obbCollision.enabled = false;
            //obbCollision.SetDamage(0);

        }
        else
        {
            weapon.OnAttackEvent = OnAttack;
            //obbCollision.SetDamage(damage);
            //obbCollision.enabled = true;

        }

    }

    private void OnOBBCollision(ITargetAble _target)
    {
        _target.OnDamege(damage);
    }


    private void OnAttack(bool _isAttack)
    {
        obbCollision.enabled = _isAttack;
        obbCollision.SetInfo();
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

        obbCollision.SetTarget(MonsterManager.getInstance.GetTargetArr());
    }
}
