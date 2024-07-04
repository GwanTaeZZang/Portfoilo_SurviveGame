using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class AugmenterCanvas : UIBaseController
{
    [SerializeField] private List<AugmenterElement> augmenterElementList = new List<AugmenterElement>();
    [SerializeField] private Button rerollBtn;
    [SerializeField] private Text rerollValue;

    [SerializeField] private GameObject selectAugmenterBG;
    [SerializeField] private AugmenterElement selectAugmenterElement;
    [SerializeField] private Button selectButton;
    [SerializeField] private Button cancleButton;


    private List<AugmenterData> augmenterList;
    private List<int> selectedAugmenterUidList;

    private List<AugmenterData> showAugmenterList = new List<AugmenterData>();
    private List<int> showAugmenterUid = new List<int>();
    private List<int> showAugmenterGroupId = new List<int>();

    private AugmenterData selectAugmenterData;

    private AugmenterManager augmenterMgr;

    private void Awake()
    {
        augmenterMgr = AugmenterManager.getInstance;
        augmenterList = augmenterMgr.GetAugmenterList();
        selectedAugmenterUidList = augmenterMgr.GetSelectedAugmenterUidList();
    }

    private void Start()
    {
        BindButtonEvent();
    }

    public override void Show()
    {
        base.Show();
        showAugmenterList.Clear();
        UpdateAugmenterElementInfo();
    }

    private void UpdateAugmenterElementInfo()
    {
        augmenterList = augmenterList.OrderBy(a => Random.value).ToList();

        foreach(var augmenter in augmenterList)
        {
            // Same UID Check
            if (showAugmenterUid.Contains(augmenter.Uid))
            {
                continue;
            }
            // Same gorupId Check
            if(showAugmenterList.Count(a => a.groupId == augmenter.groupId) > 0)
            {
                continue;
            }
            if(!augmenter.isDuplicat && CheckNotDuplicatGroupId(augmenter.groupId))
            {
                continue;
            }
            // Is Selected Augmenter Check
            if (selectedAugmenterUidList.Contains(augmenter.Uid))
            {
                continue;
            }

            showAugmenterList.Add(augmenter);
            showAugmenterUid.Add(augmenter.Uid);
            showAugmenterGroupId.Add(augmenter.groupId);

            if(showAugmenterList.Count == 3)
            {
                break;
            }
        }
        showAugmenterUid.Clear();
        showAugmenterGroupId.Clear();


        int count = augmenterElementList.Count;
        for (int i = 0; i < count; i++)
        {
            AugmenterData augmenterData = showAugmenterList[i];
            AugmenterElement element = augmenterElementList[i];

            Sprite icon = Resources.Load<Sprite>(augmenterData.augmenterSpritePath);
            element.SetAugmenterIcon(icon);
            element.SetAugmenterTitle(augmenterData.augmenterName);
            element.SetAugmenterInfo(augmenterData.augmenterContent);
        }
    }

    private bool CheckNotDuplicatGroupId(int _gorupId)
    {
        foreach(int uid in selectedAugmenterUidList)
        {
            AugmenterData augmenter = augmenterMgr.GetAugmenterData(uid);

            if(augmenter.groupId == _gorupId)
            {
                return true;
            }
        }
        return false;
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

        selectButton.onClick.AddListener(OnClickSelectBtn);
        cancleButton.onClick.AddListener(OnClickCancleBtn);
    }


    private void OnClickAugmenterElementBtn(int _idx)
    {

        selectAugmenterBG.SetActive(true);

        AugmenterData selectedAugmenter = showAugmenterList[_idx];
        selectAugmenterData = selectedAugmenter;
        selectAugmenterElement.SetAugmenterIcon(Resources.Load<Sprite>(selectedAugmenter.augmenterSpritePath));
        selectAugmenterElement.SetAugmenterTitle(selectedAugmenter.augmenterName);
        selectAugmenterElement.SetAugmenterInfo(selectedAugmenter.augmenterContent);

    }


    private void OnClickRerollBtn()
    {
        showAugmenterList.Clear();

        UpdateAugmenterElementInfo();
    }

    private void OnClickSelectBtn()
    {
        selectAugmenterBG.SetActive(false);

        augmenterMgr.SetSeletedAugmenterList(selectAugmenterData);
        augmenterMgr.SetSelectedAugmenterUid(selectAugmenterData.Uid);

        Debug.Log("Selected Augmenter Uid is  =  " + selectAugmenterData.Uid);

        UIManager.getInstance.Show<ShopCanvas>("Canvas/ShopCanvas");
        this.Hide();
    }

    private void OnClickCancleBtn()
    {
        selectAugmenterBG.SetActive(false);
        selectAugmenterData = null;
    }
}
