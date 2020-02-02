using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private void Awake()
    {
        if (instance && instance != this)
            Destroy(this);
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public GameObject dangerText;
    public GameObject repairProgress;
    public Text fireAmountText;

    public void UpdateFireAmountText(int amount) 
    {
        fireAmountText.text = "x " + amount;

        if (amount > 0)
            dangerText.SetActive(true);
        else
            dangerText.SetActive(false);
    }

    public void TriggerRepairProgress() 
    {
        repairProgress.SetActive(!repairProgress.activeInHierarchy);
        repairProgress.GetComponent<Animator>().Play("RepairProgress");
    }
}
