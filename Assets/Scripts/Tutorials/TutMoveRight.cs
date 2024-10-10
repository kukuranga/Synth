using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutMoveRight : MonoBehaviour
{
    public PageScroll _PS;

    public void MoveRight()
    {
        _PS.MoveToNextPage();
    }
}
