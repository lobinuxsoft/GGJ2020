using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public InternalSheepDamages InternalSheepDamages;

    public static UIManager instance;

    public GameObject dangerText;
    public GameObject repairProgress;
    public Text fireAmountText;

    private void Awake()
    {
        instance = this;

        InternalSheepDamages.ResetData();
    }

    private void Start()
    {
        Fade.Reference.FadeOut();
    }

    public void UpdateFireAmountText(int amount) 
    {
        fireAmountText.text = "x " + amount;

        if (amount > 0)
            dangerText.SetActive(true);
        else
            dangerText.SetActive(false);

        if (InternalSheepDamages.isAllDestroyed()) 
        {
            Fade.Reference.FadeIn();
            StartCoroutine(CallLose());
        }
    }

    public void TriggerRepairProgress() 
    {
        repairProgress.SetActive(!repairProgress.activeInHierarchy);
        repairProgress.GetComponent<Animator>().Play("RepairProgress");
    }

    IEnumerator CallLose()
    {
        yield return new WaitForSeconds(1.5f);
        SceneLoader.StaticCallLoadScene("LoseState");
    }
}
