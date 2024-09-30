using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gold Item Unlock", menuName = "Unlocks/GoldItemUnlock", order = 51)]
public class GoldItemUnlock : Unlock
{
    public override void Bonus()
    {
        GameManager.Instance._GoldItemUnclocked = true;
    }
}
