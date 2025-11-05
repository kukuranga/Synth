using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DBUnitList", menuName = "DeckBuilder/UnitList")]
public class DBUnitList : ScriptableObject
{
    public List<DBUnit> _UnitList;

}
