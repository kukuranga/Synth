using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum LevelPreSet
{
    Normal,
    Rain,
    Dust,
    lava,
    gold
}
public class GameManager : Singleton<GameManager>
{
    //Todo: add logic to persist the number of moves available and add the moves on each level up

    //TODO  Today: add different value presets to load for each level and determin the ammount dependant on level
    // eg a frozen level and a lava level preset
    
    //Todo: Use this to add to the level preset and add on differnt values to roll for when spawning the planets
    // eg: rain planet will have a 0% chance to spawn lava but a 60% to spawn frozen planets
    public LevelPreSet _levelPreSet;

    //possible presets: Rain level(more ice planets), lava level(more red planets), gold levels(more golden planets)


    //add here: Preset values for all planets, a save method to save the planets original values on normal levels,

    //add here values for each of the different planet presets depending on the _levelpreset selected

    //Figure out the different how the level presets would be required.

    /*
    -figure out how to add visuals to moving planets
    -Fix Color spawning when color variation is selected
    -remake the color background to be an even circle
    -Add background particle systems to each spawn location for buttons
    -Use those PS to create a graphic when set correct is triggered,
    That will draw lines or cubes to the synth position
    -Add a boarder around the game that can change colorand be used
    to change color depending on the state of the game

    Special levels
    -100 (Special type)


    Problems
    -Hard stuck at rain level

    -fix visuals for color spinning
    */


    public bool _SetColors;
    public List<Color> colors;

    public bool _Debugger;
    public float _SwipeSensitivity;
    public int _Level = 1;
    public SceneReference _Homepage;
    public SceneReference _GameOverScene;

    //Set scene to load when the game starts
    public SceneReference _LevelToLoad;
    public int _RowsToGive = 1;
    public bool _GameOver = false;

    private int _MovesLeft = 0;
    private int _MovesToGive = 8;

    public int _TotalPurpleItemsToSpawn = 0;
    public int _purpleItemsSpawned = 0;
    public int _YellowItemsSpawned = 0;
    public int _FrozenItemLimit = 5;
    public int _FrozenItemsSpawned = 0;

    //Synth Upgrade Stats;
    [SerializeField] int _StartMoveBonusMultiplier = 1;
    [SerializeField] int _RoundsMoveBonusMultiplier = 1;
    [SerializeField] float _GoldItemChanceBonusMultiplier = 0.01f;
    [SerializeField] float _TreasureItemChanceMultiplier = 0.01f;
    [SerializeField] float _LuckBonusMultiplier = 0.02f;

    private int _StartMoveBonus = 0;
    private int _RoundWonBonus = 0;
    private int _GoldItemChanceBonus = 0;
    private int _TreasureItemChanceBonus = 0;
    private int _LuckBonus = 0;

    //GoldItem
    [SerializeField] private float _GoldenItemChance = 0.1f;
    [SerializeField] private int _GoldenItemBonus = 6;

    [SerializeField] private float _PurpleItemChance = 0.4f;
    [SerializeField] private float _RedItemChance = 0f;
    [SerializeField] private float _YellowItemChance = 0f;

    [SerializeField] private float _FrozenItemChance = 0f;
    [SerializeField] private int _StartingMoves = 8;

    public Color _RedItemColorChange;
    public Color _PurpleItemColorChange;

    //Item Unlock Bools
    public bool _GoldItemUnclocked = false;
    public bool _PurpleItemUnlocked = false;
    public bool _RedItemUnlocked = false;
    public bool _YellowItemUnlocked = false;
    public bool _FrozenItemUnlocked = false;
    public bool _TreasureItemUnlocked = false;

    //UI Unlocks
    public bool GoalsUnlocked = false;
    public bool StatsUnlocked = false;
    public bool SynthUnlocked = false;

    //Reset Values post unlocks
    private float _GoldItemPreset = 0f;
    private float _PurpleItemPreset = 0f;
    private float _RedItemPreset = 0f;
    private float _YellowItemPreset = 0f;
    private float _FrozenItemPreset = 0f;

    private void Awake()
    {
        SaveManager.Instance.LoadGame();
    }

    private void Start()
    {
        //AudioManager.Instance.PlayMusic("Music");
        AudioManager.Instance.CrossfadeMusic("MusicOld", 1f);
        OnStartSetRunValues();
        CheckLevelPreset();//Test here 
    }

    private void Update()
    {
        if(_Debugger)
        {
            _RowsToGive = 3;
            _MovesToGive = 9001;
        }
    }

    //Playes when the main game starts
    public void GameStart()
    {
        AudioManager.Instance.CrossfadeMusic("Music", 1f);
    }

    //Happenes when the game ends and the player goes back to the home screen
    public void GameEnd()
    {
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlayMusic("MusicOld");
    }

    //Base Values used to reset
    private float _BaseGoldenItemChance;
    private int _BaseGoldenItemBonus;
    private float _BasePurpleItemChance;
    private float _BaseRedItemChance;
    private float _BaseYellowItemChance;
    private float _BaseFrozenItemChance;
    private int _BaseStartingMoves;
    private float _BaseRewardChance;

