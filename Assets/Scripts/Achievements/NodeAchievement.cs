using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodeAchievement : MonoBehaviour
{
    public Achievement _Achievement;
    public Sprite _NotUnlockedImage;
    public Image _Image;
    public Image _Background;
    public TextMeshProUGUI _Name;
    public TextMeshProUGUI _Desc;

    public Color _NotCompletedColor;
    public Color _EasyBackground;
    public Color _NormalBackground;
    public Color _HardBackground;
    public Color _VeryHardBackground;


    private void Start()    
    {
        _Name.text = _Achievement._Name;
        _Desc.text = _Achievement._Description;

        if (_Achievement._Unlocked)
        {
            _Image.sprite = _Achievement._Sprite;

            if (_Achievement._Difficulty == AchievementDifficulty.Easy)
                _Background.color = _EasyBackground;
            else if (_Achievement._Difficulty == AchievementDifficulty.Normal)
                _Background.color = _NormalBackground;
            else if (_Achievement._Difficulty == AchievementDifficulty.Hard)
                _Background.color = _HardBackground;
            else if (_Achievement._Difficulty == AchievementDifficulty.VeryHard)
                _Background.color = _VeryHardBackground;
        }
        else
        {
            _Image.sprite = _NotUnlockedImage;
            _Background.color = _NotCompletedColor;
            _Name.text = "";
        }
        
    }
}
