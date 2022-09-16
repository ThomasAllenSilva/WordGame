using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAD : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsShowListener
{
    public const string gameID = "4893961";

    private static BannerAD Instance;

    private void Awake()
    {
        if (DataManager.Instance.GameDataManager.HasBuyedNoAds)
        {
            Destroy(this.gameObject);
        }

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

    private void Start() => ScenesManager.Instance.onSceneLoaded += OnSceneWasLoaded;

    private void OnSceneWasLoaded()
    {
        int sceneIndex = ScenesManager.Instance.CurrentSceneIndex;

        if (sceneIndex == 2 || sceneIndex == 3)
        {
            ShowBannerAD();
        }

        else Advertisement.Banner.Hide();
    }

    public void ShowBannerAD()
    {
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show("Banner_Android");
    }

    public void OnInitializationComplete()
    {
        
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {

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

    private void OnDestroy()
    {
        ScenesManager.Instance.onSceneLoaded -= OnSceneWasLoaded;
    }
}
