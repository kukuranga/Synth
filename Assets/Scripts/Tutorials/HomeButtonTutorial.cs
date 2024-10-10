using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeButtonTutorial : MonoBehaviour
{

    public HomeScreenTutorial _tutorial;

    public void Onclick()
    {
        _tutorial.Next();
    }
}