    //resets the values after a failed run attempt
    public void ResetRunValues()
    {
        _TotalPurpleItemsToSpawn = 1;
        _RowsToGive = 1;
        _MovesToGive = _StartingMoves;
        _GoldenItemChance = _BaseGoldenItemChance;
        _GoldenItemBonus = _BaseGoldenItemBonus;
        _PurpleItemChance = _BasePurpleItemChance;
        _RedItemChance = _BaseRedItemChance;
        _YellowItemChance = _BaseYellowItemChance;
        _FrozenItemChance = _BaseFrozenItemChance;
        _StartingMoves = _BaseStartingMoves;
        RewardsManager.Instance._RewardsChance = _BaseRewardChance;
    }

    private void OnStartSetRunValues()
    {
        //Set the default values to be reset on game load
        _BaseGoldenItemChance = _GoldenItemChance;
        _BaseGoldenItemBonus = _GoldenItemBonus;
        _BasePurpleItemChance = _PurpleItemChance;
        _BaseRedItemChance = _RedItemChance;
        _BaseYellowItemChance = _YellowItemChance;
        _BaseFrozenItemChance = _FrozenItemChance;
        _BaseStartingMoves = _StartingMoves;
        _BaseRewardChance = RewardsManager.Instance._RewardsChance;
    }

    private void CheckLevel()
    {
        Debug.Log("Level checkd");
        switch (_Level)
        {
            case 1:
                ResetRunValues();
                AddBonusesToGame();
                break;
            case 2:
                _MovesToGive = 5;
                _RowsToGive = 1;
                break;
            case 5:
                _MovesToGive += 1;
                _RowsToGive = 2;
                break;
            case 10:
                _MovesToGive += 3;
                _levelPreSet = LevelPreSet.gold;
                break;
            case 11:
                _levelPreSet = LevelPreSet.Normal;
                break;
            case 15:
                _MovesToGive += 2;
                _RowsToGive = 3;
                break;
            case 25:
                _TotalPurpleItemsToSpawn++;
                _MovesToGive += 1;
                IncreaseFrozenItemChance(0.2f);
                IncreaseYellowItemChance(0.1f);
                _RowsToGive = 3;
                _levelPreSet = LevelPreSet.lava;
                break;
            case 26:
                _levelPreSet = LevelPreSet.Normal;
                break;
            case 30:
                IncreaseFrozenItemChance(0.1f);
                _MovesToGive += 3;
                _RowsToGive = 3;
                _levelPreSet = LevelPreSet.Dust;
                break;
            case 31:
                _levelPreSet = LevelPreSet.Normal;
                break;
            case 40:
                _levelPreSet = LevelPreSet.Rain;
                break;
            case 41:
                _levelPreSet = LevelPreSet.Normal;
                break;
            case 50:
                IncreaseYellowItemChance(0.1f);
                _MovesToGive -= 5;
                _RowsToGive = 3;
                _levelPreSet = LevelPreSet.Dust;
                break;
            case 51:
                _levelPreSet = LevelPreSet.Normal;
                break;
            default:
                //_MovesToGive--;
                break;
        }
        CheckLevelPreset();
    }

    bool _PresetSaved = false;

    public void CheckLevelPreset()
    {
        //Set the preset amounts
         if (_levelPreSet != LevelPreSet.Normal && !_PresetSaved)
        {
            _GoldItemPreset = _GoldenItemChance;
            _PurpleItemPreset = _PurpleItemChance;
            _RedItemPreset = _RedItemChance;
            _YellowItemPreset = _YellowItemChance;
            _FrozenItemPreset = _FrozenItemChance;
            _PresetSaved = true;
        }
        else if(_PresetSaved)
        {
            ResetValuesAfterLevelPreset();
            _PresetSaved = false;
        }

        switch(_levelPreSet)
        {
            case LevelPreSet.Normal:
                //nothing changes
                break;
            case LevelPreSet.Dust:
                if (_YellowItemUnlocked)
                {
                    _GoldenItemChance = 0f;
                    _PurpleItemChance = 0f;
                    _RedItemChance = 0f;
                    _YellowItemChance = 0.6f;
                    _FrozenItemChance = 0f;
                }
                break;
            case LevelPreSet.Rain:
                if (_FrozenItemUnlocked)
                {
                    _GoldenItemChance = 0f;
                    _PurpleItemChance = 0f;
                    _RedItemChance = 0f;
                    _YellowItemChance = 0.2f;
                    _FrozenItemChance = 0.85f;
                }
                break;
            case LevelPreSet.lava:
                if (_RedItemUnlocked)
                {
                    _GoldenItemChance = 0f;
                    _PurpleItemChance = 0f;
                    _RedItemChance = 0.6f;
                    _YellowItemChance = 0f;
                    _FrozenItemChance = 0f;
                }
                break;
            case LevelPreSet.gold:
                if (_GoldItemUnclocked)
                {
                    _GoldenItemChance = 0.6f;
                    _PurpleItemChance = 0f;
                    _RedItemChance = 0f;
                    _YellowItemChance = 0f;
                    _FrozenItemChance = 0f;
                }
                break;
        }
    }

