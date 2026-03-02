using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DBBringUnit", menuName = "DeckBuilder/BringUnit")]

public class DBBringUnitOnPull : DBAbility
{

    public override void OnPullAbility()
    {

        Game2Manager.Instance.BringAnotherUnit();
    }
}
