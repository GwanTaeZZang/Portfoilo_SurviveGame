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

    //private ItemInfo itemInfo;

    public Button.ButtonClickedEvent GetButtonEvent()
    {
        return itemButton.onClick;
    }

    //public void UpdateItemInformation(Item _item)
    //{
    //    itemInfo = _item.itemInfo;
    //    itemNameText.text = itemInfo.itemName;

    //    List<StatusEffect> buff = itemInfo.buff;
    //    int buffCount = itemInfo.buff.Count;
    //    effectInformationText.text = "Increse \n";
    //    for(int i = 0; i < buffCount; i++)
    //    {
    //        effectInformationText.text += buff[i].effectType;
    //        effectInformationText.text += "  :  " + buff[i].amount.ToString();
    //        effectInformationText.text += "\n";
    //    }

    //    List<StatusEffect> deBuff = itemInfo.deBuff;
    //    int deBuffCount = itemInfo.deBuff.Count;
    //    effectInformationText.text += "Decrese \n";
    //    for (int i = 0; i < deBuffCount; i++)
    //    {
    //        effectInformationText.text += deBuff[i].effectType;
    //        effectInformationText.text += "  :  " + deBuff[i].amount.ToString();

    //        effectInformationText.text += "\n";
    //    }
    //}

    //public ItemInfo GetItemInfo()
    //{
    //    return itemInfo;
    //}
}
