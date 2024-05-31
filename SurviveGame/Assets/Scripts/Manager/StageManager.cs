using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    private Dictionary<int, MonsterSpwanData> monsterSpwanDict = new Dictionary<int, MonsterSpwanData>();
    private Dictionary<int, WaveData> waveDict = new Dictionary<int, WaveData>();
    private Dictionary<int, StageData> stageDict = new Dictionary<int, StageData>();

    private int stageBaseUid = 4100;

    public override bool Initialize()
    {
        LoadMonsterSpwanData();
        LoadWaveData();
        LoadStageData();

        return base.Initialize();
    }

    public int GetStageCount()
    {
        return stageDict.Count;
    }

    public void SelectedStageIdx(int _idx)
    {
        stageBaseUid += _idx;
    }

    public StageData GetSelectedStage()
    {
        return stageDict[stageBaseUid];
    }

    public WaveData GetWaveData(int _Uid)
    {
        return waveDict[_Uid];
    }

    public MonsterSpwanData GetMonsterSpwanData(int _Uid)
    {
        return monsterSpwanDict[_Uid];
    }

    private void LoadMonsterSpwanData()
    {
        MonsterSpwanGroupData monsterSpwanGroupData = JsonController.ReadJson<MonsterSpwanGroupData>("MonsterSpwanData");

        int count = monsterSpwanGroupData.monsterSpwanDataArr.Length;

        for(int i=0; i < count; i++)
        {
            MonsterSpwanData data = monsterSpwanGroupData.monsterSpwanDataArr[i];
            monsterSpwanDict.Add(data.Uid, data);
        }
    }

    private void LoadWaveData()
    {
        WaveGroupData waveGroupData = JsonController.ReadJson<WaveGroupData>("WaveData");

        int count = waveGroupData.waveDataArr.Length;

        for (int i = 0; i < count; i++)
        {
            WaveData data = waveGroupData.waveDataArr[i];
            if(data.Uid != 0)
            {
                waveDict.Add(data.Uid, data);
            }
        }

    }

    private void LoadStageData()
    {
        StageGroupData stageData = JsonController.ReadJson<StageGroupData>("StageData");
        int count = stageData.stageDataArr.Length;

        for (int i = 0; i < count; i++)
        {
            StageData data = stageData.stageDataArr[i];
            stageDict.Add(data.Uid, data);
        }
    }
}
