using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreenManager : MonoBehaviour
{
    private void Start()
    {
        AchievementManager.Instance.CheckAchievements();
    }
}
