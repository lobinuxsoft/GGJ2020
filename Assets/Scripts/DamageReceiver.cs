using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    public void SetDamage(Vector3 pos, float force)
    {
        Debug.Log($"Collision point: {pos} / Force: {force}");
    }
}
