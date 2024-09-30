using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats UI Unlock", menuName = "Unlocks/UIStatsUnlock", order = 51)]
public class StatsUnlock : Unlock
{

    public override void Bonus()
    {
        GameManager.Instance.StatsUnlocked = true;
    }
}
