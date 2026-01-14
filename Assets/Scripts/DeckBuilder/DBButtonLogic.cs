using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBButtonLogic : MonoBehaviour
{
    //Handles the buttons for the DBButtons
    public void AddUnitOnClick()
    {
        if (Game2Manager.Instance._gameState == GameState.GamePlay)
        {
            DBMessageManager.Instance.ClearMessage();
            Game2Manager.Instance.SetUnit();
        }
    }

    public void EndOnClick()
    {
        if (Game2Manager.Instance._gameState == GameState.GamePlay)
        {
            DBMessageManager.Instance.ClearMessage();
            Game2Manager.Instance.EndSelection();
        }
    }

}
