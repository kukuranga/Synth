using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<DBUnit> _AllUnits;
    public DBUnitList _StartingDeck;

    public List<DBUnit> _ActiveDeck;
    public List<DBUnit> _CurrentDeck; // Used while a game is running

    private void Start()
    {
        Game2Manager.Instance.SetDeck(this);
        Game2Manager.Instance.UpdateGameState(GameState.pregame);
    }

    public void SetActiveDeck()
    {
        _ActiveDeck = new List<DBUnit>(_StartingDeck._UnitList); // clone
    }

    public void AddUnit(DBUnit _unit)
    {
        _ActiveDeck.Add(_unit);
    }

    public void StartRound()
    {
        _CurrentDeck = new List<DBUnit>(_ActiveDeck); // clone
        ShuffleCurrentDeck();
    }

    public void ShuffleCurrentDeck()
    {
        // Fisher–Yates Shuffle
        for (int i = _CurrentDeck.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            DBUnit temp = _CurrentDeck[i];
            _CurrentDeck[i] = _CurrentDeck[randomIndex];
            _CurrentDeck[randomIndex] = temp;
        }
    }

    public DBUnit PullUnit()
    {
        if (_CurrentDeck == null || _CurrentDeck.Count == 0)
        {
            Debug.LogWarning("No cards left in the deck!");
            return null;
        }

        DBUnit _unit = _CurrentDeck[0];
        _CurrentDeck.RemoveAt(0);
        return _unit;
    }
}
