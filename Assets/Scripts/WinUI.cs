using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinUI : MonoBehaviour
{
    [SerializeField] InternalSheepDamages internalSheepDamages;
    [SerializeField] TextMeshProUGUI winText;

    private void Start()
    {
        winText.text = $"You win! \n <color=black><size=20> You arrived with  <color=yellow>{internalSheepDamages.current}</color> / <color=yellow>{internalSheepDamages.total}</color> th \nof your merchandise </size></color>";
    }
}
