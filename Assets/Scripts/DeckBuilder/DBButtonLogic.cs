using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBButtonLogic : MonoBehaviour
{
    //Handles the buttons for the DBButtons
    public void AddUnitOnClick()
    {
        Game2Manager.Instance.SetUnit();
    }

    public void EndOnClick()
    {
        Game2Manager.Instance.EndSelection();
    }

}
