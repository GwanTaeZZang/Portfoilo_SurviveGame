using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StageData
{
    public int Uid;
    public int[] waveUidArr;
    public int curWaveIdx;
}


[System.Serializable]
public class WaveGroupData
{
    public WaveData[] waveDataArr;
}
[System.Serializable]
public class WaveData
{
    public int Uid;
    public int[] monsterSpwanUid;
    public float waveTime;
}



[System.Serializable]
public class MonsterSpwanGroupData
{
    public MonsterSpwanData[] monsterSpwanDataArr;
}
[System.Serializable]
public class MonsterSpwanData
{
    public int Uid;
    public int monsterID;
    public float reSpwanTime;
    public int monsterCount;
}
