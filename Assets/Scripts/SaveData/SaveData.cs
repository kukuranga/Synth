using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    //includes all data to be saved 
    
    //synth values
    //achievements unlocked 
    //items unlocked

    public void Load()
    {
        UpdateGameStats();
    }
    
    public void Save()
    {
        SaveGameStats();
    }

    #region Game Stats

    private int _LevelsCompleted;
    private int _HighestLevelAchieved;
    private long _TotalNumberOfRuns;
    private long _TotalPointsScored;
    private int _HighestPointTotal;
    private long _TotalNumberOfMovesUsed;
    private int _HighestMovesNumberAchieved;
    private int _TotalNumberOfUpgrades;
    
    public void SaveGameStats()
    {
        _LevelsCompleted = StatsManager.Instance._LevelsCompleted;
        _HighestLevelAchieved = StatsManager.Instance._HighestLevelAchieved;
        _TotalNumberOfRuns = StatsManager.Instance._TotalNumberOfRuns;
        _TotalPointsScored = StatsManager.Instance._TotalPointsScored;
        _HighestPointTotal = StatsManager.Instance._HighestPointTotal;
        _TotalNumberOfMovesUsed = StatsManager.Instance._TotalNumberOfMovesUsed;
        _HighestMovesNumberAchieved = StatsManager.Instance._HighestMovesNumberAchieved;
        _TotalNumberOfUpgrades = StatsManager.Instance._TotalNumberOfUpgrades;
    }

    public void UpdateGameStats()
    {
        StatsManager.Instance._LevelsCompleted = _LevelsCompleted;
        StatsManager.Instance._HighestLevelAchieved = _HighestLevelAchieved;
        StatsManager.Instance._TotalNumberOfRuns = _TotalNumberOfRuns;
        StatsManager.Instance._TotalPointsScored = _TotalPointsScored;
        StatsManager.Instance._HighestPointTotal = _HighestPointTotal;
        StatsManager.Instance._TotalNumberOfMovesUsed = _TotalNumberOfMovesUsed;
        StatsManager.Instance._HighestMovesNumberAchieved = _HighestMovesNumberAchieved;
        StatsManager.Instance._TotalNumberOfUpgrades = _TotalNumberOfUpgrades;
    }
    #endregion


}
