using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DBShopExpandButton : MonoBehaviour
{
    public TextMeshProUGUI _AmountTxt;
    public int _Amount;

    private void Start()
    {
        _Amount = 2;
    }

    private void Update()
    {
        _AmountTxt.text = _Amount.ToString();
    }

    public void OnClick()
    {
        if(Game2Manager.Instance._GreenResource >= _Amount)
        {
            Game2Manager.Instance._GreenResource -= _Amount;
            _Amount++;
            Game2Manager.Instance.IncreaseSlots();
        }
    }
}
