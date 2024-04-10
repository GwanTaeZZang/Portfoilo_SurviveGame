using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class JsonController
{
    public static T ReadJson<T> (string _fileName)
    {
        string path = Path.Combine(Application.dataPath, "FileIO", "Json", _fileName + ".json");
        string jsonData = File.ReadAllText(path);
        T t = JsonUtility.FromJson<T>(jsonData);

        return t;
    }

    public static void WriteJson<T> (string _fileName, T t)
    {
        string jsonData = JsonUtility.ToJson(t, true);
        string path = Path.Combine(Application.dataPath, "FileIO", "Json", _fileName + ".json");
        File.WriteAllText(path, jsonData);

    }
}
