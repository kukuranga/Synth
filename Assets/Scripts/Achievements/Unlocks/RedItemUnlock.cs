using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Red Item Unlock", menuName = "Unlocks/RedItemUnlock", order = 51)]

public class RedItemUnlock : Unlock
{
    public override void Bonus()
    {
        GameManager.Instance._RedItemUnlocked = true;
    }
}
