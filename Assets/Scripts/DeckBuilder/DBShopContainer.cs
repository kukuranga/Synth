using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class DBShopContainer : MonoBehaviour , IPointerClickHandler
{
    //Container for each unit type
    public DBUnit _unit;
    //public DBContainer _DBCont;
    //make a variable to hold the number of units you can buy
    public int _CurrentAvailableBuys;
    //public TextMeshPro _BuysTMP;
    public GameObject _Container;
    public TextMeshPro _YellowTMP;
    public TextMeshPro _GreenTMP;
    public SpriteRenderer _AbilityRend;
    public GameObject _DangerGO;
    public GameObject _StarGO;
    //public TextMeshPro _CostTmp;
    //public TextMeshPro _DescTMP;

    //public TextMeshPro _AbilityDescTMP;

    //todo: add elements for visual components here
    //: also make the grab not bring repeats
    public SpriteRenderer _Sprite;

    private void Update()
    {
        //_BuysTMP.text = _CurrentAvailableBuys.ToString();
    }

    public void Init()
    {
        //set the unit here
        _CurrentAvailableBuys = _unit._NumberOfBuys;
        //_CostTmp.text = _DBCont._unit._ShopCost.ToString();
        //_DescTMP.text = _DBCont._unit._Description;

        //if (_DBCont._unit._Ability != null)
        //    _AbilityDescTMP.text = _DBCont._unit._Ability.Description;
        //else
        //    _AbilityDescTMP.text = "";

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
        //if (_CurrentAvailableBuys > 0 && Game2Manager.Instance._YellowResource >= _DBCont._unit._ShopCost)
        //{
        //    Game2Manager.Instance._YellowResource -= _DBCont._unit._ShopCost;
        //    Game2Manager.Instance.AddUnitToDeck(_DBCont._unit);
        //    _CurrentAvailableBuys--;
        //    Debug.Log("Unit Added");
        //}
        //else
        //    Debug.Log("Cant Buy Anymore");
        _Container.SetActive(false);
        Game2Manager.Instance.OpenShopBuyScreen(_unit, this);
    }

    public void ActivateVisuals()
    {
        _Container.SetActive(true);
    }

}
