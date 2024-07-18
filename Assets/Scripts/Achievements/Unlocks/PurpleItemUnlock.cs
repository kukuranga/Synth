using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Purple Item Unlock", menuName = "Unlocks/PurpleItemUnlock", order = 51)]

public class PurpleItemUnlock : Unlock
{
    public override void Bonus()
    {
        GameManager.Instance._PurpleItemUnlocked = true;
    }
}
