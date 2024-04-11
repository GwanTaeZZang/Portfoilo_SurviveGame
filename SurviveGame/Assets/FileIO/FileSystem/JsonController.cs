using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class JsonController
{
    public static T ReadJson<T> (string _fileName)
    {
        //string path = Path.Combine(Application.dataPath, "FileIO", "Json", _fileName + ".json");
        //string jsonData = File.ReadAllText(path);
        //T t = JsonUtility.FromJson<T>(jsonData);

        //return t;


        // FileStream 사용
        string path = Path.Combine(Application.dataPath, "FileIO", "Json", _fileName + ".json");
        FileStream fs = new FileStream(path, FileMode.Open);
        StreamReader stream = new StreamReader(fs);

        string data = stream.ReadToEnd();
        T t = JsonUtility.FromJson<T>(data);
        stream.Close();

        return t;

    }

    public static void WriteJson<T> (string _fileName, T t)
    {
        string path = Path.Combine(Application.dataPath, "FileIO", "Json", _fileName + ".json");
        FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
        StreamWriter writer = new StreamWriter(fs);

        string jsonData = JsonUtility.ToJson(t, true);
        writer.Write(jsonData);
        writer.Close();

    }
}
