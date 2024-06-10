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


    private List<WeaponItemInfo> weaponList;
    private WeaponItemInfo[] equipWeaponArr;
    private WeaponItemInfo[] itemElementinfoArr = new WeaponItemInfo[4];
    private WeaponItemInfo curSelectedEquipWeapon;
    private int curSelectedEquipWeaponIdx = -1;

    private void Awake()
    {
        startWaveBtn.onClick.AddListener(OnClickStartWaveBtn);

        weaponList = ItemManager.getInstance.GetWeaponList();
        equipWeaponArr = ItemManager.getInstance.GetEquipmentWeaponArr();

        BindButtonEvent();
    }

    public override void Show()
    {
        base.Show();
        UpdateItemElementInfo();
        UpdateEquipWeaponInfo();
    }


    private void UpdateItemElementInfo()
    {
        int count = itemElementList.Count;
        for (int i = 0; i < count; i++)
        {
            int randomWeaponNum = Random.Range(0, weaponList.Count);
            WeaponItemInfo weaponInfo = weaponList[randomWeaponNum];
            itemElementinfoArr[i] = weaponInfo;

            itemElementList[i].ShowItemIconImage(Resources.Load<Sprite>(weaponInfo.weaponSpritePath));

        }
    }

    private void UpdateEquipWeaponInfo()
    {
        int count = shopEquipWeaponList.Count;
        for(int i = 0; i < count; i++)
        {
            if(equipWeaponArr[i] != null)
            {
                shopEquipWeaponList[i].ShowWeaponImage(Resources.Load<Sprite>(equipWeaponArr[i].weaponSpritePath));
                shopEquipWeaponList[i].isEquip = true;
            }
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


            //shopEquipWeaponList[sameItemIdx].ShowWeaponColor(Color.white);

            //curSelectedEquipWeapon.level++;
            //int level = curSelectedEquipWeapon.level;



            //if (level == 2)
            //{
            //    shopEquipWeaponList[curSelectedEquipWeaponIdx].ShowWeaponColor(Color.yellow);
            //}
            //if(level == 3)
            //{
            //    shopEquipWeaponList[curSelectedEquipWeaponIdx].ShowWeaponColor(Color.blue);
            //}
            //if (level == 4)
            //{
            //    shopEquipWeaponList[curSelectedEquipWeaponIdx].ShowWeaponColor(Color.red);
            //}
        }

        OnClickOptionCancleButton();
        UpdateEquipWeaponInfo();
    }

    private void OnClickOptionSellButton()
    {
        if(curSelectedEquipWeaponIdx != -1)
        {
            ItemManager.getInstance.UnEquipWeapon(curSelectedEquipWeaponIdx);

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

        Debug.Log(equipWeaponArr[_idx].weaponName);
    }

    private void OnClickItemElementBtn(int _idx)
    {
        WeaponItemInfo itemInfo = itemElementinfoArr[_idx];

        Debug.Log("item info  = " + itemInfo.weaponName);

        int slotIdx = FindUnEquipWeaponSlotIdx();
        if(slotIdx == -1)
        {
            Debug.Log("have not slot");
            return;
        }

        ItemManager.getInstance.EquipWeapon(itemInfo, slotIdx);


        UpdateEquipWeaponInfo();
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
}
