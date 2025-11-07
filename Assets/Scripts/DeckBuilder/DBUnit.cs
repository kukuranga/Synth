using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DBUnit", menuName = "DeckBuilder/Unit")]
public class DBUnit : ScriptableObject
{
    //The base deck builder unit 
    public string _Name;
    public string _Description;
    public int _YellowResourceGain;
    public int _GreenResourceGain;
    public bool _Star;
    public bool _Danger;
    public bool _ActivatedAbility;
    public bool _PostGameAbility;
    public Sprite _sprite;
    public int _ShopCost;
    public int _NumberOfBuys;
    public DBAbility _Ability;
    public bool _FlagDangerReduction;

    public void OnConditionsChecked()
    {

    }

}
