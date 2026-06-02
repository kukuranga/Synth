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
    public TextMeshPro _BuysTMP;
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
        _BuysTMP.text = _unit._NumberOfBuys.ToString();
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

        Game2Manager.Instance.OpenShopBuyScreen(_unit, this);
    }

}
