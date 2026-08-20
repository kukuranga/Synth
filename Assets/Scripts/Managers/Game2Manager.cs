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
    GameLost,
    SelectUnit,
    Animation,
    FetchUnit
}

public enum DBAbilityTypes
{
    None,
    Kick

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
    public int _NumberOfShopContainersToOpen;
    public GameObject _Containers;
    public GameObject _ShopScene;
    public GameObject _GameUI;
    public GameObject _GameWon;
    public GameObject _GameLost;
    public GameObject _CheckBanner;
    public GameObject _MessageScreen;
    public GameObject _SelectUnitUI;
    public GameObject _NextUnitUI;
    public SelectUnitUI _SelectUnitUiComponent;
    public GameObject _ShopBuyUI;
    public DBMessageScreen _DBMessageScreen;
    public GameState _gameState;
    public Deck _deck;
    public DBContainerManager _ContainerManager;
    public DBShop _shop;
    public Camera _CameraMain;

    public DBAbilityTypes _CurrentAbility;
    public DBUnit _TempUnit;

    public List<DBRotateAroundCentre> _RotationComponents;

    private int _RoundStartYellow;
    private int _RoundStartGreen;
    private bool _DontCheckConditions;
    private Vector3 _CameraOriginalPosition;

    private void Update()
    {
        if(DebuggerMode)
        {
            Debug.Log("Game state = " + _gameState);
        }
    }
    private void Start()
    {
        _CameraOriginalPosition = _CameraMain.transform.position;
        _ = SFXManager.Instance;
    }

