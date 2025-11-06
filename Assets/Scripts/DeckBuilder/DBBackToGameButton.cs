using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBBackToGameButton : MonoBehaviour
{
    public void OnClick()
    {
        Game2Manager.Instance.UpdateGameState(GameState.RoundStart);
    }
}
