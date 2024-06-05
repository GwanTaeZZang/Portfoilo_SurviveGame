using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopEquipWeaponElement : MonoBehaviour
{
    [SerializeField] private Button weapomButton;
    [SerializeField] private Image weaponImage;

    public bool isEquip = false;



    public Button.ButtonClickedEvent GetButtonEvent()
    {
        return weapomButton.onClick;
    }

    public void ShowWeaponImage(Sprite _weaponSprite)
    {
        weaponImage.sprite = _weaponSprite;
    }

}
