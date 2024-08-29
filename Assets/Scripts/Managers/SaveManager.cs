using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : Singleton<SaveManager>
{
    public SaveData data;

    public void SaveGame()
    {
        if (data == null)
        {
            data = new SaveData(); // Ensure that 'data' is instantiated
        }

        data.Save();
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        Debug.Log("Game Saved");
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<SaveData>(json);
            data.Load();

            Debug.Log("Game Loaded");
        }
        else
        {
            Debug.LogWarning("Save file not found");

            data = new SaveData();
        }
    }

    public void DeleteSave()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Save file deleted");
        }
        else
        {
            Debug.LogWarning("Save file not found");
        }
    }
}
