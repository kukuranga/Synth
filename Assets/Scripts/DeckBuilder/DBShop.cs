using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBShop : MonoBehaviour
{
    public DBUnitList _ShopUnits;
    //display each of them in a list of units
    //public GameObject _DBShopContainerPrefab;
    public GameObject _ContainerParent; //Not Used
    public List<DBShopContainer> _ShopContainers;

    private void Awake()          
    {
        Game2Manager.Instance.SetShop(this);
        // Get all DBShopContainer components inside _ContainerParent
        _ShopContainers.Clear();
        _ShopContainers = new List<DBShopContainer>(_ContainerParent.GetComponentsInChildren<DBShopContainer>());

    }

    public void Init()
    {
        for (int i = 0; i < _ShopContainers.Count; i++)
        {
            if (i < _ShopUnits._UnitList.Count && _ShopUnits._UnitList[i] != null)
            {
                _ShopContainers[i].SetUnit(_ShopUnits._UnitList[i]);
            }
            else
            {
                _ShopContainers[i].gameObject.SetActive(false);
            }
        }
    }

}
