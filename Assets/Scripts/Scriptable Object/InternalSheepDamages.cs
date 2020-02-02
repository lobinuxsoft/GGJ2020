﻿using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewInternalSheepDamages", menuName = "Scriptable Object/InternalSheepDamages")]
public class InternalSheepDamages : BaseVariable
{
    public int total = 0;
    public int current = 0;
    public DamageEvent OnDamage;
    public UnityEvent OnRepair;

    public void ResetData()
    {
        total = 0;
        current = 0;
        OnDamage.RemoveAllListeners();
        OnRepair.RemoveAllListeners();
    }
}

[System.Serializable]
public class DamageEvent : UnityEvent<float>{ }