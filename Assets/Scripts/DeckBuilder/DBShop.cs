using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBShop : MonoBehaviour
{
    public DBUnitList _BasePack;

    public DBUnitList _ShopPool; //Contains all of the units to pull from for the actual shop

    //display each of them in a list of units
    //public GameObject _DBShopContainerPrefab;
    public GameObject _ContainerParent;
    public List<DBShopContainer> _ShopContainers;

    private void Awake()          
    {
        Game2Manager.Instance.SetShop(this);//Sets the shop object in the game manager
        // Get all DBShopContainer components inside _ContainerParent
        _ShopPool._UnitList.Clear();
        _ShopContainers.Clear();
        _ShopContainers = new List<DBShopContainer>(_ContainerParent.GetComponentsInChildren<DBShopContainer>());

    }

    public void Init() //Called once during pregame
    {
        
        AddUnitListToPool(_BasePack);
    }

    public void SetUpShop(int NumberOfOptions)
    {
        foreach(DBShopContainer _u in _ShopContainers)
        {
            int i = 0;
            if (i < NumberOfOptions)
            {
                _u.ActivateVisuals();
                _u.SetUnit(PullForShop());
                //ToDo: add conditions to check for rarity etc
            }
            else
                _u.gameObject.SetActive(false);
        }
    }

    public DBUnit PullForShop()
    {

        DBUnit _U = _ShopPool._UnitList[Random.Range(0, _ShopPool._UnitList.Count)];

        foreach(DBShopContainer _cont in _ShopContainers)
        {
            if (_cont._unit == _U)
                _U = PullForShop();
        }

        return _U;
    }

    public void AddUnitListToPool(DBUnitList _list)
    {
        foreach (DBUnit _u in _list._UnitList)
        {
            _ShopPool._UnitList.Add(_u);
        }
    }
}
