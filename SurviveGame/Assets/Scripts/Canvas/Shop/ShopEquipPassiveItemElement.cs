using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopEquipPassiveItemElement : MonoBehaviour
{
    [SerializeField] private Image iconImage;

    private string itemName;
    private string itemContent;

    public void SetIconIamge(Sprite _sprite)
    {
        iconImage.sprite = _sprite;
    }

    public void SetItemName(string _name)
    {
        itemName = _name;
    }

    public void SetItemContent(string _content)
    {
        itemContent = _content;
    }
}
