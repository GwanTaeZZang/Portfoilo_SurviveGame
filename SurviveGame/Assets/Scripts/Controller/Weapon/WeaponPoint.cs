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

    private bool isEquip = false;

    public void Awake()
    {
        originLocalPos = new Vector2(0.4f, 0);
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

        weaponTransform.localPosition = originLocalPos;
        weapon.SetWeapon(weaponTransform);

        weaponSprite.sprite = ItemManager.getInstance.GetWeaponSprite(_info.Uid);
        //weapon.anim = anim;
        isEquip = true;

        obbCollision.SetObejctSize();
        if (_info.weaponType == WeaponType.ShoootingWeapon)
        {
            obbCollision.enabled = false;
        }
        else
        {
            weapon.OnAttackEvent = OnAttack;
            //obbCollision.enabled = true;

        }

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
