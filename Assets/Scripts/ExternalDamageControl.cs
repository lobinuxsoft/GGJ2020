using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalDamageControl : MonoBehaviour
{
    [SerializeField] InternalSheepDamages internalSheepDamages;

    private void Start()
    {
        foreach (var item in transform.GetComponentsInChildren<ParticleSystem>())
        {
            item.Stop();
        }

        internalSheepDamages.OnRepair.AddListener(delegate { CheckDamage(0); });
        internalSheepDamages.OnDamage.AddListener(CheckDamage);    
    }

    private void CheckDamage(float noUse)
    {
        var check = (float)internalSheepDamages.current / (float)internalSheepDamages.total;

        var act = Mathf.FloorToInt(transform.childCount * check);

        var part = transform.GetComponentsInChildren<ParticleSystem>();

        for (int i = 0; i < part.Length; i++)
        {
            if(i < act)
            {
                part[i].Play();
            }
            else
            {
                part[i].Stop();
            }
        }
    }

    private void OnDestroy()
    {
        internalSheepDamages.ResetData();
    }
}
