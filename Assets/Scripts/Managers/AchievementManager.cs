using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : Singleton<AchievementManager>
{
    //this script will keep track of all information protraying to the achievements
   //todo: for each achievement that has been unlcoked, set a flag in the game manager or coresponding manager to be altered by the achievement
    
    //list of all achievements
    //list of not achieved achievements
    //list of pending achievements //used to show the player after a run what achievements they have just earned
    //list of all achievements completed

    //Problem? how to correctly assign the rewards for each achievement

    public List<Achievement> _AllAchievements;
    public List<Achievement> _NotCompleatedAchievements;
    public List<Achievement> _PendingAchievements;
    public List<Achievement> _CompletedAchievements;

    private void Start()
    {
        foreach (Achievement _Ach in _AllAchievements)
        {
            if (_Ach.Achieve())
                _PendingAchievements.Add(_Ach);
            else
                _NotCompleatedAchievements.Add(_Ach);
        }
    }

    public void CheckAchievements()
    {
        //Maybe change this when save system is implemented
        //fix to not include repeat achievements
        foreach (Achievement _Ach in _NotCompleatedAchievements)
        {
            if (_Ach.Achieve())
                _PendingAchievements.Add(_Ach);
        }

        //CheckPendingAchievements();
        CheckUpgrades();
    }

    //To be called when the achievement tab is opened
    public void OpenAchievementsTab()
    {
        CheckPendingAchievements();
    }

    public void CheckPendingAchievements()
    {
        //Add A visual pop up in the home screen with the pending achievements info and reward
        //on close remove the pending achievement and add it to the completed file
        //if the list isnt empty open a new popup and check for the achievement again

        //achivement.unlock.bonus(); //set this for each achievemnt in the completed tab
        if(_PendingAchievements.Count > 0)
        {
            _CompletedAchievements.Add(_PendingAchievements[0]);
            _PendingAchievements.RemoveAt(0);
            CheckPendingAchievements();
        }
    }

    public void CheckUpgrades()
    {
        foreach(Achievement _ach in _CompletedAchievements)
        {
            _ach._Unlock.Bonus();
            _ach._Unlocked = true;
        }
    }

    public bool HasPendingAchievements()
    {
        if(_PendingAchievements.Count > 0)
        {
            return true;
        }

        return false;
    }
}
