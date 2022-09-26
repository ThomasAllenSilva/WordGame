using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DownloadManager : MonoBehaviour
{
    private FirebaseMenuManager firebaseMenuManager;

    [SerializeField] private GameObject internetCanvas;
    [SerializeField] private GameObject funFactsCanvas;
    [SerializeField] private Button creditsButton;

    private bool hasFinishedDownloadLevels;

    public bool CanDownloadNewContent { get; private set; }

    private void Awake()
    {
        CanDownloadNewContent = true;
        firebaseMenuManager = FirebaseMenuManager.Instance;
        firebaseMenuManager.FirebaseRealTimeDataBaseReader.onFailedToReadDataBaseValues += CancelDownload;
        firebaseMenuManager.FirebaseRealTimeDataBaseReader.onFinishedToReadDataBaseValues += FinishedDownloadLevels;
    }

    public void CancelDownload()
    {
        
        CanDownloadNewContent = false;
        if (!hasFinishedDownloadLevels)
        {
            internetCanvas.SetActive(true);
            funFactsCanvas.SetActive(false);
            creditsButton.interactable = false;
        }
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
        internetCanvas.SetActive(false);
        funFactsCanvas.SetActive(true);
        creditsButton.interactable = true;
    }

    private void FinishedDownloadLevels()
    {
        hasFinishedDownloadLevels = true;
    }

    private void OnDestroy()
    {
        firebaseMenuManager.FirebaseRealTimeDataBaseReader.onFailedToReadDataBaseValues -= CancelDownload;
    }
}
