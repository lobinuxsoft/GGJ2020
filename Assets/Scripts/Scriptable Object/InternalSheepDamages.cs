using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewInternalSheepDamages", menuName = "Scriptable Object/InternalSheepDamages")]
public class InternalSheepDamages : BaseVariable
{
    public DamageEvent OnDamage;
}

[System.Serializable]
public class DamageEvent : UnityEvent<float>{ }