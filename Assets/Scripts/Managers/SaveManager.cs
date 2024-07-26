using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : Singleton<SaveManager>
{

    public void SaveGame()
    {
        SaveData data = new SaveData();
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
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            data.Load();
            //player.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
            //player.score = data.score;

            Debug.Log("Game Loaded");
        }
        else
        {
            Debug.LogWarning("Save file not found");
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
