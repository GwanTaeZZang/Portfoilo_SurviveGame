using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPoint : MonoBehaviour
{
    [SerializeField] private SpriteRenderer weaponSprite;
    [SerializeField] private Animator anim;

    public void SetWeaponSprite(Sprite _sprite)
    {
        weaponSprite.sprite = _sprite;
    }

    public void SetAnim(WeaponType _type)
    {
        anim.SetInteger("index",(int)_type);
    }

    public Animator GetAnimator()
    {
        return anim;
    }
}
