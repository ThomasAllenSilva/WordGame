using UnityEngine;

public class FirebaseMenuManager : MonoBehaviour
{
    public FirebaseManager FireBaseManager { get; private set; }

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

        FireBaseManager = GetComponentInChildren<FirebaseManager>(true);
        DownloadManager = GetComponentInChildren<DownloadManager>();
    }
}
