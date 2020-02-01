using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    [SerializeField] InternalSheepDamages internalSheepDamages;
    public void SetDamage(float damage)
    {
        internalSheepDamages.OnDamage.Invoke(damage);
    }
}
