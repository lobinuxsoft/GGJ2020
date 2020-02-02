using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotator : MonoBehaviour
{
    [SerializeField] float speed = 360;
    [SerializeField] Vector3 axis = Vector3.up;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.Rotate(axis * speed * Time.deltaTime);
    }
}
