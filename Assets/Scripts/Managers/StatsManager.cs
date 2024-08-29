using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : Singleton<StatsManager>
{
    //This scriipt keeps track of all stats in the game.
    public int _LevelsCompleted;
    public int _HighestLevelAchieved;
    public long _TotalNumberOfRuns;
    public long _TotalPointsScored;
    public int _HighestPointTotal;
    public long _TotalNumberOfMovesUsed;
    public int _HighestMovesNumberAchieved;
    public int _TotalNumberOfUpgrades;

    public void AddToLevelsCompleted(int _val)
    {
        _LevelsCompleted += _val;
    }

    public void CheckHighestLevelCompleted(int _val)
    {
        if (_val > _HighestLevelAchieved)
            _HighestLevelAchieved = _val;
    }

    public void AddToTotalNumberOfRuns(int _val)
    {
        _TotalNumberOfRuns += _val;
    }

    public void AddToTotalPointsAchieved(int _val)
    {
        _TotalPointsScored += _val;
    }

    public void CheckHighestPointTotal(int _val)
    {
        if (_HighestPointTotal < _val)
            _HighestPointTotal = _val;
    }

    public void AddToTotalMovesUsed(int _val)
    {
        _TotalNumberOfMovesUsed += _val;
    }

    public void CheckHighestMoveCount(int _val)
    {
        if (_HighestMovesNumberAchieved < _val)
            _HighestMovesNumberAchieved = _val;
    }

    public void AddToTotalNumberOfUpgrades(int _val)
    {
        _TotalNumberOfUpgrades += _val;
    }
}
