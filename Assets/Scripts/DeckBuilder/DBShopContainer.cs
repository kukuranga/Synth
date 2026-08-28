using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public enum ShopType
{
    Yellow,
    Green,
    Star,
    Danger,
    Unique
}

public class DBShopContainer : MonoBehaviour , IPointerClickHandler
{
    //Container for each unit type
    public DBUnit _unit;
    //public DBContainer _DBCont;
    //make a variable to hold the number of units you can buy
    public int _CurrentAvailableBuys;
    public GameObject _Container;
    public TextMeshPro _UnlockCostTMP;
    public TextMeshPro _YellowTMP;
    public TextMeshPro _GreenTMP;
    public SpriteRenderer _AbilityRend;
    public GameObject _DangerGO;
    public GameObject _StarGO;
    public ShopType _shopType;
    public bool _Unlocked;
    public bool _Visible;

    //todo: add elements for visual components here
    //: also make the grab not bring repeats
    public SpriteRenderer _Sprite;
    public SpriteRenderer _LockedSprite;

    public Sprite _YellowLockSprite;
    public Sprite _GreenLockSprite;
    public Sprite _DangerLockSprite;
    public Sprite _StarLockSprite;
    public Sprite _UniqueLockSprite;

    private void Update()
    {
        if(_Unlocked)
        {
            _Container.SetActive(true);
            _LockedSprite.gameObject.SetActive(false);
        }
        else
        {
            _Container.SetActive(false);
            _LockedSprite.gameObject.SetActive(true);
            _UnlockCostTMP.text = Game2Manager.Instance._shop._UnlockCost.ToString();
            switch(_shopType)
            {
                case ShopType.Yellow:
                    _LockedSprite.sprite = _YellowLockSprite;
                    break;
                case ShopType.Green:
                    _LockedSprite.sprite = _GreenLockSprite;
                    break;
                case ShopType.Danger:
                    _LockedSprite.sprite = _DangerLockSprite;
                    break;
                case ShopType.Star:
                    _LockedSprite.sprite = _StarLockSprite;
                    break;
                case ShopType.Unique:
                    _LockedSprite.sprite = _UniqueLockSprite;
                    break;
            }
        }
    }

    public void Init()
    {
        //set the unit here
        _CurrentAvailableBuys = _unit._NumberOfBuys;

        _Sprite.sprite = _unit._sprite;
        //_BuysTMP.text = _unit._NumberOfBuys.ToString();
        if (_unit._YellowResourceGain > 0) _YellowTMP.text = _unit._YellowResourceGain.ToString(); else _YellowTMP.text = "0";
        if (_unit._GreenResourceGain >0) _GreenTMP.text = _unit._GreenResourceGain.ToString(); else _GreenTMP.text = "0";
        
        if (_unit._Ability != null) { _AbilityRend.gameObject.SetActive(true); _AbilityRend.sprite = _unit._Ability._sprite; } else _AbilityRend.gameObject.SetActive(false);
        
        if (_unit._Danger) _DangerGO.SetActive(true); else _DangerGO.SetActive(false);
        if (_unit._Star) _StarGO.SetActive(true); else _StarGO.SetActive(false);

    }

    public void SetUnit(DBUnit _U)
    {
        //_DBCont._Coin.SetActive(true);
        //_DBCont._Unlocked = true;
        ////_DBCont.SetUnit(_U);
        //_DBCont.SetUnitNoAnimation(_U);
        _unit = _U;
        Init();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_Unlocked)
        {
            _Container.SetActive(false);
            Game2Manager.Instance.OpenShopBuyScreen(_unit, this);
        }
        else
        {
            //code for unlocking
            if(Game2Manager.Instance._GreenResource >= Game2Manager.Instance._shop._UnlockCost)
            {
                Game2Manager.Instance.SubtractGreenResource(Game2Manager.Instance._shop._UnlockCost);
                Game2Manager.Instance._shop._UnlockCost += 2;
                _Unlocked = true;
            }
            else
            {
                Debug.Log("Not enough green");
            }
        }

    }

    public void ActivateVisuals()
    {
        _Container.SetActive(true);
    }

    private void OnEnable()
    {
        _Container.SetActive(true);
    }
}
