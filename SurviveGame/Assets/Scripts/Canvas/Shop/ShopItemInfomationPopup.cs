using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemInfomationPopup : MonoBehaviour
{
    [SerializeField] private Image iconIamge;
    [SerializeField] private Text itemName;
    [SerializeField] private Text itemContent;
    [SerializeField] private Button backButton;

    public void SetIconImage(Sprite _sprite)
    {
        iconIamge.sprite = _sprite;
    }
    public void SetItemName(string _name)
    {
        itemName.text = _name;
    }
    public void SetItemContent(string _content)
    {
        itemContent.text = _content;
    }

    public Button.ButtonClickedEvent GetBackButtonEvent()
    {
        return backButton.onClick;
    }
}
