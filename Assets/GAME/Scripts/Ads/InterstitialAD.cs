using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAD : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsShowListener
{
    public const string gameID = "4893961";

    private void Awake()
    {
        GameManager.Instance.LevelManager.onLevelCompleted += ShowInterstitialAD;    
    }

    private bool CheckIfCanShowInterstitialAD()
    {
       if(DataManager.Instance.PlayerDataManager.PlayerData.currentGameLevel % DataManager.Instance.PlayerDataManager.PlayerData.currentGameLevel == 0)
       {
            return true;
       }

        else
        {
            return false;
        }
    }

    public void ShowInterstitialAD()
    {
        if (CheckIfCanShowInterstitialAD())
        {
            Advertisement.Initialize(gameID, true, this);
        }

        else
        {
            Debug.Log("Not level to show yet");
            return;
        }
    }

    public void OnInitializationComplete()
    {
        Advertisement.Show("Interstitial_Android", this);
        Debug.Log("Showing Unity AD");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Application.Quit();
    }

    public void OnUnityAdsShowClick(string placementId)
    {

    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Application.Quit();
    }

    public void OnUnityAdsShowStart(string placementId)
    {

    }
}
