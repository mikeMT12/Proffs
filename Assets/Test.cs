using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class Test : MonoBehaviour
{
    public string url;
    IEnumerator send()
    {
        UnityWebRequest reqest = UnityWebRequest.Get(url);
        yield return reqest.SendWebRequest(); print(reqest.downloadHandler.text);
        File.WriteAllText("Assets/c#/code.cs", reqest.downloadHandler.text);
    }
    public void Start()
    {
        StartCoroutine(send());
    }
}