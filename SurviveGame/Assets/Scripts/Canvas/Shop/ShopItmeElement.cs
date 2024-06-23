using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItmeElement : MonoBehaviour
{
    [SerializeField] private Button BuyButton;
    [SerializeField] private Text itemNameText;
    [SerializeField] private Text effectInformationText;
    [SerializeField] private Image itemImage;
    [SerializeField] private Text price;

    private WeaponItemInfo weaponInfo;

    //private ItemInfo itemInfo;

    public Button.ButtonClickedEvent GetBuyButtonEvent()
    {
        return BuyButton.onClick;
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

    public void ShowItemPriceText(int _priceAmount)
    {
        price.text = _priceAmount.ToString();
    }
}
