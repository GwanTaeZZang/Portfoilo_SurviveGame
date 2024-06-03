using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    private Dictionary<int, MonsterGroupData> monsterGroupDict = new Dictionary<int, MonsterGroupData>();
    //private Dictionary<int, WaveData> waveDict = new Dictionary<int, WaveData>();
    private Dictionary<int, StageData> stageDict = new Dictionary<int, StageData>();

    public delegate void WaveDelegate(List<MonsterSpawnData> _monsterSpwanDataList, float _waveTime);
    public WaveDelegate OnWaveEvent;

    private List<MonsterSpawnData> monsterSpwanDataList = new List<MonsterSpawnData>();
    private int stageBaseUid = 4100;


    public override bool Initialize()
    {
        LoadMonsterSpwanData();
        LoadWaveData();
        LoadStageData();

        return base.Initialize();
    }

    public void StartWave()
    {
        Debug.Log("This Current Wave  : " + GetSelectedStage().curWaveIdx);

        StageData stageData = GetSelectedStage();
        int monsterGroupId = stageData.waveMonsterGroupId[stageData.curWaveIdx];

        MonsterGroupData monsterData = GetMonsterSpwanData(monsterGroupId);

        float waveTime = 30;

        int count = monsterData.monsterSpawnDataArr.Count;
        for (int i = 0; i < count; i++)
        {
            MonsterSpawnData data = monsterData.monsterSpawnDataArr[i];
            monsterSpwanDataList.Add(data);
        }

        OnWaveEvent?.Invoke(monsterSpwanDataList, waveTime);
    }

    public void EndWave()
    {
        MonsterManager.getInstance.EndWave();
        GetSelectedStage().curWaveIdx++;
        monsterSpwanDataList.Clear();

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

    //public WaveData GetWaveData(int _Uid)
    //{
    //    return waveDict[_Uid];
    //}

    public MonsterGroupData GetMonsterSpwanData(int _Uid)
    {
        return monsterGroupDict[_Uid];
    }

    private void LoadMonsterSpwanData()
    {
        MonsterGroupArrJsonModel monsterSpwanGroupData = JsonController.ReadJson<MonsterGroupArrJsonModel>("MonsterSpwanData");

        int count = monsterSpwanGroupData.modelArr.Length;

        for(int i=0; i < count; i++)
        {
            MonsterGroupJsonModel data = monsterSpwanGroupData.modelArr[i];
            MonsterGroupData monsterData = new MonsterGroupData();
            monsterData.Uid = data.Uid;
            monsterData.monsterSpawnDataArr = new List<MonsterSpawnData>();

            MonsterSpawnData model = new MonsterSpawnData();
            model.monsterId = data.monsterId;
            model.count = data.count;
            model.firstSpawnTime = data.firstSpawnTime;
            model.reSpawnTime = data.reSpawnTime;
            model.endSpawnTime = data.endSpawnTime;

            if (!monsterGroupDict.ContainsKey(data.Uid))
            {
                monsterGroupDict.Add(data.Uid, monsterData);
                monsterGroupDict[data.Uid].monsterSpawnDataArr.Add(model);
            }
            else
            {
                monsterGroupDict[data.Uid].monsterSpawnDataArr.Add(model);
            }
        }
    }

    private void LoadWaveData()
    {
        //WaveGroupData waveGroupData = JsonController.ReadJson<WaveGroupData>("WaveData");

        //int count = waveGroupData.waveDataArr.Length;

        //for (int i = 0; i < count; i++)
        //{
        //    WaveData data = waveGroupData.waveDataArr[i];
        //    if(data.Uid != 0 && !waveDict.ContainsKey(data.Uid))
        //    {
        //        waveDict.Add(data.Uid, data);
        //    }
        //}

    }

    private void LoadStageData()
    {
        StageArrJsonModel stageData = JsonController.ReadJson<StageArrJsonModel>("StageData");
        int count = stageData.stageDataArr.Length;

        for (int i = 0; i < count; i++)
        {
            StageData data = stageData.stageDataArr[i];
            if (!stageDict.ContainsKey(data.Uid))
            {
                stageDict.Add(data.Uid, data);
            }
        }
    }
}
