using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public enum State
    {
        broken,
        inReparation,
        reapired,
        count
    }

    public State state = State.reapired;
    public InternalSheepDamages damages;
    public float reparationTime = 1.0f;
    private ParticleSystem fire;

    private void Start()
    {
        gameObject.tag = Tags.repairObjectTag;
        damages.total++;
        damages.OnDamage.AddListener(TryBreak);
        fire = GetComponentInChildren<ParticleSystem>();
    }

    private void TryBreak(float breakProbavility)
    {
        if (Random.Range(0, 100) <= breakProbavility)
        {
            Break();
        }
    }

    public void Break()
    {
        if (state == State.reapired)
        {
            state = State.broken;
            damages.current++;
            //Debug.Log("Broken: " + gameObject.name);
            fire.Play();
        }
    }

    public void InReparation()
    {
        if (state == State.broken)
        {
            state = State.inReparation;
            Invoke("Repair", reparationTime);
        }
    }

    private void Repair()
    {
        if (state == State.inReparation)
        {
            state = State.reapired;
            fire.Stop();
            damages.current--;
            damages.OnRepair.Invoke();
        }
    }
}
