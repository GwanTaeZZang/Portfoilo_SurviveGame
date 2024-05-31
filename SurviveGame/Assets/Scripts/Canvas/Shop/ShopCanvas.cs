using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCanvas : UIBaseController
{
    [SerializeField] private List<ShopItmeElement> itemElementList;
    [SerializeField] private List<Text> statusAmountList;
    [SerializeField] private List<Image> equipWeaponImageList;
    [SerializeField] private Button startWaveBtn;

    private void Awake()
    {

    }
    private void Start()
    {
        UpdateItemElementInfo();

        startWaveBtn.onClick.AddListener(OnClickStartWaveBtn);
    }

    private void UpdateItemElementInfo()
    {
        int count = itemElementList.Count;
        for (int i = 0; i < count; i++)
        {

        }
    }

    private void OnClickStartWaveBtn()
    {
        StageManager.getInstance.StartWave();
        this.Hide();
    }
}
