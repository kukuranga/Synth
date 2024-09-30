using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Points Gain Achievement", menuName = "Achievements/Highest Points", order = 51)]
public class PointsAchievement : Achievement
{
    public int _PointsRequirement;

    public override bool Achieve()
    {
        if (_PointsRequirement <= StatsManager.Instance._HighestPointTotal)
        {
            return true;
        }
        return false;
    }
}
