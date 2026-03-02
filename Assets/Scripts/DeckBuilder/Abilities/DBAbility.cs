using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DBAbility", menuName = "DeckBuilder/Ability")]
public class DBAbility : ScriptableObject
{

    //This is The base Ability class
    //Children of this class will handle all the logic for the class

    public bool Activated; //If it is not activated, the ability will just be run passivly during the check game state
    public bool OnPull; //This activates if the ability happens after the unit is pulled
    public Sprite _sprite; //This is the sprite associated with the ability, to be displayed on the character portrate
    public string Description; //Describes the ability and its uses
    public string Prompt; //Used to detail what the ability does at the bottom of the screen
    public bool _Used; //This is used to tell if an ability is already used
    public DBUnit _OwnerUnit;



    public virtual void UseAbility()
    {

    }

    public virtual void ActivateAbility()
    {
        
    }

    public virtual void OnPullAbility()
    {

    }

}
