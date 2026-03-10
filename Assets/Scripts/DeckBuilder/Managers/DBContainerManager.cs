using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBContainerManager : Singleton<DBContainerManager>
{

    //In charge of the containers and all its functions
    public int _StartingContainers;
    public List<DBContainer> _AllContainers;
    public List<DBContainer> _ActiveContainers;
    public int _NumberOfUnitsPulled;
    public GameObject _SpawnPoint;

    private void Start()
    {
        Game2Manager.Instance.SetContainerManager(this);    
    }

    public void FirstSetUp()
    {
        ClearContainers();
        ClearNumberOfUnitsPulled();
        SetActiveContainerNumber(_StartingContainers);
    }

    public void ClearContainers()
    {
        foreach (DBContainer _cont in _AllContainers)
        {
            _cont.ClearContainer();
        }
    }

    public int NumberOfEmptyContainers()
    {
        int i = 0;
        foreach(DBContainer _con in _ActiveContainers)
        {
            if (_con._unit == null)
                i++;
        }
        return i;
    }

    public void IncreaseStartingContainers()
    {
        _StartingContainers++;
        //Expand circle
        if (_StartingContainers > 6)
            CircleManager.Instance.ExpandCircleOne();

        Game2Manager.Instance._ActiveContainers = _StartingContainers;

    }

    public void SetActiveContainerNumber(int _activeNumber)
    {
        for (int i = 0; i < _activeNumber; i++)
        {
            _AllContainers[i]._Unlocked = true;
            _ActiveContainers.Add(_AllContainers[i]);
        }
    }

    public void ClearActiveContainers()
    {
        _ActiveContainers.Clear();
    }
    public void ClearNumberOfUnitsPulled()
    {
        _NumberOfUnitsPulled = 0;
    }

    public int GetNumberOfDangerActive()
    {
        int i = 0;

        foreach(DBContainer _cont in _ActiveContainers)
        {
            if(_cont._unit != null)
            {
                if (_cont._unit._Danger)
                    i++;

                if (_cont._unit._FlagDangerReduction)
                    i--;
            }
        }
        return i;
    }

    public int GetNumberOfStarsActive()
    {
        int i = 0;

        foreach (DBContainer _cont in _ActiveContainers)
        {
            if (_cont._unit != null)
            {
                if (_cont._unit._Star)
                    i++;
            }
        }
        return i;
    }

    public void AddUnitToLastContainer(DBUnit _u)
    {
        foreach (DBContainer _cont in _ActiveContainers)
        {
            if (_cont._unit == null)
            {
                //set the unit off screen and add the animation here ------------------------------------------------------------------------
                _NumberOfUnitsPulled++;
                _cont.SetUnit(_u);
                return;
            }
        }
    }

    public int CheckIfActivatedabilities()
    {
        int i = 0;

        foreach(DBContainer _cont in _AllContainers)
        {
            if(_cont._unit != null)
                if (_cont._Activated && _cont._unit._Ability != null)
                    if(_cont._unit._Ability.Activated)
                        i++;
        }

        return i;
    }
}
