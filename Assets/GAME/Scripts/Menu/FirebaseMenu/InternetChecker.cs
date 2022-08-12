using UnityEngine;

public class InternetChecker : MonoBehaviour
{
    private const bool allowCarrierDataNetwork = false;
    private const string pingAddress = "8.8.8.8"; // Google Public DNS server
    private const float waitingTime = 2.5f;

    private float pingStartTime;

    private Ping ping;

    private DownloadManager downloadManager;

    public bool InternetConnectBool { get; private set; }

    private void Start()
    {
        downloadManager = FirebaseMenuManager.Instance.DownloadManager;
        InvokeRepeating(nameof(InternetCheck), 0.5f, 0.7f);
    }

    private void InternetCheck()
    {
        bool internetPossiblyAvailable;

        switch (Application.internetReachability)
        {
            case NetworkReachability.ReachableViaLocalAreaNetwork:
                internetPossiblyAvailable = true;
                break;

            case NetworkReachability.ReachableViaCarrierDataNetwork:
                internetPossiblyAvailable = true;
                break;

            default:
                internetPossiblyAvailable = false;
                break;
        }

        if (!internetPossiblyAvailable)
        {
            InternetIsNotAvailable();
            return;
        }

        ping = new Ping(pingAddress);
        pingStartTime = Time.time;
        
    }

    private void Update()
    {
        if (ping != null)
        {
            bool stopCheck = true;
            if (ping.isDone)
                InternetAvailable();

            else if (Time.time - pingStartTime < waitingTime)
                stopCheck = false;

            else
                InternetIsNotAvailable();

            if (stopCheck)
                ping = null;
        }
    }

    private void InternetIsNotAvailable()
    {
        InternetConnectBool = false;

        downloadManager.CancelDownload();
    }

    private void InternetAvailable()
    {
        InternetConnectBool = true;

        downloadManager.ContinueDownload();
    }
}