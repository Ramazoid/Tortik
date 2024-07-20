using Siccity.GLTFUtility;
using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Random = UnityEngine.Random;

public class Detector : MonoBehaviour
{
    public ARTrackedImageManager tm;

    public AddReferenceImageJobState state = new AddReferenceImageJobState();
    private GameObject tortik;
    public Text SessionLog;
    public Fader fader;
    private ARSession session;

    void Start()
    {
        session = FindAnyObjectByType<ARSession>();

        session.enabled = true;
        StartCoroutine(WaitForSession());

        GamePlay.scale = 0.2f;
        tm = GetComponent<ARTrackedImageManager>();
        tm.trackedImagesChanged += Changed;
        print("state=" + ARSession.state);



    }


    IEnumerator WaitForSession()
    {
        
            if ((ARSession.state == ARSessionState.None) ||
                (ARSession.state == ARSessionState.CheckingAvailability))
            {

                SessionLog.text = "Wait for AR Session\n";
                yield return ARSession.CheckAvailability();
            }


        session.enabled = true;
        Fader.FadeOUT();
    }



            

    private void Changed(ARTrackedImagesChangedEventArgs e)
    {
        MetaEvents.Fire("ShowRotateTip");
        GameObject p = GameObject.FindGameObjectWithTag("Placement");
        

        if (GamePlay.tortik != null)
        {
            GamePlay.tortik.SetActive(true);

            if (p != null)
            { 
                GamePlay.tortik.transform.position = p.transform.position;  
                GamePlay.tortik.transform.localScale = Vector3.one * GamePlay.scale;
            }
        }
        else
        {
            if (Loader.tortik != null)
            {
                GamePlay.tortik = Instantiate(Loader.tortik, p.transform);
            }
        }
    }

}
