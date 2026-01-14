using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DBKick", menuName = "DeckBuilder/KickAbility")]
public class DBKickAbility : DBAbility
{

    public override void ActivateAbility()
    {
        base.ActivateAbility();

        //Set The Gamestate to the pick a unit to kick
        Game2Manager.Instance.KickUnitSetup(_OwnerUnit);


    }

}
