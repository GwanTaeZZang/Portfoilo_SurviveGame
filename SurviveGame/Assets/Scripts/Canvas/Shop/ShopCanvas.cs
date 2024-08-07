using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCanvas : UIBaseController
{
    [SerializeField] private List<ShopItmeElement> itemElementList;
    [SerializeField] private List<Text> statusAmountList;
    [SerializeField] private List<ShopEquipWeaponElement> shopEquipWeaponList;
    [SerializeField] private Button startWaveBtn;
    [SerializeField] private EquipWeaponOptionpopup optionPopup;
    [SerializeField] private Button rerollBtn;
    [SerializeField] private Button showStatusBtn;
    [SerializeField] private Button showItemBtn;
    // PassiveItem
    [SerializeField] private ShopEquipPassiveItemElement equipItemElement;
    [SerializeField] private Transform equipItemElementParent;
    [SerializeField] private GameObject statusZone;
    [SerializeField] private GameObject equipItemZone;
    // item Infomation Popup
    [SerializeField] private ShopItemInfomationPopup itemInfomationPopup;
    // gold Text
    [SerializeField] private Text goldText;
    // status View
    [SerializeField] private ShopPlayerStatusView statusView;



    private ItemManager itemMgr;
    private List<BaseItemInfo> itemList;
    private List<ShopEquipPassiveItemElement> passiveItemSlotList = new List<ShopEquipPassiveItemElement>();
    private WeaponItemInfo[] equipWeaponArr;
    private List<PassiveItemInfo> equipPassiveItemList = new List<PassiveItemInfo>();
    private BaseItemInfo[] baseItemElementinfoArr = new BaseItemInfo[4];
    private WeaponItemInfo curSelectedEquipWeapon;
    private int curSelectedEquipWeaponIdx = -1;

    private int goldAmount;

    private void Awake()
    {
        startWaveBtn.onClick.AddListener(OnClickStartWaveBtn);

        itemMgr = ItemManager.getInstance;

        itemList = itemMgr.GetItemInfoList();
        equipWeaponArr = itemMgr.GetEquipmentWeaponArr();
        equipPassiveItemList = itemMgr.GetEquipPassiveItemList();

        BindButtonEvent();


        CreatePassiveItemSlot();
    }

    public override void Show()
    {
        base.Show();
        UpdateItemElementInfo();
        UpdateEquipWeaponInfo();

        OnClickShowStatusZoneButton();
        UpdateEquipPassiveItemInfo();

        goldAmount = GlobalData.getInstance.GetGoldAmount();
        goldText.text = goldAmount.ToString();

        statusView.UpdateStatusAmount(PlayerManager.getInstance.GetCharacter().statusArr);
    }



    private void UpdateItemElementInfo()
    {
        int count = itemElementList.Count;
        for (int i = 0; i < count; i++)
        {
            int randomWeaponNum = Random.Range(0, itemList.Count);
            BaseItemInfo itemInfo = itemList[randomWeaponNum];
            baseItemElementinfoArr[i] = itemInfo;

            ShopItmeElement element = itemElementList[i];

            element.ShowItemNameText(itemInfo.itemName);
            element.ShowItemIconImage(itemMgr.GetItemSprite(itemInfo.Uid));
            element.ShowEffectInformationText(itemInfo.itemContent);
            element.ShowItemPriceText(itemInfo.price);
            element.gameObject.SetActive(true);
        }
    }

    private void UpdateEquipWeaponInfo()
    {
        int count = shopEquipWeaponList.Count;
        for(int i = 0; i < count; i++)
        {
            if(equipWeaponArr[i] != null)
            {
                shopEquipWeaponList[i].ShowWeaponImage(itemMgr.GetItemSprite(equipWeaponArr[i].Uid));
                shopEquipWeaponList[i].isEquip = true;
            }
        }
    }

    private void UpdateEquipPassiveItemInfo()
    {

        int count = equipPassiveItemList.Count;

        if (count != 0)
        {
            ShopEquipPassiveItemElement slot = passiveItemSlotList[count - 1];
            PassiveItemInfo info = equipPassiveItemList[count - 1];
            slot.gameObject.SetActive(true);
            slot.SetIconIamge(itemMgr.GetItemSprite(info.Uid));
            slot.SetItemName(info.itemName);
            slot.SetItemContent(info.itemContent);
        }

    }

    private int FindUnEquipWeaponSlotIdx()
    {
        int count = shopEquipWeaponList.Count;
        for (int i = 0; i < count; i++)
        {
            if(!shopEquipWeaponList[i].isEquip)
            {
                return i;
            }
        }
        return -1;
    }

    private int FindSameWeaponIdx(WeaponItemInfo _weaponInfo)
    {
        int count = shopEquipWeaponList.Count;
        for (int i = 0; i < count; i++)
        {
            if(curSelectedEquipWeaponIdx != i)
            {
                if (shopEquipWeaponList[i].isEquip)
                {
                    if (_weaponInfo.Uid == equipWeaponArr[i].Uid &&
                        _weaponInfo.level == equipWeaponArr[i].level)
                    {
                        return i;
                    }

                }
            }
        }
        return -1;
    }

    private void CreatePassiveItemSlot()
    {
        int count = 100;
        for(int i =0; i< count; i++)
        {
            int idx = i;

            ShopEquipPassiveItemElement element = GameObject.Instantiate<ShopEquipPassiveItemElement>(equipItemElement, equipItemElementParent);
            element.gameObject.SetActive(false);

            element.GetPassiveButtonEvent().AddListener(() => OnClickEquipPassiveItemElementButton(idx));

            passiveItemSlotList.Add(element);
        }
    }



    /// <summary>
    /// Button Binding Method
    /// </summary>
    private void BindButtonEvent()
    {
        int count = itemElementList.Count;
        for(int i =0; i < count; i++)
        {
            int idx = i;
            itemElementList[i].GetBuyButtonEvent().AddListener(() => OnClickItemBuyBtn(idx));
        }

        count = shopEquipWeaponList.Count;
        for(int i =0; i < count; i++)
        {
            int idx = i;
            shopEquipWeaponList[i].GetButtonEvent().AddListener(() => OnClickShopEquipWeaponButton(idx));
        }

        optionPopup.GetCancleButtonEvent().AddListener(OnClickOptionCancleButton);
        optionPopup.GetSynthesisButtonEvent().AddListener(OnClickOptionSynthesisButton);
        optionPopup.GetShowInfoButtonEvent().AddListener(OnClickShowWeaponItemInfo);
        optionPopup.GetSellButtonEvent().AddListener(OnClickOptionSellButton);

        rerollBtn.onClick.AddListener(OnClickReRollBtn);

        showStatusBtn.onClick.AddListener(OnClickShowStatusZoneButton);
        showItemBtn.onClick.AddListener(OnClickShowEuquipItemZoneButton);

        itemInfomationPopup.GetBackButtonEvent().AddListener(OnClickItemInfoPopupBackButton);

    }


    /// <summary>
    /// Event
    /// </summary>

    private void OnClickShowStatusZoneButton()
    {
        statusZone.SetActive(true);
        equipItemZone.SetActive(false);

    }

    private void OnClickShowEuquipItemZoneButton()
    {
        statusZone.SetActive(false);
        equipItemZone.SetActive(true);

    }

    private void OnClickOptionCancleButton()
    {
        optionPopup.gameObject.SetActive(false);
        curSelectedEquipWeapon = null;
        curSelectedEquipWeaponIdx = -1;
    }

    private void OnClickOptionSynthesisButton()
    {
        int sameItemIdx = FindSameWeaponIdx(curSelectedEquipWeapon);

        if(sameItemIdx != -1 && curSelectedEquipWeapon.level != 4)
        {
            ItemManager.getInstance.UnEquipWeapon(sameItemIdx);

            shopEquipWeaponList[sameItemIdx].isEquip = false;
            shopEquipWeaponList[sameItemIdx].ShowWeaponImage(null);

            int upgradeItemKey = curSelectedEquipWeapon.Uid + 1;

            WeaponItemInfo itemInfo = itemMgr.GetWeaponInfoValue(upgradeItemKey);

            itemMgr.EquipWeapon(itemInfo, curSelectedEquipWeaponIdx);

        }

        OnClickOptionCancleButton();
        UpdateEquipWeaponInfo();
    }

    private void OnClickShowWeaponItemInfo()
    {
        WeaponItemInfo info = equipWeaponArr[curSelectedEquipWeaponIdx];

        itemInfomationPopup.gameObject.SetActive(true);

        itemInfomationPopup.SetIconImage(itemMgr.GetItemSprite(info.Uid));
        itemInfomationPopup.SetItemName(info.itemName);
        itemInfomationPopup.SetItemContent(info.itemContent);
    }

    private void OnClickOptionSellButton()
    {
        if(curSelectedEquipWeaponIdx != -1)
        {
            WeaponItemInfo info = equipWeaponArr[curSelectedEquipWeaponIdx];
            GlobalData.getInstance.InCreaseGold(info.price);
            goldAmount += info.price;
            goldText.text = goldAmount.ToString();


            itemMgr.UnEquipWeapon(curSelectedEquipWeaponIdx);

            shopEquipWeaponList[curSelectedEquipWeaponIdx].isEquip = false;
            shopEquipWeaponList[curSelectedEquipWeaponIdx].ShowWeaponImage(null);

            
        }
        OnClickOptionCancleButton();

    }

    private void OnClickShopEquipWeaponButton(int _idx)
    {
        if (!shopEquipWeaponList[_idx].isEquip)
        {
            return;
        }

        optionPopup.transform.position = shopEquipWeaponList[_idx].transform.position;
        optionPopup.gameObject.SetActive(true);

        curSelectedEquipWeapon = equipWeaponArr[_idx];
        curSelectedEquipWeaponIdx = _idx;

        Debug.Log(equipWeaponArr[_idx].itemName);
    }

    private void OnClickItemBuyBtn(int _idx)
    {
        BaseItemInfo itemInfo = baseItemElementinfoArr[_idx];

        if(goldAmount - itemInfo.price < 0)
        {
            Debug.Log("No Money");
            return;
        }

        GlobalData.getInstance.DeCreaseGold(itemInfo.price);
        goldAmount -= itemInfo.price;
        goldText.text = goldAmount.ToString();


        Debug.Log("item info  = " + itemInfo.itemName);

        int slotIdx = FindUnEquipWeaponSlotIdx();
        if(slotIdx == -1)
        {
            Debug.Log("have not slot");
            return;
        }

        if(itemInfo.itemType == ItemType.WeaponType)
        {

            itemMgr.EquipWeapon((WeaponItemInfo)itemInfo, slotIdx);
        }
        else if(itemInfo.itemType == ItemType.PassiveType)
        {
            itemMgr.EquipPassiveItem((PassiveItemInfo)itemInfo);
        }

        itemElementList[_idx].gameObject.SetActive(false);
        UpdateEquipWeaponInfo();
        UpdateEquipPassiveItemInfo();
        statusView.UpdateStatusAmount(PlayerManager.getInstance.GetCharacter().statusArr);

    }

    private void OnClickStartWaveBtn()
    {
        StageManager.getInstance.StartWave();

        this.Hide();
    }

    private void OnClickReRollBtn()
    {
        UpdateItemElementInfo();
    }

    private void OnClickEquipPassiveItemElementButton(int _idx)
    {
        PassiveItemInfo info = equipPassiveItemList[_idx];


        itemInfomationPopup.gameObject.SetActive(true);

        itemInfomationPopup.SetIconImage(itemMgr.GetItemSprite(info.Uid));
        itemInfomationPopup.SetItemName(info.itemName);
        itemInfomationPopup.SetItemContent(info.itemContent);

    }

    private void OnClickItemInfoPopupBackButton()
    {
        itemInfomationPopup.gameObject.SetActive(false);
    }
}
