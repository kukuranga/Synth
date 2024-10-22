using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopulateAchievemntPage : MonoBehaviour
{

    public GameObject _Prefab;
    public GameObject _Parent;
    public ScrollRect _ScrollRect;

    private void Start()
    {
        foreach(Achievement _a in AchievementManager.Instance._AllAchievements)
        {
            GameObject _g = Instantiate(_Prefab, _Parent.transform);
            _g.GetComponent<NodeAchievement>()._Achievement = _a;
        }

        _ScrollRect.verticalNormalizedPosition = 1f;
    }

}
