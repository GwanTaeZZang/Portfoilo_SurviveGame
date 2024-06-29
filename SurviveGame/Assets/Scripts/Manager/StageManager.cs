using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    private Dictionary<int, MonsterGroupData> monsterGroupDict = new Dictionary<int, MonsterGroupData>();
    private Dictionary<int, StageData> stageDict = new Dictionary<int, StageData>();

    public delegate void WaveDelegate(List<MonsterSpawnData> _monsterSpwanDataList, float _waveTime, int _curWaveIdx, bool _isBossWave);
    public WaveDelegate OnWaveEvent;

    private List<MonsterSpawnData> monsterSpwanDataList = new List<MonsterSpawnData>();
    private int stageBaseUid = 4100;

    private bool isGameClear = false;

    public override bool Initialize()
    {
        LoadMonsterSpwanData();
        LoadStageData();

        return base.Initialize();
    }

    public void StartWave()
    {
        if (isGameClear)
        {
            return;
        }

        //if(GetSelectedStage().curWaveIdx == GetSelectedStage().waveMonsterGroupId.Length)
        //{
        //    Debug.Log("Game Clear");
        //    return;
        //}

        Debug.Log("This Current Wave  : " + GetSelectedStage().curWaveIdx);

        StageData stageData = GetSelectedStage();
        int monsterGroupId = stageData.waveMonsterGroupId[stageData.curWaveIdx];

        MonsterGroupData monsterData = GetMonsterSpwanData(monsterGroupId);

        float waveTime = 40;

        int count = monsterData.monsterSpawnDataArr.Count;
        for (int i = 0; i < count; i++)
        {
            MonsterSpawnData data = monsterData.monsterSpawnDataArr[i];
            monsterSpwanDataList.Add(data);
        }

        PlayerManager.getInstance.ResetPlayer();

        if (GetSelectedStage().curWaveIdx == GetSelectedStage().waveMonsterGroupId.Length - 1)
        {
            waveTime = 60;
            Debug.Log("Last Wave");
            OnWaveEvent?.Invoke(monsterSpwanDataList, waveTime, GetSelectedStage().curWaveIdx, true);
        }
        else
        {
            OnWaveEvent?.Invoke(monsterSpwanDataList, waveTime, GetSelectedStage().curWaveIdx, false);
        }
    }

    public bool EndWave()
    {
        MonsterManager.getInstance.EndWave();
        GetSelectedStage().curWaveIdx++;
        monsterSpwanDataList.Clear();

        if (GetSelectedStage().curWaveIdx == GetSelectedStage().waveMonsterGroupId.Length)
        {
            Debug.Log("Game Clear");
            isGameClear = true;

            UIManager.getInstance.Show<GameClearCanvas>("Canvas/GameClearCanvas");
        }
        return isGameClear;
    }

    public int GetStageCount()
    {
        return stageDict.Count;
    }

    public void SelectedStageIdx(int _idx)
    {
        stageBaseUid += _idx;
    }

    public int GetCurrentWave()
    {
        return stageDict[stageBaseUid].curWaveIdx;
    }

    public StageData GetSelectedStage()
    {
        return stageDict[stageBaseUid];
    }

    public MonsterGroupData GetMonsterSpwanData(int _Uid)
    {
        return monsterGroupDict[_Uid];
    }

    private void LoadMonsterSpwanData()
    {
        MonsterGroupArrJson monsterSpwanGroupData = JsonController.ReadJson<MonsterGroupArrJson>("MonsterSpwanData");

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


    private void LoadStageData()
    {
        StageArrJson stageData = JsonController.ReadJson<StageArrJson>("StageData");
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
