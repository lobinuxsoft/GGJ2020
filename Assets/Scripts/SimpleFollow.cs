using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollow : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    [SerializeField] private Transform targetToFollow;

    private void LateUpdate()
    {
        transform.position = targetToFollow.position + offset;
        transform.LookAt(targetToFollow);
    }
}
