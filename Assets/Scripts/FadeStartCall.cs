using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeStartCall : MonoBehaviour
{
    [SerializeField] bool callFadeIn = false;
    // Start is called before the first frame update
    void Start()
    {
        if (callFadeIn)
            Fade.Reference.FadeIn();
        else
            Fade.Reference.FadeOut();
    }

    public void FadeIn() {
        Fade.Reference.FadeIn();
    }
    
    public void FadeOut() {
        Fade.Reference.FadeOut();
    }
}
