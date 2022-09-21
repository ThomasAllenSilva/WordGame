using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class InterstitialAD : MonoBehaviour
{
    private RewardedAd interstitialAD;

    private void Start()
    {
        MobileAds.Initialize(initStatus => { });

        StartCoroutine(RequestInterstitialAD());
    }

    public IEnumerator RequestInterstitialAD()
    {
        string AdId = "ca-app-pub-3940256099942544/1033173712";

        interstitialAD = new RewardedAd(AdId);

        interstitialAD.LoadAd(CreateNewInterstitialAdRequest());

        while (!interstitialAD.IsLoaded())
        {
            yield return new WaitForSeconds(0.2f);
          
        }
        interstitialAD.Show();
    }

    private AdRequest CreateNewInterstitialAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

}
