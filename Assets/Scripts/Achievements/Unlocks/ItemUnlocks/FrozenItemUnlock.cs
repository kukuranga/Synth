using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Frozen Item Unlock", menuName = "Unlocks/FrozenItemUnlock", order = 51)]
public class FrozenItemUnlock : Unlock
{
    public override void Bonus()
    {
        GameManager.Instance._FrozenItemUnlocked = true;
    }
}
