using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WeaponSelectCanvas : UIBaseController
{
    [SerializeField] private Button backBtn;
    [SerializeField] private Button selectCompleteBtn;
    [SerializeField] private Transform weaponElementParent;
    [SerializeField] private SelectIconElement weaponElementPrefab;
    [SerializeField] private Text selectWeaponName;
    [SerializeField] private Image selectWeaponImage;
    [SerializeField] private Text selectWeaponInfo;

    private List<SelectIconElement> weaponElementList;
    private List<WeaponItemInfo> weaponInfoList;
    private List<Sprite> weaponSpriteList;
    private WeaponItemInfo curSelectedWeaponInfo;

    private void Start()
    {
        weaponElementList = new List<SelectIconElement>();
        weaponSpriteList = new List<Sprite>();
        weaponInfoList = ItemManager.getInstance.GetWeaponList();
        backBtn.onClick.AddListener(OnClickBackBtn);
        selectCompleteBtn.onClick.AddListener(OnClickSelectCompleteBtn);
        SetWeaponElement();

        //UpdateSelectWeaponInfo(0);
    }

    private void SetWeaponElement()
    {
        int count = weaponInfoList.Count;
        int btnIdx = 0;

        for (int i = 0; i < count; i++)
        {
            if(weaponInfoList[i].level == 1)
            {
                int idx = btnIdx;
                WeaponItemInfo weaponInfo = weaponInfoList[i];
                SelectIconElement element = Instantiate(weaponElementPrefab, weaponElementParent);
                weaponSpriteList.Add(Resources.Load<Sprite>(weaponInfoList[i].weaponSpritePath));

                element.SetElementThumbnail(weaponSpriteList[btnIdx]);
                element.GetElementSelectBtnEvent().AddListener(() => UpdateSelectWeaponInfo(weaponInfo, idx));
                element.gameObject.SetActive(true);

                weaponElementList.Add(element);

                btnIdx++;
            }
        }
    }

    //private void OnClickWeaponSelectBtn(int _idx)
    //{
    //    Debug.Log(_idx + "Click");
    //    UpdateSelectWeaponInfo(_idx);
    //}

    private void UpdateSelectWeaponInfo(WeaponItemInfo _weaponInfo, int _idx)
    {
        //curSelectedWeaponIdx = _idx;
        //WeaponItemInfo weaponInfo = weaponInfoList[_idx];


        selectWeaponName.text = _weaponInfo.weaponName;
        selectWeaponImage.sprite = weaponSpriteList[_idx];

        StringBuilder builder = new StringBuilder();
        builder.AppendLine("Damage  " + _weaponInfo.damage);
        builder.AppendLine("Damage Rate  " + _weaponInfo.damageRate);
        builder.AppendLine("Attack Speed  " + _weaponInfo.attackSpeed);
        builder.AppendLine("Level  " + _weaponInfo.level);
        selectWeaponInfo.text = builder.ToString();

        builder = null;

        curSelectedWeaponInfo = _weaponInfo;
    }

    private void OnClickBackBtn()
    {
        this.Hide();
    }

    private void OnClickSelectCompleteBtn()
    {
        ItemManager.getInstance.SetSelectedWeapon(curSelectedWeaponInfo);
        //UIManager.getInstance.Hide();
        //UIManager.getInstance.Hide();
        UIManager.getInstance.Show<StageSelectCanvas>("Canvas/StageSelectCanvas");
    }
}
