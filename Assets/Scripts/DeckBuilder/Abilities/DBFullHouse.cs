using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DBFullHouse", menuName = "DeckBuilder/FullHouseAbility")]

public class DBFullHouse : DBAbility
{

    public int _AmountToAdd;

    public override void UseAbility()
    {
        base.UseAbility();
        if(Game2Manager.Instance._ContainerManager._NumberOfUnitsPulled == Game2Manager.Instance._ActiveContainers)
        {
            Game2Manager.Instance.AddYellowResource(_AmountToAdd);
        }

    }

}
