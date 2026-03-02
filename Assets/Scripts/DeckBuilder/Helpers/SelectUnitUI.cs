using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUnitUI : MonoBehaviour
{
    //Controls the selection UI when its time to set it up

    private List<DBUnit> _Units;
    private bool _IsActive;
    public int _CurrentIndex;//The currently selected Unit
    private DBUnit _SelectedUnit;
    public DBContainer _Container;


    private void Start()
    {
        _CurrentIndex = 0;
    }

    private void Update()
    {
        if (_IsActive)
        {
            if (_CurrentIndex > _Units.Count)
                _CurrentIndex = _Units.Count;

            if (_CurrentIndex < 0)
                _CurrentIndex = 0;

            if (_IsActive && _Units != null)
                _SelectedUnit = _Units[_CurrentIndex];

            SetUnitsVisuals();
        }

    }

    private void SetUnitsVisuals()
    {
        _Container._Coin.SetActive(true);
        _Container._unit = _SelectedUnit;
        _Container.UpdateSpriteVisuals();
        
    }

    public void SelectUnit()
    {
        //OnClick send the selected unit to the game2Manager to grab the specific unit and use it to add it
        //after set the unit to disable the UI
        Game2Manager.Instance.SetSpecificUnit(_Units[_CurrentIndex]);
        CloseSelectionUI();
        Game2Manager.Instance._SelectUnitUI.SetActive(false);
        Game2Manager.Instance.UpdateGameState(GameState.GamePlay);
    }

    public void OpenSelectionUI(List<DBUnit> _u)
    {
        _Units = _u;
        _IsActive = true;
    }

    public void CloseSelectionUI()
    {
        _IsActive = false;
        _SelectedUnit = null;
        _CurrentIndex = 0;
        _Units.Clear();
    }

    //Onclick move right
    public void MoveSelectionUp()
    {
        if(_CurrentIndex < _Units.Count)
            _CurrentIndex++;
        SetUnitsVisuals();        
    }

    //OnClick move Left
    public void MoveSelectionDown()
    {
        if (_CurrentIndex > 0)
            _CurrentIndex--;
        SetUnitsVisuals();
    }

}
