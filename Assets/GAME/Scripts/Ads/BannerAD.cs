using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAD : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsShowListener
{
    public const string gameID = "4893961";

    private static BannerAD Instance;

    private void Awake()
    {


        if (Instance == null)
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
    }

    public void OnInitializationComplete()
    {
        ShowBannerAD();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {

    }

    public void ShowBannerAD()
    {
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show("Banner_Android");
      
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
       
    }

    public void OnUnityAdsShowStart(string placementId)
    {

    }

    public void OnUnityAdsShowClick(string placementId)
    {

    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {

    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 2 || level == 3)
        {
            ShowBannerAD();
        }

        else Advertisement.Banner.Hide();
    }
}
