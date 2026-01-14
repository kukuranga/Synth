using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{

    //Todo: Add buttons for options and unlocks
    public bool _GameSelect;
    public bool _LoadDB;

    public void LoadLevel()
    {
        if (_GameSelect)
        {
            SceneLoader.Instance.UnloadScene(GameManager.Instance._GameSelect);
            LoadGameGameSelect();
        }
        else
        {
            SceneLoader.Instance.UnloadScene(GameManager.Instance._Homepage);
            SceneLoader.Instance.UnloadScene(GameManager.Instance._GameOverScene);
            LoadGameHomepage();
        }
    }

    public void LoadGameHomepage()
    {
        if (_LoadDB)
            SceneLoader.Instance.LoadScene(GameManager.Instance._DBLevelToLoad);
        else
        {
            SceneLoader.Instance.LoadScene(GameManager.Instance._LevelToLoad);
            SynthManager.Instance.ResetSynthValues();
            GameManager.Instance.GameStart();
        }
    }

    public void LoadGameGameSelect()
    {
        if (_LoadDB)
            SceneLoader.Instance.LoadScene(GameManager.Instance._DBLevelToLoad);
        else
        {
            SceneLoader.Instance.LoadScene(GameManager.Instance._Homepage);
            //SynthManager.Instance.ResetSynthValues();
            //GameManager.Instance.GameStart();
        }

    }
}
