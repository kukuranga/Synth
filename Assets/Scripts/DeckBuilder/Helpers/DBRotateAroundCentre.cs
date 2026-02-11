using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBRotateAroundCentre : MonoBehaviour
{
    public Transform target;
    public float orbitSpeed = 50f;
    public bool Rotate;

    void Update()
    {
        if (target == null) return;
        if (!Rotate) return;

        transform.RotateAround(target.position, Vector3.forward, orbitSpeed * Time.deltaTime);
    }
}
