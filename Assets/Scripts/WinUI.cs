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
        var rest = internalSheepDamages.total - internalSheepDamages.current;
        winText.text = $"You win! \n <color=black><size=20> You arrived with  <color=yellow>{rest}</color> / <color=yellow>{internalSheepDamages.total}</color> th \nof your merchandise </size></color>";
    }
}
