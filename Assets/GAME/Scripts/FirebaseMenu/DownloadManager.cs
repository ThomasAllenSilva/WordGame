using System.Collections;
using UnityEngine;

public class DownloadManager : MonoBehaviour
{
    private FirebaseMenuManager firebaseMenuManager;

    [SerializeField] private GameObject internetCanvas;

    public bool CanDownloadNewContent { get; private set; }

    private void Awake()
    {
        CanDownloadNewContent = true;
        firebaseMenuManager = FirebaseMenuManager.Instance;
        FirebaseRealTimeDataBaseReader.onFailedToReadDataBaseValues += CancelDownload;
    }

    public void CancelDownload()
    {
        CanDownloadNewContent = false;
        internetCanvas.SetActive(true);
    }

    public void ContinueDownload()
    {
        CanDownloadNewContent = true;
    }

    public void TryToDownloadLevelsAgain()
    {
        StartCoroutine(InitializeFirebaseAgain());
    }

    private IEnumerator InitializeFirebaseAgain()
    {
        yield return new WaitForSeconds(0.5f);
        firebaseMenuManager.FirebaseRealTimeDataBaseReader.InitializeFirebaseRealTimeDataBase();
    }

    private void OnDestroy()
    {
        FirebaseRealTimeDataBaseReader.onFailedToReadDataBaseValues -= CancelDownload;
    }
}
