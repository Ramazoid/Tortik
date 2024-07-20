using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaEvents : MonoBehaviour
{
    private static MetaEvents IN;
    public Dictionary<string, Action> events = new Dictionary<string, Action>();
    public Dictionary<string, Action> callbacks = new Dictionary<string, Action>();
    public List<string> eventNames = new List<string>();

    internal static void Fire(string eventName)
    {
        Action a = GetAction(eventName);
        if (a != null) a();
        else Debug.LogError($"Something went wrong with event[{eventName}]");
    }

    private static Action GetAction(string eventName)
    {
        if(IN.events.ContainsKey(eventName))
            return IN.events[eventName];
        else
            throw new Exception($"Non existing event[<color=yellow>{eventName}</color>]");
    }

    internal static void Register(string eventBName, Action action)
    {
        if (IN.events.ContainsKey(eventBName))
            throw new Exception($"Duplicate event subscription for event[{eventBName}]");
        else
       
            IN.events.Add(eventBName, action);
        IN.eventNames = new List<string>(IN.events.Keys);
    }

    private void Awake()
    {
        IN = this;
    }
    public void FireUICallBack(string eventName)
    {
        if (IN.events.ContainsKey(eventName))
        {
            Action a = IN.events[eventName];
            a();
        }
        else
            throw new Exception($"Non existing CallBack[<color=yellow>{eventName}</color>]");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
