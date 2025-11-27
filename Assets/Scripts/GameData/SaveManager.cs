using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveManager
{
    public static void SaveByJson(string saveFileName,object data)
    {
        var json = JsonUtility.ToJson(data);
        string path = Path.Combine(Application.persistentDataPath,saveFileName);
        File.WriteAllText(path,json);
    }

    public static T LoadFromJson<T>(string saveFileName)
    {
        string path = Path.Combine(Application.persistentDataPath, saveFileName);
        var json = File.ReadAllText(path);  
        T data = JsonUtility.FromJson<T>(json);
        return data;
    }

    public static void DeleteSaveFile(string saveFileName)
    {
        string path = Path.Combine(Application.persistentDataPath, saveFileName);
        File.Delete(path);
    }
}