    public void ResetValuesAfterLevelPreset()
    {
        //Porblem: Not correctly assigning the new chance values
        _GoldenItemChance = _GoldItemPreset;
        _PurpleItemChance = _PurpleItemPreset;
        _RedItemChance = _RedItemPreset;
        _YellowItemChance = _YellowItemPreset;
        _FrozenItemChance = _FrozenItemPreset;
    }

    public void SetSynthBonuses(int _StartMoves, int _Move, int _GoldItems, int _TreasureItems, int _Luck)
    {
        _StartMoveBonus = _StartingMoves;
        _StartMoveBonus = _Move;
        _GoldItemChanceBonus = _GoldItems;
        _TreasureItemChanceBonus = _TreasureItems;
        _LuckBonus = _Luck;
    }

    private  void AddBonusesToGame()
    {
        _StartingMoves += (_StartMoveBonus * _StartMoveBonusMultiplier);
        _MovesToGive += (_RoundWonBonus * _RoundsMoveBonusMultiplier);
        _GoldenItemChance += (_GoldItemChanceBonus * _GoldItemChanceBonusMultiplier);
        RewardsManager.Instance._RewardsChance += (_TreasureItemChanceBonus * _TreasureItemChanceMultiplier);
        RewardsManager.Instance._Luck += Mathf.RoundToInt(_LuckBonus * _LuckBonusMultiplier);
    }

    public void AddMovesToGive(int amount)
    {
        _MovesToGive += amount;
    }

    public void AddToGoldSpawnChance(float _Increase)
    {
        _GoldenItemChance += _Increase;
    }

    public void IncreasePurpleItemChance(float _Increase)
    {
        _PurpleItemChance += _Increase;
    }

    public void IncreaseYellowItemChance(float _Increase)
    {
        _YellowItemChance += _Increase;
    }

    public void IncreaseRedItemChance(float _Increase)
    {
        _RedItemChance += _Increase;
    }

    public void IncreaseFrozenItemChance(float _Increase)
    {
        _FrozenItemChance += _Increase;
    }

    public void IncreaseGoldItemBonus(int _value)
    {
        _GoldenItemBonus += _value;
    }

    public int GetGoldenBonus()
    {
        return _GoldenItemBonus;
    }

    private int _SpawnDecay = 1;
    public bool SpawnGoldenItem()
    {
        if (_GoldItemUnclocked)
        {
            float c = Random.Range(0f, 1f);
            if (c <= (_GoldenItemChance / _SpawnDecay))
            {
                _SpawnDecay++;
                return true;
            }
        }

        return false;
    }

    public bool SpawnPurpleItem()
    {
        if (_PurpleItemUnlocked)
        {
            float c = Random.Range(0f, 1f);
            if (c <= _PurpleItemChance)
            {
                return true;
            }
        }

        return false;
    }

    public bool SpawnYellowItem()
    {
        if (_YellowItemUnlocked && _RowsToGive >= 3)
        {
            float c = Random.Range(0f, 1f);
            if (c <= _YellowItemChance)
            {
                return true;
            }
        }

        return false;
    }

    public bool SpawnRedItem()
    {
        if (_RedItemUnlocked)
        {
            float c = Random.Range(0f, 1f);
            if (c <= _RedItemChance)
            {
                return true;
            }
        }
        return false;
    }

    public bool SpawnFrozenItem()
    {
        if (_FrozenItemUnlocked && _RowsToGive > 1)
        {
            float c = Random.Range(0f, 1f);
            if (c <= _FrozenItemChance)
            {
                return true;
            }
        }
        return false;
    }

    public int GetMovesToGive()
    {
        return _MovesToGive;
    }

    public void StoreMoves(int n)
    {
        _MovesLeft = n;
    }

    public int SetMoves()
    {

        return _MovesLeft + _MovesToGive;
    }

    public void SetSwipeSensitivity(int n)
    {
        _SwipeSensitivity = n;
    }

    public void GameOver()
    {
        if (!_GameOver)
        {
            ResetGame();
            _GameOver = true;
        }
    }

    public void GameWon()
    {
        SetMoves();
        IncreasePurpleItemChance(0.001f);
        IncreaseRedItemChance(0.002f);
        _purpleItemsSpawned = 0;
        _YellowItemsSpawned = 0;
        _FrozenItemsSpawned = 0;
        _SpawnDecay = 1;
        CheckLevelPreset();
        StatsManager.Instance.AddToLevelsCompleted(1);
        StatsManager.Instance.CheckHighestLevelCompleted(_Level);
        _Level++;
        SynthManager.Instance.GrowSynth();
        SaveManager.Instance.SaveGame();
        CheckLevel();
    }

    public void ResetGame()
    {
        StatsManager.Instance.AddToTotalNumberOfRuns(1);
        _Level = 1;
        _MovesLeft = 0;
        _SpawnDecay = 1;
        _GameOver = false;
        _purpleItemsSpawned = 0;
        _YellowItemsSpawned = 0;
        _FrozenItemsSpawned = 0;
        PointsManager.Instance.ResetPoints();
        SynthManager.Instance.ResetSynth();
        SaveManager.Instance.SaveGame();
        CheckLevel();
    }
}
