using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : Singleton<SpriteManager>
{
    private string _MageSpritePath = "GameSprites/Texture/Mage";
    private string _ColorlessPatrh = "GameSprites/Texture/Color";

    public Sprite[] _MageloadedSprites;

    void Start()
    {
        LoadSprites();
    }

    void LoadSprites()
    {
        // Load all sprites from the specified folder path
        //if(GameManager.Instance._SetColors)
            //_MageloadedSprites = Resources.LoadAll<Sprite>(_ColorlessPatrh);
        //else
            _MageloadedSprites = Resources.LoadAll<Sprite>(_MageSpritePath);

        if (_MageloadedSprites.Length > 0)
        {
            foreach (Sprite sprite in _MageloadedSprites)
            {
                Debug.Log("Loaded Sprite: " + sprite.name);
            }
        }
        else
        {
            Debug.LogError("No sprites found in the specified folder path: " + _MageSpritePath);
        }
    }

    public Sprite[] GetMageItems()
    {
        return _MageloadedSprites;
    }

}
