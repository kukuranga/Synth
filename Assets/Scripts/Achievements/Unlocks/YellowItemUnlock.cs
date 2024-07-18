using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Yellow Item Unlock", menuName = "Unlocks/YellowItemUnlock", order = 51)]

public class YellowItemUnlock : Unlock
{
    public override void Bonus()
    {
        GameManager.Instance._YellowItemUnlocked = true;
    }
}
