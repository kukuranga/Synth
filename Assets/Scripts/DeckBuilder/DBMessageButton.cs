using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBMessageButton : MonoBehaviour
{
    public void OnClick()
    {
        Game2Manager.Instance.UpdateGameState(GameState.Shop);  
    }
}
