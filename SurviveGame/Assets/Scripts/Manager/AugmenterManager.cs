using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AugmenterManager : Singleton<AugmenterManager>
{
    private Dictionary<int, AugmenterData> augmenterDict = new Dictionary<int, AugmenterData>();
    //private Dictionary<int, List<AugmenterData>> augmenterGorupDict = new Dictionary<int, List<AugmenterData>>();
    private List<int> selectedAugmenterUidList = new List<int>();
    private List<AugmenterData> selectedAugmenterList = new List<AugmenterData>();

    private List<AugmenterData> augmenterList = new List<AugmenterData>();
    //private Queue<AugmenterData> notSelectedAugmenterQueue = new Queue<AugmenterData>();
    //private Queue<AugmenterData> selectedAugmenterQueue = new Queue<AugmenterData>();

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

    //public List<AugmenterData> GetRandomAugment()
    //{
    //    List<AugmenterData> result = new List<AugmenterData>();

    //    List<int> selectedGroupIds = new List<int>();
    //    List<int> selectedAugmentUids = new List<int>(selectedAugmentUidList);

    //    List<AugmenterData> allAugments = augmenterGorupDict.Values.SelectMany(group => group).ToList();
    //    allAugments = allAugments.OrderBy(a => Random.value).ToList();

    //    foreach (var augment in allAugments)
    //    {
    //        if (selectedAugmentUids.Contains(augment.augmentUid))
    //        {
    //            continue;
    //        }

    //        int groupCount = result.Count(a => a.augmentGroup == augment.augmentGroup);
    //        if (groupCount >= 1)
    //        {
    //            continue;
    //        }

    //        if (augment.isNotDuplicated && selectedGroupIds.Contains(augment.augmentGroup))
    //        {
    //            continue;
    //        }

    //        result.Add(augment);

    //        selectedGroupIds.Add(augment.augmentGroup);
    //        selectedAugmentUids.Add(augment.augmentUid);

    //        if (result.Count >= 3)
    //        {
    //            break;
    //        }
    //    }

    //    return result;
    //}



    private void LoadAugmenterData()
    {
        AugmenterArrJson augmenterJson = JsonController.ReadJson<AugmenterArrJson>("AugmenterData");
        int count = augmenterJson.augmenterArr.Length;
        for(int i =0; i < count; i++)
        {
            AugmenterData data = augmenterJson.augmenterArr[i];
            augmenterDict.Add(data.Uid, data);
            augmenterList.Add(data);
            //notSelectedAugmenterQueue.Enqueue(data);



            //if (!augmenterGorupDict.ContainsKey(data.groupId))
            //{
            //    augmenterGorupDict[data.groupId] = new List<AugmenterData>();
            //}
            //augmenterGorupDict[data.groupId].Add(data);
        }
    }
}
