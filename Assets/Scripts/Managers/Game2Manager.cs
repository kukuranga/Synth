using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    pregame,
    RoundStart,
    GamePlay,
    CheckConditions,
    Shop,
    Gamewon,
    GameLost
}

public class Game2Manager : Singleton<Game2Manager>
{
    public bool DebuggerMode;
    public int _YellowResource;
    public int _GreenResource;
    public int _TotalTurnCount;
    public int _CurrentTurnCount;
    public int _ActiveContainers;
    public int _DangerLevelAllowed;
    public int _StarCountToWin;
    public GameObject _Containers;
    public GameObject _ShopScene;
    public GameObject _GameUI;
    public GameObject _GameWon;
    public GameObject _GameLost;
    public GameObject _MessageScreen;
    public DBMessageScreen _DBMessageScreen;
    public GameState _gameState;
    public Deck _deck;
    public DBContainerManager _ContainerManager;
    public DBShop _shop;

    private void Update()
    {
        if(DebuggerMode)
        {
            Debug.Log("Game state = " + _gameState);
        }
    }

    public void UpdateGameState(GameState newState)
    {
        _gameState = newState;

        switch(newState)
        {
            case GameState.pregame:
                DisableUI();
                _GameUI.SetActive(true);
                _Containers.SetActive(true);
                _deck.SetActiveDeck();
                _ContainerManager.FirstSetUp();
                _deck.StartRound();
                _CurrentTurnCount = _TotalTurnCount;
                _shop.Init();
                UpdateGameState(GameState.GamePlay);
                break;
            case GameState.RoundStart:
                DisableUI();
                _ContainerManager.FirstSetUp();
                _deck.StartRound();
                _GameUI.SetActive(true);
                _Containers.SetActive(true); 
                UpdateGameState(GameState.GamePlay);
                break;
            case GameState.GamePlay:
                //allow the player to click on the screen to get a unit from the deck
                //when its clicked check the number of units, the total allowed and the number of danger points
                //check the number of star points and if you win

                //Set the buttons to active, and set the scene to the correcct state
                //to be called from the shop or the check conditions method

                //if the containers are full or the player clickes the end button the state will change to the checkconditions state

                break;
            case GameState.CheckConditions:
                //this is called after the party is full or you hit too many danger points
                //it slowly goes through each unit and adds to their values to the resources you have made
                //check any post party special abilities

                StartCoroutine(CheckConditions());

                break;
            case GameState.Shop:
                //after the conditions are checked the shop will be opened
                //players can buy units to add to their deck or increase the size of the galaxy
                //reset a suspended unit if applicible
                DisableUI();
                _ShopScene.SetActive(true);
                _ContainerManager.ClearActiveContainers();//clears the current active containers
                break;
            case GameState.Gamewon:
                DisableUI();
                _GameWon.SetActive(true);
                //start the game won value and continue with the game
                break;
            case GameState.GameLost:
                DisableUI();
                _GameLost.SetActive(true);
                //if the number of turns hits 0 and you dont have the star count the game is lost
                break;
        }
    }

    public void AddUnitToDeck(DBUnit _Unit)
    {
        _deck.AddUnit(_Unit);
    }

    public void SetContainerManager(DBContainerManager _DBContMan)
    {
        _ContainerManager = _DBContMan;
        _ActiveContainers = _DBContMan._StartingContainers;
    }

    //Call this after the unit has been placed into the scene from the deck
    public bool CheckDanger()
    {
        int i = _ContainerManager.GetNumberOfDangerActive();
        
        //TODO: Add logic here to reduce the danger count with abilities

        if (i < _DangerLevelAllowed)
            return false;

        return true;
    }

    public void IncreaseSlots()
    {
        _ContainerManager.IncreaseStartingContainers();
    }

    #region OnButtonClick

    //Called on click of the grab button
    public void SetUnit()
    {
        if (_ContainerManager._NumberOfUnitsPulled < _ContainerManager._StartingContainers)
        {
            DBUnit _u = _deck.PullUnit();
            _ContainerManager.AddUnitToLastContainer(_u);

            //check the number of units and check if the danger level is too high after that
            if (CheckDanger())
            {
                DisableUI();
                _DBMessageScreen.UpdateMessage("Danger too high");
                _MessageScreen.SetActive(true);
                //UpdateGameState(GameState.Shop);
            }
            else if (_ContainerManager._NumberOfUnitsPulled == _ActiveContainers)
                UpdateGameState(GameState.CheckConditions);
        }
        else
            UpdateGameState(GameState.CheckConditions);
    }

    //Called onclick of the end button
    public void EndSelection()
    {
        if(_gameState != GameState.CheckConditions)
            UpdateGameState(GameState.CheckConditions);
    }

    #endregion

    public void SetShop(DBShop _DBShop)
    {
        _shop = _DBShop;
    }

    public void SetDeck(Deck _d)
    {
        _deck = _d;
    }

    public void ClearDeck()
    {
        _deck = null;
    }

    public void AddYellowResource(int i)
    {
        _YellowResource += i;
    }

    private void DisableUI()
    {
        _GameUI.SetActive(false);
        _ShopScene.SetActive(false);
        _GameWon.SetActive(false);
        _GameLost.SetActive(false);
        _Containers.SetActive(false);
        _MessageScreen.SetActive(false);
    }

    private IEnumerator CheckConditions()
    {
        int _NumberOfStars = 0;

        foreach(DBContainer _cont in _ContainerManager._ActiveContainers)
        {
            if(_cont._unit != null)
            {
                _YellowResource += _cont._unit._YellowResourceGain;
                _GreenResource += _cont._unit._GreenResourceGain;                

                if (_cont._unit._Star)
                    _NumberOfStars++;

                yield return new WaitForSeconds(1);
            }
        }

        foreach(DBContainer _cont in _ContainerManager._ActiveContainers)
        {
            if(_cont._unit != null)
            {
                if(_cont._unit._Ability != null && !_cont._unit._Ability.Activated)
                {
                    _cont._unit._Ability.UseAbility();
                }
            }
        }

        _CurrentTurnCount--;

        if (_GreenResource <= -1)
        {
            _GreenResource = 0;
            _YellowResource -= 5;
            Debug.Log("5 yellow taken away");
        }

        if (_CurrentTurnCount <= 0)
            UpdateGameState(GameState.GameLost);
        else if (_NumberOfStars >= _StarCountToWin)
            UpdateGameState(GameState.Gamewon);
        else
            UpdateGameState(GameState.Shop);

        yield return null;
    }
}
