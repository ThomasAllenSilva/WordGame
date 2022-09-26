using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class InterstitialAD : MonoBehaviour
{
    private RewardedAd interstitialAD;

    private void Awake()
    {
        if (DataManager.Instance.GameDataManager.HasBuyedNoAds)
        {
            Destroy(this.gameObject);
        }

        else
        {
            MobileAds.Initialize(initStatus => { });

            GameManager.Instance.LevelManager.onLevelCompleted += CheckIfCanShowAd;
        }
    }

    private void CheckIfCanShowAd()
    {
        if(GameManager.Instance.DataManager.PlayerDataManager.CurrentGameLevel % 3 == 0)
        {
            StartCoroutine(RequestInterstitialAD());
        }
    }

    private IEnumerator RequestInterstitialAD()
    {
        string AdId = "ca-app-pub-8786657835012152/3022239537";

        interstitialAD = new RewardedAd(AdId);

        interstitialAD.LoadAd(CreateNewInterstitialAdRequest());

        while (!interstitialAD.IsLoaded())
        {
            yield return new WaitForSeconds(0.1f);       
        }

        yield return new WaitForSeconds(0.3f);
        interstitialAD.Show();
    }

    private AdRequest CreateNewInterstitialAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

    private void OnDestroy()
    {
        GameManager.Instance.LevelManager.onLevelCompleted -= CheckIfCanShowAd;
    }
}