    public void UpdateGameState(GameState newState)
    {
        ResetCamera();

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
                _CheckBanner.SetActive(false);
                UpdateGameState(GameState.GamePlay);
                break;
            case GameState.RoundStart:
                DisableUI();
                _NumberOfStacks = 0;
                _StackChecked = false;
                _RoundStartYellow = _YellowResource;
                _RoundStartGreen = _GreenResource;
                _ContainerManager.FirstSetUp();
                _deck.StartRound();
                _GameUI.SetActive(true);
                _Containers.SetActive(true);
                _CheckBanner.SetActive(false);
                VFX2Manager.Instance.StartOrbitals();
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
                if (!CheckIfAnyActivesLeft())
                {
                    VFX2Manager.Instance.StopOrbitals();
                    _CheckBanner.SetActive(true);
                    StartCoroutine(CheckConditions());
                }
                break;
            case GameState.Shop:
                //after the conditions are checked the shop will be opened
                //players can buy units to add to their deck or increase the size of the galaxy
                //reset a suspended unit if applicible
                VFX2Manager.Instance.OpenShopVFX();
                _shop.SetUpShop(_NumberOfShopContainersToOpen);
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
            case GameState.SelectUnit:
                //Used to select a unit in reference to an ability
                break;
            case GameState.Animation:
                //used during animations in the game are happenening
                break;
            case GameState.FetchUnit:
                //open ui here
                _SelectUnitUI.SetActive(true);
                break;
        }
    }

    public bool CheckIfAnyActivesLeft()
    {
        //this should return true if there are any activatd abilities left
        int i = _ContainerManager.CheckIfActivatedabilities();

        if (i >= 1)
        {
            //set the game state back to gampeplay
            DBMessageManager.Instance.UpdateMessage("Unused abilities");
            UpdateGameState(GameState.GamePlay);
            return true;
        }

        //TODO: figure out how to check the gamestate and bring it back to check conditions after an ability is used        
        return false;
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
        StartCoroutine(SetUnitCoroutine());
    }

    IEnumerator SetUnitCoroutine()
    {
        if (_ContainerManager._NumberOfUnitsPulled <= _ContainerManager._StartingContainers)
        {
            _NextUnitUI.SetActive(false);
            DBUnit _u = _deck.PullUnit();
            _ContainerManager.AddUnitToLastContainer(_u);

            _DontCheckConditions = false;
        }
        else
        {
            yield return new WaitForSeconds(2);
            UpdateGameState(GameState.CheckConditions);
        }
        yield return null;
    }

    public void SetSpecificUnit(DBUnit _Unit)
    {
        if (_ContainerManager._NumberOfUnitsPulled <= _ContainerManager._StartingContainers)
        {
            _ContainerManager.AddUnitToLastContainer(_Unit);

            //check the number of units and check if the danger level is too high after that
            if (CheckDanger())
            {
                DangerTooHigh();
            }
            else if (_ContainerManager._NumberOfUnitsPulled == _ActiveContainers)
                UpdateGameState(GameState.CheckConditions);
        }
        else
            UpdateGameState(GameState.CheckConditions);
    }

    public void DangerTooHigh()
    {
        DisableUI();
        _DBMessageScreen.UpdateMessage("Danger too high");
        _MessageScreen.SetActive(true);
        _YellowResource = _RoundStartYellow;
        _GreenResource = _RoundStartGreen;
        _DontCheckConditions = true;
        //UpdateGameState(GameState.Shop);
    }

    //Called onclick of the end button
    public void EndSelection()
    {
        if(_gameState != GameState.CheckConditions && !_DontCheckConditions)
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
    public void AddGreenResource(int i)
    {
        _GreenResource += i;
    }
    public void SubtractYellowResource(int i)
    {
        _YellowResource -= i;
    }
    public void SubtractGreenResource(int i)
    {
        _GreenResource -= i;
    }


    public void DisableUI()
    {
        _GameUI.SetActive(false);
        _ShopScene.SetActive(false);
        _GameWon.SetActive(false);
        _GameLost.SetActive(false);
        _Containers.SetActive(false);
        _MessageScreen.SetActive(false);
        _SelectUnitUI.SetActive(false);
        _NextUnitUI.SetActive(false);
        _ShopBuyUI.SetActive(false);
    }

    public void CloseSelectUnitUI()
    {
        _SelectUnitUI.SetActive(false);
    }

    #region Abilities

    public void KickUnitSetup(DBUnit _unit)
    {
        UpdateGameState(GameState.SelectUnit);
        _TempUnit = _unit;
        _CurrentAbility = DBAbilityTypes.Kick;
        DBMessageManager.Instance.UpdateMessage("Select unit to kick");
    }

    //To be called after a unit is selected to activate an ability, this only works if the temp unnit is set already
    public void UnitClicked(DBContainer _Container)
    {
        switch (_CurrentAbility)
        {

            case DBAbilityTypes.Kick:
                //logic to kick a unit here
                _Container.ClearContainer(); //TODO: change this to just remove the active unit in the container
                _Container._Unlocked = true;
                _Container._Activated = false;
                _ContainerManager._NumberOfUnitsPulled--;
                _TempUnit = null;
                _CurrentAbility = DBAbilityTypes.None;
                DBMessageManager.Instance.ClearMessage();
                UpdateGameState(GameState.GamePlay);
                break;
        }
    }

    //Shows the next unit in the nextUnitUI
    public void SetNextUnitShow()
    {
        DBContainer _cont = _NextUnitUI.GetComponent<NextUnitUi>()._Container;
        _cont.SetUnitNoAnimation(_deck._CurrentDeck[0]);
        _cont._Unlocked = true;
        _cont._Coin.SetActive(true);
        _NextUnitUI.SetActive(true);
    }

    //Brings another unit and checks if there is space for another unit
    public void BringAnotherUnit()
    {
        //this brings another unit with it
        //if there are no more open containers the place overfills and the game ends
        if(_ContainerManager.NumberOfEmptyContainers() < 1)
        {

            _YellowResource = _RoundStartYellow;
            _GreenResource = _RoundStartGreen;
            DangerTooHigh();
        }
        else
            SetUnit();

    }

    bool _StackChecked = false;
    int _NumberOfStacks = 0;
    //adds yellow based on each stack
    public void AddStackingYellow()
    {
        if (!_StackChecked)
        {
            foreach (DBContainer _cont in _ContainerManager._ActiveContainers)
            {
                if (_cont._unit._Ability is  DBStackingAbility)
                {
                    _NumberOfStacks++;
                }
            }
            _StackChecked = true;
        }

        switch(_NumberOfStacks)
        {
            case 0:
                break;
            case 1:
                _YellowResource += 1;
                break;
            case 2:
                _YellowResource += 2;
                break;
            case 3:
                _YellowResource += 9;
                break;
            case 4:
                _YellowResource += 16;
                break;
            default:
                _YellowResource += 16;
                break;
        }
    }

    #endregion

    private IEnumerator CheckConditions()
    {
            int _NumberOfStars = 0;

            //Check for negatives
            while (_YellowResource < 0 && _GreenResource < 0)
            {

                if (_YellowResource < 0)
                {
                    SubtractGreenResource(3);
                    AddYellowResource(1);
                }

                if (_GreenResource < 0)
                {
                    SubtractYellowResource(3);
                    AddGreenResource(1);
                }

                if (_YellowResource < 0 && _GreenResource < 0)
                {
                    _YellowResource = 0;
                    _GreenResource = 0;
                }
            }

            foreach (DBContainer _cont in _ContainerManager._ActiveContainers)
            {
                if (_cont._unit != null)
                {
                    _cont.ConditionsAnim();
                    //_YellowResource += _cont._unit._YellowResourceGain;
                    //_GreenResource += _cont._unit._GreenResourceGain;

                    if (_cont._unit._Star)
                        _NumberOfStars++;

                if (_cont._unit._Ability != null && _cont._unit._PostGameAbility)
                {
                    _cont._unit._Ability.UseAbility();
                }

                yield return new WaitForSeconds(1);
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

    public void StopRingsRotating()
    {
        foreach(DBRotateAroundCentre _items in _RotationComponents)
        {
            _items.Rotate = false;
        }
    }

    public void StartRingsRotating()
    {
        foreach (DBRotateAroundCentre _items in _RotationComponents)
        {
            _items.Rotate = true;
        }
    }

    public void ResetCamera()
    {
        _CameraMain.transform.position = _CameraOriginalPosition;
    }

    public void OpenShopBuyScreen(DBUnit _Unit , DBShopContainer _ShopCont)
    {
        _ShopBuyUI.SetActive(true);
        _ShopBuyUI.GetComponent<ShopBuyScreen>().SetUnit(_Unit, _ShopCont);
        _shop.gameObject.SetActive(false);

        // Set Shop UI Unit here
        //update the info
        //

    }

    public void CloseShopBuyScreen()
    {
        _ShopBuyUI.SetActive(false);
    }

    public void ShopBuyBack()
    {
        _ShopBuyUI.SetActive(false);
        _shop.gameObject.SetActive(true);
    }
}
