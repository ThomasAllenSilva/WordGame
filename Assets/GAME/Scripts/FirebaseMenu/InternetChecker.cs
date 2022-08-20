using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class InternetChecker : MonoBehaviour
{
    private DownloadManager downloadManager;

    private UnityWebRequest webRequest;

    private void Awake()
    {
        downloadManager = FirebaseMenuManager.Instance.DownloadManager;

        StartCoroutine(CheckInternetConnection());
    }

    
    private IEnumerator CheckInternetConnection()
    {
        webRequest = new UnityWebRequest("https://www.google.com");

        yield return webRequest.SendWebRequest();

        switch (webRequest.result) 
        {
            case UnityWebRequest.Result.Success:
                InternetIsAvailable();
                break;

            case UnityWebRequest.Result.ConnectionError:
                InternetIsNotAvailable();
                break;
        }

        StartCoroutine(CheckInternetConnection());
    }

    private void InternetIsNotAvailable()
    {
        downloadManager.CancelDownload();
    }

    private void InternetIsAvailable()
    {
        downloadManager.ContinueDownload();
    }
}