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
        //for (int i = 0; i < _ShopContainers.Count; i++)
        //{
        //    if (i < _ShopUnits._UnitList.Count && _ShopUnits._UnitList[i] != null)
        //    {
        //        _ShopContainers[i].SetUnit(_ShopUnits._UnitList[i]);
        //    }
        //    else
        //    {
        //        _ShopContainers[i].gameObject.SetActive(false);
        //    }
        //}
        AddUnitListToPool(_BasePack);
    }

    public void SetUpShop(int NumberOfOptions)
    {
        foreach(DBShopContainer _u in _ShopContainers)
        {
            int i = 0;
            if (i < NumberOfOptions)
            {
                _u.SetUnit(PullForShop());
                //ToDo: add conditions to check for rarity etc
            }
            else
                _u.gameObject.SetActive(false);
        }
    }

    public DBUnit PullForShop()
    {
        return _ShopPool._UnitList[Random.Range(0, _ShopPool._UnitList.Count)];
    }

    public void AddUnitListToPool(DBUnitList _list)
    {
        foreach (DBUnit _u in _list._UnitList)
        {
            _ShopPool._UnitList.Add(_u);
        }
    }
}
