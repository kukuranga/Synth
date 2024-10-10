using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tutorial Object", menuName = "Tutorial", order = 51)]
public class TutorialSO : ScriptableObject
{
    public string _Text;
    public bool _EndingTut;
    public bool _Move;
    public Vector3 _Position;
}
