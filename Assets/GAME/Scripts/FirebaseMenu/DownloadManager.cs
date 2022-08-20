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
        StartCoroutine(InitializeFibaseAgain());
    }

    private IEnumerator InitializeFibaseAgain()
    {
        yield return new WaitForSeconds(0.5f);
        firebaseMenuManager.FireBaseManager.InitializeFirebaseRealTimeDataBase();
    }
}
