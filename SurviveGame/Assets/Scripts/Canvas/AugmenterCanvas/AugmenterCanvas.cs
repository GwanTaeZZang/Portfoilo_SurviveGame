using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AugmenterCanvas : UIBaseController
{
    [SerializeField] private List<AugmenterElement> augmenterElementList = new List<AugmenterElement>();
    [SerializeField] private Button rerollBtn;
    [SerializeField] private Text rerollValue;

    private List<AugmenterData> augmenterList;

    private void Awake()
    {
        augmenterList = AugmenterManager.getInstance.GetAugmenterList();
    }

    private void Start()
    {
        BindButtonEvent();
    }

    public override void Show()
    {
        base.Show();
        UpdateAugmenterElementInfo();
    }

    private void UpdateAugmenterElementInfo()
    {
        int count = augmenterElementList.Count;
        for(int i = 0; i < count; i++)
        {
            int randomIdx = Random.Range(0, augmenterList.Count);
            AugmenterData augmenterData = augmenterList[randomIdx];
            AugmenterElement element = augmenterElementList[i];

            Sprite icon = Resources.Load<Sprite>(augmenterData.augmenterSpritePath);
            element.SetAugmenterIcon(icon);
            element.SetAugmenterTitle(augmenterData.augmenterName);
            element.SetAugmenterInfo(augmenterData.augmenterContent);
        }
    }

    private void BindButtonEvent()
    {
        int count = augmenterElementList.Count;
        for(int i = 0; i < count; i++)
        {
            int idx = i;
            augmenterElementList[i].GetAugmenterBtnEvent().AddListener(() => OnClickAugmenterElementBtn(idx));
        }

        rerollBtn.onClick.AddListener(OnClickRerollBtn);
    }


    private void OnClickAugmenterElementBtn(int _idx)
    {

    }


    private void OnClickRerollBtn()
    {
        UpdateAugmenterElementInfo();
    }
}
