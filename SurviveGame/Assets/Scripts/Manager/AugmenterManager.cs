using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AugmenterManager : Singleton<AugmenterManager>
{
    private Dictionary<int, AugmenterData> augmenterDict = new Dictionary<int, AugmenterData>();
    private List<AugmenterData> augmenterList = new List<AugmenterData>();


    public override bool Initialize()
    {
        LoadAugmenterData();
        return true;
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
