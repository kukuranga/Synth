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
        //Get A list of all active units in the deck; -DONE
        //send the list to the UI for selection
        //On click use the second ability of this unit
        _CurrentDeck = Game2Manager.Instance._deck._CurrentDeck;

        _CurrentDeck = _CurrentDeck.GroupBy(unit => unit._ID).Select(group => group.First()).ToList(); //Works

        //TODO create The UI

        //sends the list to the ui selection containing all the containers
        Game2Manager.Instance.UpdateGameState(GameState.FetchUnit);
        Game2Manager.Instance._SelectUnitUiComponent.OpenSelectionUI(_CurrentDeck);
        //opens the UI
        //OnClick should add the unit to the deck and add to active container
        //Add check to see if its posible to add a unit at all, if not it should not be selectable (ON full containers disable this ability)

    }
}
