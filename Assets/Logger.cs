using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logger : MonoBehaviour
{
    public Text logText;

    private void Awake()
    {
        
    }

    void Start()
    {
        logText=GetComponent<Text>();
        //print("------------------------------Logger STSART ");

        Application.logMessageReceivedThreaded += log;logText.text = "";
    }

    private void log(string condition, string stackTrace, LogType type)
    {
        logText.supportRichText = true;
        if (type != LogType.Log)
        {
            string tag = "<color=";
            if (type == LogType.Exception || type == LogType.Error) { tag += "red>"; }
            logText.text += tag + condition + "\nType=" + type + "\n</color>";
        }
        else
            logText.text += condition + "\n";
    }
    public void Clear()
    {
        logText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
