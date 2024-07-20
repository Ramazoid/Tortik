using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;

public class MyBuildPostprocessor
{
    [PostProcessBuildAttribute(1)]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
    {
        Debug.Log("****************************________<size=30><b><color=yellow>"+Path.GetFileName(pathToBuiltProject)+"</color></b></size>");
    }
}