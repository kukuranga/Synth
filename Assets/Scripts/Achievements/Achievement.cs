using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement : ScriptableObject
{
    public Sprite _Sprite;
    public string _Name;
    public string _Description;

    public virtual bool Achieve()
    {
        return false;
    }
}
