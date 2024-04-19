using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPoint : MonoBehaviour
{
    [SerializeField] private SpriteRenderer weaponSprite;

    public void SetWeaponSprite(Sprite _sprite)
    {
        weaponSprite.sprite = _sprite;
    }
}
