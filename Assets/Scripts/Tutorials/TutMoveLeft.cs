using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutMoveLeft : MonoBehaviour
{

    public PageScroll _PS;

    public void MoveLeft()
    {
        _PS.MoveToPreviousPage();
    }

}
