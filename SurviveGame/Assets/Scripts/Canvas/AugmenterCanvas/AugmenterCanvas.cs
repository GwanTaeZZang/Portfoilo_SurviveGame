using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AugmenterCanvas : UIBaseController
{
    [SerializeField] private List<AugmenterElement> augmenterElementList = new List<AugmenterElement>();
    [SerializeField] private Button rerollBtn;
    [SerializeField] private Text rerollValue;

    private void Awake()
    {
        
    }

    private void Start()
    {
        BindButtonEvent();
    }

    private void UpdateAugmenterElementInfo()
    {
        
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

    }
}
