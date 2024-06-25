using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController
{

    private List<MonsterSpawnData> MosnterSpwanDataList = new List<MonsterSpawnData>();
    private MonsterSpawnController monsterSpawnContoller;
    private InGameCanvas inGameCanvas;
    private float waveTime;
    private bool isWave = false;

    public void Initialized(InGameCanvas _inGameCanvas)
    {
        StageManager.getInstance.OnWaveEvent = SetWave;
        monsterSpawnContoller = new MonsterSpawnController();
        monsterSpawnContoller.Initialized();

        inGameCanvas = _inGameCanvas;
    }

    private void SetWave(List<MonsterSpawnData> _monsterSpwanDataList, float _waveTime)
    {
        waveTime = _waveTime;
        MosnterSpwanDataList = _monsterSpwanDataList;


        int count = MosnterSpwanDataList.Count;
        Debug.Log("This Wave Monster Type Count  : " + count);
        for (int i = 0; i < count; i++)
        {
            Debug.Log("This Wave Monster Type   : " + MosnterSpwanDataList[i].monsterId + " Monster Count  : " + MosnterSpwanDataList[i].count);
        }

        isWave = true;

    }

    public void UpdateWave()
    {
        if (isWave)
        {
            waveTime -= Time.deltaTime;
            inGameCanvas.SetwaveTimeText((int)waveTime);

            if (waveTime < 0)
            {
                bool isGameClear = StageManager.getInstance.EndWave();
                if (!isGameClear)
                {
                    int curWaveIdx = StageManager.getInstance.GetCurrentWave();
                    if (curWaveIdx == 1 || curWaveIdx == 6 || curWaveIdx == 11)
                    {
                        UIManager.getInstance.Show<AugmenterCanvas>("Canvas/AugmenterCanvas");
                    }
                    else
                    {
                        UIManager.getInstance.Show<ShopCanvas>("Canvas/ShopCanvas");
                    }
                }



                isWave = false;
            }

            int count = MosnterSpwanDataList.Count;
            for (int i = 0; i < count; i++)
            {
                MonsterSpawnData monster = MosnterSpwanDataList[i];
                monster.timer += Time.deltaTime;

                if (monster.timer > monster.reSpawnTime)
                {
                    monsterSpawnContoller.SpawnMonster(monster.count, monster.monsterId);
                    monster.timer = 0;
                }
            }
        }

    }

    public void TempSpawnBossMosnter()
    {
        monsterSpawnContoller.SpawnBossMonster(-1);
    }

}
