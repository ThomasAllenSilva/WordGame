using UnityEngine;

public class InternetChecker : MonoBehaviour
{
    private const bool allowCarrierDataNetwork = false;
    private const string pingAddress = "8.8.8.8"; // Google Public DNS server
    private const float waitingTime = 2.5f;
    public bool InternetConnectBool { get; private set; }
    private Ping ping;
    private float pingStartTime;

    public static InternetChecker Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }
    public void Start()
    {
        InternetCheck();
    }
    public void InternetCheck()
    {

        Invoke("InternetCheck", 1f);

        bool internetPossiblyAvailable;

        switch (Application.internetReachability)
        {
            case NetworkReachability.ReachableViaLocalAreaNetwork:
                internetPossiblyAvailable = true;
                break;
            case NetworkReachability.ReachableViaCarrierDataNetwork:
                //internetPossiblyAvailable = allowCarrierDataNetwork;
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

    public void Update()
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

    public void InternetIsNotAvailable()
    {
        InternetConnectBool = false;
    }

    public void InternetAvailable()
    {
        InternetConnectBool = true;
    }
}