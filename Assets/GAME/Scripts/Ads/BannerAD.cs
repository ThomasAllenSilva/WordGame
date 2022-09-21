using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class BannerAD : MonoBehaviour
{
    private BannerView bannerView;
    
    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        RequestBanner();
    }

    private void RequestBanner()
    {

        string adUnitId = "ca-app-pub-3940256099942544/6300978111";


        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);


        AdRequest adRequest = new AdRequest.Builder().Build();
        
        bannerView.LoadAd(adRequest);
    }
}
