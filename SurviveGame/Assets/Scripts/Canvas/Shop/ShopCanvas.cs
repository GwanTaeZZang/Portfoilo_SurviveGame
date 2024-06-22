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

    private ItemManager itemMgr;
    //private List<WeaponItemInfo> weaponList;
    private List<BaseItemInfo> itemList;
    private List<ShopEquipPassiveItemElement> passiveItemSlotList = new List<ShopEquipPassiveItemElement>();
    private WeaponItemInfo[] equipWeaponArr;
    private List<PassiveItemInfo> equipPassiveItemList = new List<PassiveItemInfo>();
    //private WeaponItemInfo[] itemElementinfoArr = new WeaponItemInfo[4];
    private BaseItemInfo[] baseItemElementinfoArr = new BaseItemInfo[4];
    private WeaponItemInfo curSelectedEquipWeapon;
    private int curSelectedEquipWeaponIdx = -1;

    private void Awake()
    {
        startWaveBtn.onClick.AddListener(OnClickStartWaveBtn);

        itemMgr = ItemManager.getInstance;

        itemList = itemMgr.GetItemInfoList();
        //weaponList = itemMgr.GetWeaponList();
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
    }


    private void UpdateItemElementInfo()
    {
        int count = itemElementList.Count;
        for (int i = 0; i < count; i++)
        {
            //int randomWeaponNum = Random.Range(0, weaponList.Count);
            //WeaponItemInfo weaponInfo = weaponList[randomWeaponNum];
            //itemElementinfoArr[i] = weaponInfo;

            //itemElementList[i].ShowItemIconImage(Resources.Load<Sprite>(weaponInfo.itemSpritePath));



            // 수정  
            int randomWeaponNum = Random.Range(0, itemList.Count);
            BaseItemInfo weaponInfo = itemList[randomWeaponNum];
            baseItemElementinfoArr[i] = weaponInfo;

            itemElementList[i].ShowItemIconImage(itemMgr.GetItemSprite(weaponInfo.Uid));


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
        for(int i =0; i < count; i++)
        {
            ShopEquipPassiveItemElement slot = passiveItemSlotList[i];
            PassiveItemInfo info = equipPassiveItemList[i];

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
            ShopEquipPassiveItemElement element = GameObject.Instantiate<ShopEquipPassiveItemElement>(equipItemElement, equipItemElementParent);
            element.gameObject.SetActive(false);

            passiveItemSlotList.Add(element);
        }
    }

    private void BindButtonEvent()
    {
        int count = itemElementList.Count;
        for(int i =0; i < count; i++)
        {
            int idx = i;
            itemElementList[i].GetButtonEvent().AddListener(() => OnClickItemElementBtn(idx));
        }

        count = shopEquipWeaponList.Count;
        for(int i =0; i < count; i++)
        {
            int idx = i;
            shopEquipWeaponList[i].GetButtonEvent().AddListener(() => OnClickShopEquipWeaponButton(idx));
        }

        optionPopup.GetCancleButtonEvent().AddListener(OnClickOptionCancleButton);
        optionPopup.GetSynthesisButtonEvent().AddListener(OnClickOptionSynthesisButton);
        optionPopup.GetSellButtonEvent().AddListener(OnClickOptionSellButton);

        rerollBtn.onClick.AddListener(OnClickReRollBtn);

        showStatusBtn.onClick.AddListener(OnClickShowStatusZoneButton);
        showItemBtn.onClick.AddListener(OnClickShowEuquipItemZoneButton);

    }


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

            WeaponItemInfo itemInfo = ItemManager.getInstance.GetWeaponInfoValue(upgradeItemKey);

            ItemManager.getInstance.EquipWeapon(itemInfo, curSelectedEquipWeaponIdx);

        }

        OnClickOptionCancleButton();
        UpdateEquipWeaponInfo();
    }

    private void OnClickOptionSellButton()
    {
        if(curSelectedEquipWeaponIdx != -1)
        {
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

    private void OnClickItemElementBtn(int _idx)
    {
        //WeaponItemInfo itemInfo = itemElementinfoArr[_idx];
        BaseItemInfo itemInfo = baseItemElementinfoArr[_idx];

        Debug.Log("item info  = " + itemInfo.itemName);

        int slotIdx = FindUnEquipWeaponSlotIdx();
        if(slotIdx == -1)
        {
            Debug.Log("have not slot");
            return;
        }
        // 무기아이템인지 패시브 아이템인지 나눠 담아야 함

        //ItemManager.getInstance.EquipWeapon(itemInfo, slotIdx);

        if(itemInfo.itemType == ItemType.WeaponType)
        {

            itemMgr.EquipWeapon((WeaponItemInfo)itemInfo, slotIdx);
        }
        else if(itemInfo.itemType == ItemType.PassiveType)
        {
            itemMgr.EquipPassiveItem((PassiveItemInfo)itemInfo);
        }


        UpdateEquipWeaponInfo();
        UpdateEquipPassiveItemInfo();
    }

    private void OnClickStartWaveBtn()
    {
        //UIManager.getInstance.Show<AugmenterCanvas>("Canvas/AugmenterCanvas");
        //int curWaveIdx = StageManager.getInstance.GetCurrentWave();

        StageManager.getInstance.StartWave();

        //if (curWaveIdx == 6 || curWaveIdx == 11)
        //{
        //    UIManager.getInstance.Show<AugmenterCanvas>("Canvas/AugmenterCanvas");
        //}
        //else
        //{
        //    StageManager.getInstance.StartWave();
        //}

        this.Hide();

    }

    private void OnClickReRollBtn()
    {
        UpdateItemElementInfo();
    }
}
