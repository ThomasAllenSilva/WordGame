using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAD : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsShowListener
{
    public const string gameID = "4893961";

    private static InterstitialAD Instance;

    private void Awake()
    {


        if(Instance == null)
        {
            Instance = this;
        }

        else 
        {
            Destroy(Instance.gameObject);
            Instance = this;
        }

        DontDestroyOnLoad(Instance.gameObject);
    }

    private void Start()
    {
        if (DataManager.Instance.GameDataManager.GameData.hasBuyedNoAds)
        {
            Destroy(this.gameObject);
        }

        InitializeInterstitialAD();
    }

    public void InitializeInterstitialAD()
    {
        Advertisement.Initialize(gameID, true, this);  
    }

    private bool CheckIfCanShowInterstitialAD()
    {
       if(DataManager.Instance.PlayerDataManager.PlayerData.currentGameLevel % 3 == 0)
       {
            return true;
       }

        else
        {
            return false;
        }
    }


    public void OnInitializationComplete()
    {

    }

    private void OnLevelCompleted()
    {
        if (CheckIfCanShowInterstitialAD())
        {
            Advertisement.Show("Interstitial_Android", this);
        }
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("Ads Initialization Failed: " + error);
    }

    public void OnUnityAdsShowClick(string placementId)
    {

    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {

    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("Ads show failure: " + error);
    }

    public void OnUnityAdsShowStart(string placementId)
    {
 
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LevelManager.onLevelCompleted -= InitializeInterstitialAD;
            GameManager.Instance.LevelManager.onLevelCompleted -= OnLevelCompleted;
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if(level == 2 || level == 3)
        {
            GameManager.Instance.LevelManager.onLevelCompleted -= OnLevelCompleted;
            GameManager.Instance.LevelManager.onLevelCompleted += OnLevelCompleted;
        }
    }
}
