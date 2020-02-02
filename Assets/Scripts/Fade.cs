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
        if(Reference == null)
        {
            Reference = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInCorrutine());

    }

    public void FadeOut()
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
