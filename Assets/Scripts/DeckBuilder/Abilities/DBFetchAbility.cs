using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "DBFetchAbility", menuName = "DeckBuilder/FetchAbility")]

public class DBFetchAbility : DBAbility
{

    public List<DBUnit> _CurrentDeck;

    public override void ActivateAbility()
    {
        _CurrentDeck = Game2Manager.Instance._deck._CurrentDeck;

        _CurrentDeck = _CurrentDeck.GroupBy(unit => unit._ID).Select(group => group.First()).ToList(); //Works


        //sends the list to the ui selection containing all the containers
        Game2Manager.Instance.UpdateGameState(GameState.FetchUnit);
        Game2Manager.Instance._SelectUnitUiComponent.OpenSelectionUI(_CurrentDeck);

    }
}
