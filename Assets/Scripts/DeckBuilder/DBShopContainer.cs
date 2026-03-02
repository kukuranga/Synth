using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class DBShopContainer : MonoBehaviour , IPointerClickHandler
{
    //Container for each unit type
    //public DBUnit _unit;
    public DBContainer _DBCont;
    //make a variable to hold the number of units you can buy
    public int _CurrentAvailableBuys;
    public TextMeshPro _BuysTMP;
    public TextMeshPro _CostTmp;

    private void Update()
    {
        _BuysTMP.text = _CurrentAvailableBuys.ToString();
    }

    public void Init()
    {
        //set the unit here
        _CurrentAvailableBuys = _DBCont._unit._NumberOfBuys;
        _CostTmp.text = _DBCont._unit._ShopCost.ToString();
    }

    public void SetUnit(DBUnit _U)
    {
        _DBCont._Coin.SetActive(true);
        _DBCont.SetUnit(_U);
        _DBCont._Unlocked = true;
        Init();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_CurrentAvailableBuys > 0 && Game2Manager.Instance._YellowResource >= _DBCont._unit._ShopCost)
        {
            Game2Manager.Instance._YellowResource -= _DBCont._unit._ShopCost;
            Game2Manager.Instance.AddUnitToDeck(_DBCont._unit);
            _CurrentAvailableBuys--;
            Debug.Log("Unit Added");
        }
        else
            Debug.Log("Cant Buy Anymore");
    }

}
