using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlay : MonoBehaviour
{
    internal static GameObject tortik;
    public static float scale;
    public RectTransform NoInternetSign;
    public Image RotateTip;
    public static bool press;
    private Vector3 startmouse;
    private float startrotY;
    private float startrotX;
    private Vector3 nowmouse;
    public static float deltaY;
    private float deltaX;
    private float newX;
    private float newY;

    internal static void CheckScale()
    {
        if (PlayerPrefs.HasKey("TortikScale"))
            scale = PlayerPrefs.GetFloat("TortikScale");

        SaveAndSetScale();

    }

    private static void SaveAndSetScale()
    {
        if (tortik != null)
        {
            tortik.transform.localScale = Vector3.one * scale;
        }
        PlayerPrefs.SetFloat("TortikScale", scale);

    }

    public void TestNoInternet()
    {
        MetaEvents.Fire("NoInternet");
    }

    public void Enlarge()
    {
        print("enlarge");
        scale += 0.1f; SaveAndSetScale();
    }
    public void Shrink()
    {
        print("Shrink");
        scale -= 0.1f; SaveAndSetScale();

    }

    void Start()
    {
        HideNoInternetSign();
        MetaEvents.Register("NoInternet", NoInternet);
        MetaEvents.Register("HideNoInternetSign", HideNoInternetSign);
        MetaEvents.Register("ShowRotateTip", ShowRotateTip);
        RotateTip.gameObject.SetActive(false);
    }

    private void HideNoInternetSign()
    {
        NoInternetSign.Reposition(Vector3.right * 1000);
    }

    public void ShowRotateTip()
    {
        RotateTip.gameObject.SetActive(true);
    }

    public void NoInternet()
    {
        NoInternetSign.Reposition(Vector3.zero);

    }
    void Update()
    {
        if (tortik == null) return;
        if (Input.GetMouseButtonDown(0))
        {
            RotateTip.gameObject.SetActive(false);
            press = true;
            startmouse = Input.mousePosition;
            startrotY = tortik.transform.eulerAngles.y;
            startrotX = tortik.transform.eulerAngles.x;
        }

        if (Input.GetMouseButtonUp(0))
        {
            press = false;
            deltaY = 0;
        }


        if (Input.GetMouseButton(0))
        {
            nowmouse = Input.mousePosition;
            deltaY = nowmouse.x - startmouse.x;
            deltaX = nowmouse.y - startmouse.y;
            newX = startrotX + deltaX / 10;
            newY = startrotY + deltaY / 10;


        }
        if (tortik != null)
            tortik.transform.rotation = Quaternion.Euler(newX, newY, 0);

    }
}
