using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
//public class StageGroupData
//{
//    public StageData2[] stageDataArr;
//}

//[System.Serializable]
//public class StageData2
//{
//    public int Uid;
//    public int[] waveUidArr;
//    public int curWaveIdx;
//}


//[System.Serializable]
//public class WaveGroupData
//{
//    public WaveData[] waveDataArr;
//}
//[System.Serializable]
//public class WaveData
//{
//    public int Uid;
//    public int[] monsterSpwanUid;
//    public float waveTime;
//}



//[System.Serializable]
//public class MonsterSpwanGroupData
//{
//    public MonsterSpwanData[] monsterSpwanDataArr;
//}
//[System.Serializable]
//public class MonsterSpwanData
//{
//    public int Uid;
//    public int monsterID;
//    public float reSpwanTime;
//    public float timer;
//    public int monsterCount;
//}


//////////////////////////////////////
///
[System.Serializable]
public struct StageArrJson
{
    public StageData[] stageDataArr;
}

[System.Serializable]
public class StageData
{
    public int Uid;
    public int[] waveMonsterGroupId;
    public int stageInformation;
    public int bossMonsterId;
    public int curWaveIdx;
}

[System.Serializable]
public struct MonsterGroupArrJson
{
    public MonsterGroupJsonModel[] modelArr;
}

[System.Serializable]
public struct MonsterGroupJsonModel
{
    public int Uid;
    public int monsterId;
    public int count;
    public float firstSpawnTime;
    public float reSpawnTime;
    public float endSpawnTime;
}

[System.Serializable]
public class MonsterGroupData
{
    public int Uid;
    public List<MonsterSpawnData> monsterSpawnDataArr;
}

[System.Serializable]
public class MonsterSpawnData
{
    public int monsterId;
    public int count;
    public float firstSpawnTime;
    public float reSpawnTime;
    public float endSpawnTime;
    public float timer;
}
