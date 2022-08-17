using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class InternetChecker : MonoBehaviour
{
    private DownloadManager downloadManager;

    public bool InternetConnectionBool { get; private set; }

    private void Start()
    {
        downloadManager = FirebaseMenuManager.Instance.DownloadManager;
        StartCoroutine(CheckInternetConnection());
    }

    private IEnumerator CheckInternetConnection()
    {

        UnityWebRequest webRequest = new UnityWebRequest("https://www.google.com");
        yield return webRequest.SendWebRequest();

        if(webRequest.result == UnityWebRequest.Result.Success)
        {
            InternetIsAvailable();
            Debug.Log("Internet Available");
        }

        else
        {
            InternetIsNotAvailable();
            Debug.Log("Internet Not Available");
        }

        StartCoroutine(CheckInternetConnection());
    }

    private void InternetIsNotAvailable()
    {
        InternetConnectionBool = false;

        downloadManager.CancelDownload();
    }

    private void InternetIsAvailable()
    {
        InternetConnectionBool = true;

        downloadManager.ContinueDownload();
    }
}