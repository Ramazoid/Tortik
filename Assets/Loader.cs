using Siccity.GLTFUtility;
using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Random = UnityEngine.Random;

public class Loader : MonoBehaviour
{
    public ARTrackedImageManager tm;    
    private IReferenceImageLibrary library;
    public AddReferenceImageJobState state = new AddReferenceImageJobState();
    public static GameObject tortik;


    void Start()
    {
        ARSession session = FindAnyObjectByType<ARSession>();
        
        tm = FindFirstObjectByType<ARTrackedImageManager>();
        library = tm.referenceLibrary;
       
        MetaEvents.Register("TryLoadAgain", LoadItAll);

        LoadItAll();
    }
    public void LoadItAll()
    {
        MetaEvents.Fire("HideNoInternetSign");
        string markerUrl = "https://user74522.clients-cdnnow.ru/static/uploads/mrk6440mark.png";
        StartCoroutine(MarkerLoader(markerUrl));

        string tortikUrl = "https://user74522.clients-cdnnow.ru/static/uploads/mrk6564obj.glb ";
        StartCoroutine(TortikLoad(tortikUrl));
    }

    IEnumerator TortikLoad(string tortikUrl)
    {
        
        using (UnityWebRequest www = UnityWebRequest.Get(tortikUrl))
        {
            var operation = www.SendWebRequest();

            while (!operation.isDone)
            {
                Progress.Show("Loading MODEL", www.downloadProgress);
                yield return new WaitForSeconds(0.001f);
            }
            

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
                MetaEvents.Fire("NoInternet");

            if (www.result == UnityWebRequest.Result.Success)
            {
                string savePath = Application.persistentDataPath+"/tortik.glb";
                try
                {
                    System.IO.File.WriteAllBytes(savePath, www.downloadHandler.data);
                }
                catch (Exception e)
                {
                    print("какая-то ебатория с записью" + e.ToString());
                }
                finally
                {
                    Progress.Done();
                    InstantiateTortik(savePath);
                }
            }
            else
                print("tortik error:" + www.result);
        }
    }

    private async void InstantiateTortik(string savePath)
    {
        tortik= Importer.LoadFromFile(savePath, Format.GLB);
        tortik.name = "TORTIK";
        tortik.SetActive(false);
    }

    public IEnumerator MarkerLoader(string url)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
        {

            var operation = www.SendWebRequest();

            while (!operation.isDone)
            {
                Progress.Show("Loading MARKER", www.downloadProgress);
                yield return new WaitForSeconds(0.01f);
            }
            Progress.Done();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
                MetaEvents.Fire("NoInternet");
            else
            if (www.result == UnityWebRequest.Result.Success)
            {
                Texture2D tex = DownloadHandlerTexture.GetContent(www) as Texture2D;
               SetTexture(tex);

            }
            else
                print("not loaded: " + www.result);
        }
    }
    public async void SetTexture(Texture2D tex)
    {
        var library = tm.CreateRuntimeLibrary();

        if (library is MutableRuntimeReferenceImageLibrary mutableLibrary)
        {
            state = mutableLibrary.ScheduleAddImageWithValidationJob(
               tex,
               "new image",
               0.5f );
        }

        await Task.Run(() => { while (state.status != AddReferenceImageJobStatus.Success) ; });

        tm.referenceLibrary = library;
    }   
}
