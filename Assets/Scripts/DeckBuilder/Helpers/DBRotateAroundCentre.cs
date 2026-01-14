using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBRotateAroundCentre : MonoBehaviour
{
    public Transform target;
    public float orbitSpeed = 50f;

    void Update()
    {
        if (target == null) return;

        transform.RotateAround(target.position, Vector3.forward, orbitSpeed * Time.deltaTime);
    }
}
