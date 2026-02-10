using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSetUp : MonoBehaviour
{

    public List<GameObject> _ObjectsToSetActiveOnStart;

    void Awake()
    {
        foreach(GameObject _obj in _ObjectsToSetActiveOnStart)
        {
            _obj.SetActive(true);
        }
    }

}
