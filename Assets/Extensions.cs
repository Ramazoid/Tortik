using UnityEngine;
using System;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public static class StringExtensions
{
    
    public static string Extract(this string s,char c)
    {
        int i = s.IndexOf(c);
        return s.Substring(0, i);
    }
    public static string UpTo(this string s, char c)
    {
        return s.Substring(0, s.IndexOf(c));
    }
}
public static class GameObjectExtensions
{
    public static T Child<T>(this GameObject g, string nam)
    {
        return g.transform.Find(nam).GetComponent<T>();
    }
    public static Vector3 Vector3X0Z(this Vector3 v)
    {
        return new Vector3(v.x, 0, v.z);
    }
    public static Vector3 MUltiply(this Vector3 v, float x, float y, float z)
    {
        return new Vector3(v.x * x, v.y * y, v.z * z);
    }
}
public static class RectTransformExtensions
{
public static void Reposition(this RectTransform rt, Vector3 pos)
    {
        rt.anchoredPosition = pos;
    }
    public static void ColorizeImage(this RectTransform rt,Color col)
    {
        Image im = rt.GetComponent<Image>();
        if (im != null)
            im.color = col;
        else
            throw new Exception($"Object [{rt.name}] does not have Image Component to colorize it!");
    }
}


