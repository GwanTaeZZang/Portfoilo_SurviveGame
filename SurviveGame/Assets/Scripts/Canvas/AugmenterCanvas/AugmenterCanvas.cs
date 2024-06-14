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

    private List<AugmenterData> augmenterList;

    private List<AugmenterData> showAugmenterList = new List<AugmenterData>();
    private List<int> showAugmenterUid = new List<int>();
    private List<int> showAugmenterGroupId = new List<int>();


    //System.Random random = new System.Random();

    //List<AugmenterData> list = new List<AugmenterData>();
    //Queue<AugmenterData> augmenterDataQueue = new Queue<AugmenterData>();


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
            // Is Duplicat Check 
            if(!augmenter.isDuplicat && showAugmenterGroupId.Contains(augmenter.groupId))
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

            //int randomIdx = Random.Range(0, augmenterList.Count);
            //AugmenterData augmenterData = augmenterList[randomIdx];
            AugmenterData augmenterData = showAugmenterList[i];
            AugmenterElement element = augmenterElementList[i];

            Sprite icon = Resources.Load<Sprite>(augmenterData.augmenterSpritePath);
            element.SetAugmenterIcon(icon);
            element.SetAugmenterTitle(augmenterData.augmenterName);
            element.SetAugmenterInfo(augmenterData.augmenterContent);
        }
    }

    private void CopyAugmenterList()
    {
        List<AugmenterData> oriaugmenterList = AugmenterManager.getInstance.GetAugmenterList();
        augmenterList = new List<AugmenterData>();

        int count = oriaugmenterList.Count;
        for(int i =0; i < count; i++)
        {
            augmenterList.Add(oriaugmenterList[i]);
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
        showAugmenterList.Clear();

        UpdateAugmenterElementInfo();
    }

}
