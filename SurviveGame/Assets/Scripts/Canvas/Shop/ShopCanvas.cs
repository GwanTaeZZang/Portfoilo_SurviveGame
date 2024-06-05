using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCanvas : UIBaseController
{
    [SerializeField] private List<ShopItmeElement> itemElementList;
    [SerializeField] private List<Text> statusAmountList;
    [SerializeField] private List<ShopEquipWeaponElement> equipWeaponList;
    [SerializeField] private Button startWaveBtn;

    private List<WeaponItemInfo> weaponList;
    private WeaponItemInfo[] equipWeaponArr;


    private WeaponItemInfo[] itemElementinfoArr = new WeaponItemInfo[4];

    private void Awake()
    {
        startWaveBtn.onClick.AddListener(OnClickStartWaveBtn);

        weaponList = ItemManager.getInstance.GetWeaponList();
        equipWeaponArr = ItemManager.getInstance.GetEquipmentWeaponArr();

        BindItemElementButtonEvent();
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
        int count = equipWeaponList.Count;
        for(int i = 0; i < count; i++)
        {
            if(equipWeaponArr[i] != null)
            {
                equipWeaponList[i].ShowWeaponImage(Resources.Load<Sprite>(equipWeaponArr[i].weaponSpritePath));
                equipWeaponList[i].isEquip = true;
            }
        }
    }

    private int FindUnEquipWeaponSlotIdx()
    {
        int count = equipWeaponList.Count;
        for (int i = 0; i < count; i++)
        {
            if(!equipWeaponList[i].isEquip)
            {
                return i;
            }
        }
        return -1;
    }

    private void BindItemElementButtonEvent()
    {
        int count = itemElementList.Count;
        for(int i =0; i < count; i++)
        {
            int idx = i;
            itemElementList[i].GetButtonEvent().AddListener(() => OnClickItemElementBtn(idx));
        }
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
}
