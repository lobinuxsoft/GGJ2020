using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public static Fade Reference;
    public CanvasGroup group;

    void Awake()
    {
        Reference = this;
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInCorrutine());

    }

    public void fadeOut()
    {
        StartCoroutine(FadeOutCorrutine());
    }


    IEnumerator FadeInCorrutine()
    {
        for (float f = 0.1f; f <= 1.0f; f += 0.01f)
        {
            group.alpha = f;
            yield return null;
        }
    }

    IEnumerator FadeOutCorrutine()
    {
        for (float f = 1.0f; f >= 0.0f; f -= 0.01f)
        {
            group.alpha = f;
            yield return null;
        }
    }







}
