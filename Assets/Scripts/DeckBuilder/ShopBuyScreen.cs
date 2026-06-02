using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopBuyScreen : MonoBehaviour
{

    public DBUnit _Unit;
    public TextMeshProUGUI _Name;
    public TextMeshProUGUI _Desc;
    public TextMeshProUGUI _Yellow;
    public TextMeshProUGUI _Green;
    public SpriteRenderer _SpriteRend;
    public SpriteRenderer _AbilityRender; //TODO
    public TextMeshProUGUI _AbilityDesc; //TODO
    public DBShopContainer _ShopCont;


    public void SetUnit(DBUnit _U, DBShopContainer _SC)
    {
        _Unit = _U;
        _Name.text = _Unit._Name;
        _Desc.text = _Unit._Description;
        _SpriteRend.sprite = _Unit._sprite;
        _ShopCont = _SC;

        if (_Unit._YellowResourceGain > 0)
            _Yellow.text = _Unit._YellowResourceGain.ToString();
        else
            _Yellow.text = "";

        if (_Unit._GreenResourceGain > 0)
            _Green.text = _Unit._GreenResourceGain.ToString();
        else
            _Green.text = "";

    }

    public void Onclick()
    {
        //on click will buy
        //Debug.Log("Buy Unit");

        if (_ShopCont._CurrentAvailableBuys > 0 && Game2Manager.Instance._YellowResource >= _Unit._ShopCost)
        {
            Game2Manager.Instance._YellowResource -= _Unit._ShopCost;
            Game2Manager.Instance.AddUnitToDeck(_Unit);
            _ShopCont._CurrentAvailableBuys--;
            Debug.Log("unit added");
        }
        else
            Debug.Log("cant buy anymore");


    }
}
