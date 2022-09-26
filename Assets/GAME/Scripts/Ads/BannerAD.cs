using UnityEngine;
using GoogleMobileAds.Api;

public class BannerAD : MonoBehaviour
{
    private BannerView bannerView;

    public void Start()
    {
        if (DataManager.Instance.GameDataManager.HasBuyedNoAds)
        {
            Destroy(this.gameObject);
        }

        else
        {
            MobileAds.Initialize(initStatus => { });

            RequestBanner();
        }
    }

    private void RequestBanner()
    {

        string adUnitId = "ca-app-pub-8786657835012152/8807266294";


        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);


        AdRequest adRequest = new AdRequest.Builder().Build();

        bannerView.LoadAd(adRequest);
    }
}
