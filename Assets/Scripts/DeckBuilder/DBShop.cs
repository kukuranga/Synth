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

    // Tracks which units have already been shown from each pool so refreshes favor
    // units the player hasn't seen yet. Cleared out on Awake() for a fresh run.
    private HashSet<DBUnit> _yellowHistory = new HashSet<DBUnit>();
    private HashSet<DBUnit> _greenHistory = new HashSet<DBUnit>();
    private HashSet<DBUnit> _dangerHistory = new HashSet<DBUnit>();
    private HashSet<DBUnit> _starHistory = new HashSet<DBUnit>();
    private HashSet<DBUnit> _uniqueHistory = new HashSet<DBUnit>();

    private void Awake()
    {
        Game2Manager.Instance.SetShop(this);//Sets the shop object in the game manager
        // Get all DBShopContainer components inside _ContainerParent
        _ShopPool._UnitList.Clear();
        _ShopContainers.Clear();
        _ShopContainers = new List<DBShopContainer>(_ContainerParent.GetComponentsInChildren<DBShopContainer>());

        _yellowHistory.Clear();
        _greenHistory.Clear();
        _dangerHistory.Clear();
        _starHistory.Clear();
        _uniqueHistory.Clear();
    }

    public void Init() //Called once during pregame
    {
        _UnlockCost = 2;
        AddUnitListToPool(_BasePack);
    }

    public void SetUpShop()
    {
        ShopOrientation _SelectedShop = _ShopOrientations[0];

        List<DBUnit> _yelowPull = PullFromList(_YellowContainers, _SelectedShop._yellowCount, _yellowHistory);
        List<DBUnit> _greenPull = PullFromList(_GreenContainers, _SelectedShop._greenCount, _greenHistory);
        List<DBUnit> _dangerPull = PullFromList(_DangerContainers, _SelectedShop._dangerCount, _dangerHistory);
        List<DBUnit> _starPull = PullFromList(_StarContainers, _SelectedShop._starCount, _starHistory);
        List<DBUnit> _uniquePull = PullFromList(_UniqueContainers, _SelectedShop._uniqueCount, _uniqueHistory);

        int i = 0;
        foreach (DBShopContainer _u in _ShopContainers)
        {
            _u._shopType = _SelectedShop._shopTypes[i];
            _u._Unlocked = _SelectedShop._isUnlocked[i];
            _u._Visible = _SelectedShop._isVisible[i];
            i++;
        }

        AssignUnitsToContainers(_ShopContainers, _yelowPull, _greenPull, _dangerPull, _starPull, _uniquePull);
    }

    public void RefreshShop()
    {
        _ShopPool._UnitList.Clear();
        _ShopContainers.Clear();
        _ShopContainers = new List<DBShopContainer>(_ContainerParent.GetComponentsInChildren<DBShopContainer>());

        List<DBShopContainer> _unlockedContainers = _ShopContainers.FindAll(c => c._Unlocked);

        int _yellowNeeded = _unlockedContainers.FindAll(c => c._shopType == ShopType.Yellow).Count;
        int _greenNeeded = _unlockedContainers.FindAll(c => c._shopType == ShopType.Green).Count;
        int _dangerNeeded = _unlockedContainers.FindAll(c => c._shopType == ShopType.Danger).Count;
        int _starNeeded = _unlockedContainers.FindAll(c => c._shopType == ShopType.Star).Count;
        int _uniqueNeeded = _unlockedContainers.FindAll(c => c._shopType == ShopType.Unique).Count;

        List<DBUnit> _yellowPull = PullFromList(_YellowContainers, _yellowNeeded, _yellowHistory);
        List<DBUnit> _greenPull = PullFromList(_GreenContainers, _greenNeeded, _greenHistory);
        List<DBUnit> _dangerPull = PullFromList(_DangerContainers, _dangerNeeded, _dangerHistory);
        List<DBUnit> _starPull = PullFromList(_StarContainers, _starNeeded, _starHistory);
        List<DBUnit> _uniquePull = PullFromList(_UniqueContainers, _uniqueNeeded, _uniqueHistory);

        AssignUnitsToContainers(_unlockedContainers, _yellowPull, _greenPull, _dangerPull, _starPull, _uniquePull);
    }

    private void AssignUnitsToContainers(List<DBShopContainer> _containers, List<DBUnit> _yellowPull, List<DBUnit> _greenPull, List<DBUnit> _dangerPull, List<DBUnit> _starPull, List<DBUnit> _uniquePull)
    {
        foreach (DBShopContainer _u in _containers)
        {
            _u.ActivateVisuals();

            switch (_u._shopType)
            {
                case ShopType.Yellow:
                    _u.SetUnit(_yellowPull[0]);
                    _yellowPull.RemoveAt(0);
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

    public List<DBUnit> PullFromList(DBUnitList _List, int _num, HashSet<DBUnit> _pulledHistory = null)
    {
        List<DBUnit> _rand = new List<DBUnit>();

        if (_List == null || _List._UnitList == null || _List._UnitList.Count == 0 || _num <= 0)
        {
            if (_num > 0)
                Debug.LogWarning("PullFromList: list is empty or null, returning empty result.");
            return _rand;
        }

        List<DBUnit> _fullPool = new List<DBUnit>(_List._UnitList);

        // Units we haven't shown from this pool before - preferred picks.
        List<DBUnit> _freshPool = _pulledHistory == null
            ? new List<DBUnit>(_fullPool)
            : _fullPool.FindAll(u => !_pulledHistory.Contains(u));

        // Fallback once fresh options run out, so we don't hand back a repeat
        // unless it's genuinely the only option left (e.g. a pool with 1 unit).
        List<DBUnit> _repeatPool = new List<DBUnit>(_fullPool);

        for (int i = 0; i < _num; i++)
        {
            DBUnit _pick;

            if (_freshPool.Count > 0)
            {
                int index = Random.Range(0, _freshPool.Count);
                _pick = _freshPool[index];
                _freshPool.RemoveAt(index);
            }
            else
            {
                // Every unit in this pool has already been pulled at some point - forced to repeat.
                if (_repeatPool.Count == 0)
                {
                    _repeatPool = new List<DBUnit>(_fullPool); // ran out again this batch, start over
                }

                int index = Random.Range(0, _repeatPool.Count);
                _pick = _repeatPool[index];
            }

            _repeatPool.Remove(_pick);
            _pulledHistory?.Add(_pick);
            _rand.Add(_pick);
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
}