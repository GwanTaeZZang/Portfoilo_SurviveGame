using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItmeElement : MonoBehaviour
{
    [SerializeField] private Button itemButton;
    [SerializeField] private Text itemNameText;
    [SerializeField] private Text effectInformationText;
    [SerializeField] private Image itemImage;

    private WeaponItemInfo weaponInfo;

    //private ItemInfo itemInfo;

    public Button.ButtonClickedEvent GetButtonEvent()
    {
        return itemButton.onClick;
    }

    public void ShowItemNameText(string _ItemName)
    {
        itemNameText.text = _ItemName;
    }

    public void ShowEffectInformationText(string _information)
    {
        effectInformationText.text = _information;
    }

    public void ShowItemIconImage(Sprite _weaponIcon)
    {
        itemImage.sprite = _weaponIcon;
    }
}
