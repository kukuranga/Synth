using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class SaveData
{
    public void Load()
    {
        UpdateGameStats();
        UpdateSynthStats();
        UpdateOptions();
        UpdateTutorialData();
    }
    
    public void Save()
    {
        SaveGameStats();
        SaveSynthStats();
        SaveUpdateOptions();
        SaveTutorialData();
    }

    #region Game Stats

    public int _LevelsCompleted;
    public int _HighestLevelAchieved;
    public long _TotalNumberOfRuns;
    public long _TotalPointsScored;
    public int _HighestPointTotal;
    public long _TotalNumberOfMovesUsed;
    public int _HighestMovesNumberAchieved;
    public int _TotalNumberOfUpgrades;
    
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

    #region Synth stats

    [Range(1, 11)] public int _Ci1p = 1;
    [Range(1, 11)] public int _Ci2p = 1;
    [Range(1, 11)] public int _Cu1p = 1;
    [Range(1, 11)] public int _Cu2p = 1;
    [Range(1, 11)] public int _Cu3p = 1;
    public int _UsedUpgradePoints;

    public void SaveSynthStats()
    {
        _Ci1p = SynthManager.Instance._Ci1P;
        _Ci2p = SynthManager.Instance._Ci2P;
        _Cu1p = SynthManager.Instance._Cu1P;
        _Cu2p = SynthManager.Instance._Cu2P;
        _Cu3p = SynthManager.Instance._Cu3P;
        _UsedUpgradePoints = SynthManager.Instance._UsedUpgradePoints;
    }

    public void UpdateSynthStats()
    {
        SynthManager.Instance._Ci1P = _Ci1p;
        SynthManager.Instance._Ci2P = _Ci2p;
        SynthManager.Instance._Cu1P = _Cu1p;
        SynthManager.Instance._Cu2P = _Cu2p;
        SynthManager.Instance._Cu3P = _Cu3p;
        SynthManager.Instance._UsedUpgradePoints = _UsedUpgradePoints; 
    }

    #endregion

    #region Options

    public float _MusicVolume;
    public float _fXVolume;
    public float _Sensitivity;

    public void SaveUpdateOptions()
    {
        _MusicVolume = AudioManager.Instance.GetMusicVolume();
        _fXVolume = AudioManager.Instance.GetFXVolume();
        _Sensitivity = GameManager.Instance._SwipeSensitivity;
    }

    public void UpdateOptions()
    {
        AudioManager.Instance.SetMusicVolume(_MusicVolume);
        AudioManager.Instance.SetFXVolume(_fXVolume);
        GameManager.Instance._SwipeSensitivity = _Sensitivity;
    }

    #endregion

    #region Tutorials

    public bool _StartButtonTutorial;

    public void SaveTutorialData()
    {
        _StartButtonTutorial = TutorialManager.Instance._StartButtonTutorial;
    }

    public void UpdateTutorialData()
    {
        TutorialManager.Instance._StartButtonTutorial = _StartButtonTutorial;
    }

    #endregion 
}
