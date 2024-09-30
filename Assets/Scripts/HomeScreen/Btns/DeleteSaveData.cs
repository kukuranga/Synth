using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteSaveData : MonoBehaviour
{

    public void DeleteSave()
    {
        SaveManager.Instance.DeleteSave();
        SceneManager.LoadScene(0);
    }
}
