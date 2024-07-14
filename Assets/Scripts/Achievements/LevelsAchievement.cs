using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Gain Achievement", menuName = "Achievements/Highest Level", order = 51)]
public class LevelsAchievement : Achievement
{
    public int _LevelRequirement;

    public override bool Achieve()
    {
        if(_LevelRequirement <= StatsManager.Instance._HighestLevelAchieved)
        {
            return true;
        }
        return false;
    }
}
