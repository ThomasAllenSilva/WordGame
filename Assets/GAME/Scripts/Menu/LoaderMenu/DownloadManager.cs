using System.Collections;
using UnityEngine;

public class DownloadManager : MonoBehaviour
{
    private LoaderMenuManager loaderMenuManager;

    [SerializeField] private GameObject internetCanvas;

    public bool CanDownloadNewContent { get; private set; }

    private void Awake()
    {
        loaderMenuManager = LoaderMenuManager.Instance;
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
        loaderMenuManager.DataBaseController.InitializeFirebaseRealTimeDataBase();
    }
}
