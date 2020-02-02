using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryCall : MonoBehaviour
{
    public void Win()
    {
        Fade.Reference.FadeIn();
        StartCoroutine(Callwin());
    }

    IEnumerator Callwin()
    {
        yield return new WaitForSeconds(1.5f);
        SceneLoader.StaticCallLoadScene("NotLoseState");
    }
}
