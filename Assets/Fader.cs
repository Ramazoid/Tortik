using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    public  CanvasGroup CG;
    private static Fader IN;
    private Action Callback;

    private void Awake()
    {
        IN = this;
    }



    internal static void FadeOUT(Action callback=null)
    {
        IN.Callback = callback;
            IN.StartCoroutine(IN.FadeOut());
    }

     IEnumerator FadeOut()
    {
        while (CG.alpha >= 0)
        {
            IN.CG.alpha -= 0.005f;
            yield return null;
        }
        if(IN.Callback != null) IN.Callback();
    }

    void Start()
    {
        CG = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        
    }
}
