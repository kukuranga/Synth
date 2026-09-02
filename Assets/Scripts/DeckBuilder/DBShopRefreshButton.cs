using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DBShopRefreshButton : MonoBehaviour
{
    public TextMeshProUGUI _CostText;
    public int _Cost;

    private void Start()
    {
        _Cost = 2;
    }

    private void Update()
    {
        _CostText.text = _Cost.ToString();
    }

    public void OnClick()
    {
        if (Game2Manager.Instance._GreenResource >= _Cost)
        {
            Game2Manager.Instance.SubtractGreenResource(_Cost);
            Game2Manager.Instance._shop.RefreshShop();
            _Cost += 2;
        }

    }
}
