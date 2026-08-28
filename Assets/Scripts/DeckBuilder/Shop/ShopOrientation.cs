using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopOrientation", menuName = "DeckBuilder/ShopOrientation")]

public class ShopOrientation : ScriptableObject
{
    private const int SlotCount = 9;

    public ShopType[] _shopTypes = new ShopType[SlotCount];
    public bool[] _isVisible = new bool[SlotCount]; //ToDo: make different patterns for harder game modes or other needed variations on the game
    public bool[] _isUnlocked = new bool[SlotCount];
    public int _yellowCount;
    public int _greenCount;
    public int _dangerCount;
    public int _starCount;
    public int _uniqueCount;

    private void OnValidate()
    {
        System.Array.Resize(ref _shopTypes, SlotCount);
        System.Array.Resize(ref _isVisible, SlotCount);
        System.Array.Resize(ref _isUnlocked, SlotCount);

        _yellowCount = 0;
        _greenCount = 0;
        _dangerCount = 0;
        _starCount = 0;
        _uniqueCount = 0;

        foreach (ShopType type in _shopTypes)
        {
            switch (type)
            {
                case ShopType.Yellow:
                    _yellowCount++;
                    break;
                case ShopType.Green:
                    _greenCount++;
                    break;
                case ShopType.Star:
                    _starCount++;
                    break;
                case ShopType.Danger:
                    _dangerCount++;
                    break;
                case ShopType.Unique:
                    _uniqueCount++;
                    break;
            }
        }
    }
}
