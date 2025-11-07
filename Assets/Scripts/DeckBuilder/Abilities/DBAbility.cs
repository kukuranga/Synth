using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DBAbility", menuName = "DeckBuilder/Ability")]
public class DBAbility : ScriptableObject
{

    //This is The base Ability class
    //Children of this class will handle all the logic for the class

    public bool Activated; //If it is not activated, the ability will just be run passivly during the check game state
    public Sprite _sprite; //This is the sprite associated with the ability, to be displayed on the character portrate
    public string Description; //Describes the ability and its uses



    public virtual void UseAbility()
    {

    }

}
