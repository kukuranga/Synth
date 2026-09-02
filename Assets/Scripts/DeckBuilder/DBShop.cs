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
    public List<DBShopContainer> _ShopContainers;//Might be useless
    public DBUnitList _YellowContainers;
    public DBUnitList _GreenContainers;
    public DBUnitList _DangerContainers;
    public DBUnitList _StarContainers;
    public DBUnitList _UniqueContainers;
    public List<ShopOrientation> _ShopOrientations;
    public int _UnlockCost;

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
        _UnlockCost = 2;
        AddUnitListToPool(_BasePack);
    }

    public void SetUpShop()
    {
        ShopOrientation _SelectedShop = _ShopOrientations[0];

        List<DBUnit> _yelowPull = PullFromList(_YellowContainers, _SelectedShop._yellowCount);
        List<DBUnit> _greenPull = PullFromList(_GreenContainers , _SelectedShop._greenCount);
        List<DBUnit> _dangerPull = PullFromList(_DangerContainers , _SelectedShop._dangerCount);
        List<DBUnit> _starPull = PullFromList(_StarContainers , _SelectedShop._starCount);
        List<DBUnit> _uniquePull = PullFromList(_UniqueContainers , _SelectedShop._uniqueCount);

        int i = 0;
        foreach(DBShopContainer _u in _ShopContainers)
        {
            _u._shopType = _SelectedShop._shopTypes[i];
            _u._Unlocked = _SelectedShop._isUnlocked[i];
            _u._Visible = _SelectedShop._isVisible[i];
            i++;

            _u.ActivateVisuals();

            switch (_u._shopType)
            {
                case ShopType.Yellow:
                    _u.SetUnit(_yelowPull[0]);
                    _yelowPull.RemoveAt(0);
                    break;
                case ShopType.Green:
                    _u.SetUnit(_greenPull[0]);
                    _greenPull.RemoveAt(0);
                    break;
                case ShopType.Star:
                    _u.SetUnit(_starPull[0]);
                    _starPull.RemoveAt(0);
                    break;
                case ShopType.Danger:
                    _u.SetUnit(_dangerPull[0]);
                    _dangerPull.RemoveAt(0);
                    break;
                case ShopType.Unique:
                    _u.SetUnit(_uniquePull[0]);
                    _uniquePull.RemoveAt(0);
                    break;
            }

        }
    }

    public List<DBUnit> PullFromList(DBUnitList _List, int _num)
    {
        List<DBUnit> _rand = new List<DBUnit>();

        if (_List == null || _List._UnitList == null || _List._UnitList.Count < _num)
        {
            Debug.LogWarning("PullFromList: list is empty or null, returning empty result.");
            return _rand;
        }

        List<DBUnit> _pool = new List<DBUnit>(_List._UnitList); // copy so we don't touch the original

        _num = Mathf.Min(_num, _pool.Count);

        for (int i = 0; i < _num; i++)
        {
            int index = Random.Range(0, _pool.Count);
            _rand.Add(_pool[index]);
            _pool.RemoveAt(index);
        }

        return _rand;
    }

    public void AddUnitListToPool(DBUnitList _list)
    {
        foreach (DBUnit _u in _list._UnitList)
        {
            _ShopPool._UnitList.Add(_u);
        }
    }

    public void RefreshShop()
    {
        _ShopPool._UnitList.Clear();
        _ShopContainers.Clear();
        _ShopContainers = new List<DBShopContainer>(_ContainerParent.GetComponentsInChildren<DBShopContainer>());

        SetUpShop();
    }
}
