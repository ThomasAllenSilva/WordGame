using UnityEngine;

public class FirebaseMenuManager : MonoBehaviour
{
    public FirebaseRealTimeDataBaseReader FirebaseRealTimeDataBaseReader { get; private set; }

    public DownloadManager DownloadManager { get; private set; }

    public static FirebaseMenuManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
        }

        Application.targetFrameRate = 60;

        FirebaseRealTimeDataBaseReader = GetComponentInChildren<FirebaseRealTimeDataBaseReader>(true);
        DownloadManager = GetComponentInChildren<DownloadManager>();
    }
}
