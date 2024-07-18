using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlock : ScriptableObject
{
    //Base script for all achievement unlocks
    public string _Description;

    public virtual void Bonus()
    {

    }
}
