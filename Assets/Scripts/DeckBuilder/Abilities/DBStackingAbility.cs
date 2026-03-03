using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DBStackingAbility", menuName = "DeckBuilder/StackingAbility")]

public class DBStackingAbility : DBAbility
{

    //todo: Might need to add an ID if more types of stacking ability exists

    public override void UseAbility()
    {
        Game2Manager.Instance.AddStackingYellow();
    }

}
