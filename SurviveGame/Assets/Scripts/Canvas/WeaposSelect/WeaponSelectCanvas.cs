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
    private int curSelectedWeaponIdx;

    private void Start()
    {
        weaponElementList = new List<SelectIconElement>();
        weaponSpriteList = new List<Sprite>();
        weaponInfoList = ItemManager.getInstance.GetWeaponList();
        backBtn.onClick.AddListener(OnClickBackBtn);
        selectCompleteBtn.onClick.AddListener(OnClickSelectCompleteBtn);
        SetWeaponElement();

        UpdateSelectWeaponInfo(0);
    }

    private void SetWeaponElement()
    {
        int count = weaponInfoList.Count;
        for (int i = 0; i < count; i++)
        {
            int idx = i;
            SelectIconElement element = Instantiate(weaponElementPrefab, weaponElementParent);
            weaponSpriteList.Add(Resources.Load<Sprite>(weaponInfoList[i].weaponSpritePath));

            element.SetElementThumbnail(weaponSpriteList[i]);
            element.GetElementSelectBtnEvent().AddListener(() => OnClickWeaponSelectBtn(idx));
            element.gameObject.SetActive(true);

            weaponElementList.Add(element);
        }
    }

    private void OnClickWeaponSelectBtn(int _idx)
    {
        Debug.Log(_idx + "Click");
        UpdateSelectWeaponInfo(_idx);
    }

    private void UpdateSelectWeaponInfo(int _idx)
    {
        curSelectedWeaponIdx = _idx;
        WeaponItemInfo weaponInfo = weaponInfoList[_idx];


        selectWeaponName.text = weaponInfo.weaponName;
        selectWeaponImage.sprite = weaponSpriteList[_idx];

        StringBuilder builder = new StringBuilder();
        builder.AppendLine("??????  " + weaponInfo.damage);
        builder.AppendLine("????  " + weaponInfo.damageRate);
        builder.AppendLine("????????  " + weaponInfo.attackSpeed);
        builder.AppendLine("????  " + weaponInfo.level);
        selectWeaponInfo.text = builder.ToString();

        builder = null;
    }

    private void OnClickBackBtn()
    {
        this.Hide();
    }

    private void OnClickSelectCompleteBtn()
    {
        ItemManager.getInstance.SetSelectedWeapon(weaponInfoList[curSelectedWeaponIdx]);
        //UIManager.getInstance.Hide();
        //UIManager.getInstance.Hide();
        SceneManager.LoadScene("InGameScene");
    }
}
