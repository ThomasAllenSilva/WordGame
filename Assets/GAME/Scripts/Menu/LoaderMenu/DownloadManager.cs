using UnityEngine;

public class DownloadManager : MonoBehaviour
{
    private LoaderMenuManager menuManager;

    [SerializeField] private GameObject internetCanvas;

    public bool CanDownloadNewContent { get; private set; }

    private void Awake()
    {
        menuManager = LoaderMenuManager.Instance;
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

    public async void TryToDownloadLevels()
    {
        await System.Threading.Tasks.Task.Delay(500);
        menuManager.DataBaseController.InitializeDataBase();
    }
}
