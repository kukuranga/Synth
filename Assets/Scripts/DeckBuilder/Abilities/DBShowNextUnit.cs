using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DBShowNextUnit", menuName = "DeckBuilder/ShowNextUnit")]

public class DBShowNextUnit : DBAbility
{
    public override void ActivateAbility()
    {

        Game2Manager.Instance.SetNextUnitShow();
    }
}
