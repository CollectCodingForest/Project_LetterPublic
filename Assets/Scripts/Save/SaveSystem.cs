using System.IO;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveGame(SaveData saveData, string fileName)
    {
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/" + fileName + ".json", json);
    }

    public static SaveData LoadGame(string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName + ".json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
            return saveData;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
