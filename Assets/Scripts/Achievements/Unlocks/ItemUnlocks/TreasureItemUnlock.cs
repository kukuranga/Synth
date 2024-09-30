using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Treasure Item Unlock", menuName = "Unlocks/TreasureItemUnlock", order = 51)]

public class TreasureItemUnlock : Unlock
{
    public override void Bonus()
    {
        GameManager.Instance._TreasureItemUnlocked = true;
    }
}
