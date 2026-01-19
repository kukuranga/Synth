using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleManager : Singleton<CircleManager>
{

    //used to keep track of all the circle visuals in the game
    public GameObject Circle1;
    public GameObject Circle2;

    public Vector3 Circle1OriginalScale;
    public Vector3 Circle1SmallScale;

    private void Start()
    {
        Circle1OriginalScale = Circle1.transform.localScale;
    }

    //make circle 1 and all containers smaller in scale and make circle 2 visible
    public void ExpandCircleOne()
    {

        //TODO: when you close the shop make an animation here to show the expansion
        Circle1.transform.localScale = Circle1SmallScale;
        Circle2.SetActive(true);

    }


}
