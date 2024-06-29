using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController
{

    private List<MonsterSpawnData> MosnterSpwanDataList = new List<MonsterSpawnData>();
    private MonsterSpawnController monsterSpawnContoller;
    private InGameCanvas inGameCanvas;
    private float waveTime;
    private float bossTimer;
    private bool isWave = false;
    private bool isBossWave = false;

    public void Initialized(InGameCanvas _inGameCanvas, TileMapModel _mapData)
    {
        StageManager.getInstance.OnWaveEvent = SetWave;
        monsterSpawnContoller = new MonsterSpawnController();
        monsterSpawnContoller.Initialized(_mapData);

        inGameCanvas = _inGameCanvas;
    }

    private void SetWave(List<MonsterSpawnData> _monsterSpwanDataList, float _waveTime, int _curWaveIdx, bool _isBossWave)
    {
        waveTime = _waveTime;
        MosnterSpwanDataList = _monsterSpwanDataList;
        inGameCanvas.ShowWave(_curWaveIdx + 1);

        int count = MosnterSpwanDataList.Count;
        Debug.Log("This Wave Monster Type Count  : " + count);
        for (int i = 0; i < count; i++)
        {
            Debug.Log("This Wave Monster Type   : " + MosnterSpwanDataList[i].monsterId + " Monster Count  : " + MosnterSpwanDataList[i].count);
        }

        isBossWave = _isBossWave;
        isWave = true;
    }

    public void UpdateWave()
    {
        //monsterSpawnContoller.UpdateVecter();



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

            if (isBossWave)
            {
                bossTimer += Time.deltaTime;
                if(bossTimer > 3)
                {
                    monsterSpawnContoller.SpawnBossMonster(-1, inGameCanvas);
                    isBossWave = false;

                }
            }
        }

    }

    public void SpawnBossMosnter()
    {
        monsterSpawnContoller.SpawnBossMonster(-1, inGameCanvas);
    }

}
