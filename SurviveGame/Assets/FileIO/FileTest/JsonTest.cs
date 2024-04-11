using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // 저장 등 파일 관리를 위해 추가 


[System.Serializable]
public class TmpData
{
    public int[,] aa;
    public string name;
    public int age;
    public int level;
    public bool isDead;
}

public class JsonTest : MonoBehaviour
{
    public string filename;
    public TmpData tmpData;

    private void Start()
    {
        tmpData = JsonController.ReadJson<TmpData>("tmpData");
        Debug.Log(tmpData.name);
        Debug.Log(tmpData.age);
        Debug.Log(tmpData.level);
        Debug.Log(tmpData.isDead);


        TmpData tt = new TmpData();
        tt.name = "aaaaaaa";
        tt.age = 10000;

        JsonController.WriteJson<TmpData>("aadata", tt);

        tmpData = JsonController.ReadJson<TmpData>("aadata");
        Debug.Log(tmpData.name);
        Debug.Log(tmpData.age);
        Debug.Log(tmpData.level);
        Debug.Log(tmpData.isDead);

    }


    [ContextMenu("To Json Data")]  // 컴포넌트 메뉴에 아래 함수를 호출하는 To Json Data 라는 명렁어가 생성됨 
    public void SaveTmpData()
    {
        // ToJson을 사용하면 JSON형태로 포멧팅된 문자열이 생성된다
        // 2번째 프로퍼티인 perttyPrint 값을 true로 지정하면 깔끔하게 생성됨
        string jsonData = JsonUtility.ToJson(tmpData, true);
        // 데이터를 저장할 경로 지정
        string path = Path.Combine(Application.dataPath, "FileIO", "Json", filename + ".json");
        // 파일 생성 및 저장
        File.WriteAllText(path, jsonData);
    }

    [ContextMenu ("From Json Data")]
    public void LoadPlayerDataFromJson()
    {
        string path = Path.Combine(Application.dataPath, "FileIO", "Json", filename + ".json");
        string jsonData = File.ReadAllText(path);
        tmpData = JsonUtility.FromJson<TmpData>(jsonData);
    }
}
