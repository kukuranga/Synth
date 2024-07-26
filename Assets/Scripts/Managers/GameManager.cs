using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //Todo: add logic to persist the number of moves available and add the moves on each level up

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


    private void Start()
    {
        AudioManager.Instance.PlayMusic("Music");
        OnStartSetRunValues();
        SaveManager.Instance.LoadGame();
    }

    private void Update()
    {
        if(_Debugger)
        {
            _RowsToGive = 3;
            _MovesToGive = 9999;
        }
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
                break;
            case 30:
                IncreaseFrozenItemChance(0.1f);
                _MovesToGive += 3;
                _RowsToGive = 3;
                break;
            case 50:
                IncreaseYellowItemChance(0.1f);
                _MovesToGive -= 5;
                _RowsToGive = 3;
                break;
            default:
                //_MovesToGive--;
                break;
        }
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
        if (_YellowItemUnlocked)
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
    
    public void GameOver()
    {
        SaveManager.Instance.SaveGame();
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
        _SpawnDecay = 1;
        StatsManager.Instance.AddToLevelsCompleted(1);
        StatsManager.Instance.CheckHighestLevelCompleted(_Level);
        _Level++;
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
        PointsManager.Instance.ResetPoints();
        CheckLevel();
    }
}
