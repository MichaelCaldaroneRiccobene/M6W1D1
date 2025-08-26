using System.IO;
using UnityEngine;

public class SaveSystem
{
    public static string GetPath() => Application.persistentDataPath + "/porn.data";

    public static bool Save(SaveData data)
    {
        string path = GetPath();

        string jsonString = JsonUtility.ToJson(data);

        File.WriteAllText(path, jsonString);

        return true;
    }

    public static bool DoesSaveFileExists() => File.Exists(GetPath());

    public static SaveData Load()
    {
        string path = GetPath();

        if (!DoesSaveFileExists()) return null;
        

        string jsonString = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(jsonString);

        return data;
    }
}
