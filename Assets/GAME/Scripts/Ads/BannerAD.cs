using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAD : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsShowListener
{
    public const string gameID = "4893961";

    public void OnInitializationComplete()
    {
        ShowBannerAD();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Application.Quit();
    }

    public void ShowBannerAD()
    {
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show("Banner_Android");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Application.Quit();
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
}
