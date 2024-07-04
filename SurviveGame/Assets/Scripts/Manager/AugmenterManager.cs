using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AugmenterManager : Singleton<AugmenterManager>
{
    private Dictionary<int, AugmenterData> augmenterDict = new Dictionary<int, AugmenterData>();
    private List<int> selectedAugmenterUidList = new List<int>();
    private List<AugmenterData> selectedAugmenterList = new List<AugmenterData>();

    private List<AugmenterData> augmenterList = new List<AugmenterData>();

    public override bool Initialize()
    {
        LoadAugmenterData();
        selectedAugmenterList.Clear();
        selectedAugmenterUidList.Clear();
        return true;
    }

    public List<AugmenterData> GetAugmenterList()
    {
        return augmenterList;
    }

    public void SetSelectedAugmenterUid(int _uid)
    {
        selectedAugmenterUidList.Add(_uid);
    }

    public void SetSeletedAugmenterList(AugmenterData _selectedAugmenter)
    {
        selectedAugmenterList.Add(_selectedAugmenter);
    }

    public List<int> GetSelectedAugmenterUidList()
    {
        return selectedAugmenterUidList;
    }

    public AugmenterData GetAugmenterData(int _uid)
    {
        return augmenterDict[_uid];
    }


    private void LoadAugmenterData()
    {
        AugmenterArrJson augmenterJson = JsonController.ReadJson<AugmenterArrJson>("AugmenterData");
        int count = augmenterJson.augmenterArr.Length;
        for(int i =0; i < count; i++)
        {
            AugmenterData data = augmenterJson.augmenterArr[i];
            augmenterDict.Add(data.Uid, data);
            augmenterList.Add(data);
        }
    }
}
