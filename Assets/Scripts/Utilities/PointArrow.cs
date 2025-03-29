using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointArrow : MonoBehaviour
{
    public Transform pointTarget;
    private void Update()
    {
        Vector3 dir = transform.position - pointTarget.position;
        dir.Normalize();
        transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
    }
}
