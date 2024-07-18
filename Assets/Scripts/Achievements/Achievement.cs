using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AchievementDifficulty
{
    Easy,
    Normal,
    Hard,
    VeryHard
}

public class Achievement : ScriptableObject
{
    public Sprite _Sprite;
    public string _Name;
    public string _Description;
    public AchievementDifficulty _Difficulty;
    public Unlock _Unlock;
    public bool _Unlocked;

    public virtual bool Achieve()
    {
        return false;
    }
}
