using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    enum State
    {
        broken,
        inReparation,
        reapired,
        count
    }

    private State state = State.reapired;
    public float reparationTime = 1.0f;
    private void Start()
    {
        gameObject.tag = Tags.repairObjectTag;
        GetComponent<MeshRenderer>().material.color = Color.green;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Break();
        }
    }

    public void Break()
    {
        if (state == State.reapired)
        {
            state = State.broken;

            GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    public void InReparation()
    {
        if (state == State.broken)
        {
            state = State.inReparation;
            GetComponent<MeshRenderer>().material.color = Color.yellow;
            Invoke("Repair", reparationTime);
        }
    }

    private void Repair()
    {
        if (state == State.inReparation)
        {
            state = State.reapired;
            GetComponent<MeshRenderer>().material.color = Color.green;
        }
    }
}
