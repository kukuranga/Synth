using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBBackToGameButton : MonoBehaviour
{
    public void OnClick()
    {
        VFX2Manager.Instance.CloseShopVFX();
        //Game2Manager.Instance.UpdateGameState(GameState.RoundStart);
    }
}
