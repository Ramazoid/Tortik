using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Progress : MonoBehaviour
{
    public RectTransform bar;
    private float width100;
    private float hei;
    public float percent;
    private static Progress IN;
    public Text title;

    private void Awake()
    {
        IN = this;
    }


    void Start()
    {
        title = GetComponentInChildren<Text>();
        bar = transform.Find("Bar").GetComponent<RectTransform>();
        width100 = bar.rect.width;
        hei=bar.rect.height;
        percent = 50;
        SetPercent();
    }

    public void SetPercent()
    {
        bar.sizeDelta=new Vector2 (width100*(percent/100), hei);
            //SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, percent / 100 * width100);
    }

    void Update()
    {
        SetPercent();
    }

    internal static void Show(string title, float progress)
    {
        print(title+":"+progress);
        IN.title.text = title;
        IN.percent = progress * 100;
        IN.SetPercent();
    }

    internal static void Done()
    {
        IN.percent = 100;
        IN.SetPercent();
    }
}
