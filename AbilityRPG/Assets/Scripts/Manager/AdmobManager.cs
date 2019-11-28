using System;
using UnityEngine;
using GoogleMobileAds.Api;

// https://devparklibrary.tistory.com/6
public class AdmobManager : MonoBehaviour
{
    private BannerView banner;

    void Start()
    {
#if UNITY_ANDROID
        string appId = "ca-app-pub-9788866295273918~2028325364";
#else
        string appId = "unexpected_platform";
#endif
        MobileAds.Initialize(appId);
        this.RequestBanner();
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
        string AdUnitId = "ca-app-pub-9788866295273918/9638727232";
        //string AdUnitId = "ca-app-pub-3940256099942544/6300978111";
#else
        string AdUnitId = "unDefind";
#endif
        banner = new BannerView(AdUnitId, AdSize.SmartBanner, AdPosition.Top);

        // Called when an ad request has successfully loaded.
        banner.OnAdLoaded += HandleOnAdLoaded_banner;
        // Called when an ad request failed to load.
        banner.OnAdFailedToLoad += HandleOnAdFailedToLoad_banner;
        // Called when an ad is clicked.
        banner.OnAdOpening += HandleOnAdOpened_banner;
        // Called when the user returned from the app after an ad click.
        banner.OnAdClosed += HandleOnAdClosed_banner;
        // Called when the ad click caused the user to leave the application.
        banner.OnAdLeavingApplication += HandleOnAdLeavingApplication_banner;

        //AdRequest request = new AdRequest.Builder().Build();

        AdRequest request = new AdRequest.Builder().
            AddTestDevice(AdRequest.TestDeviceSimulator).
            AddTestDevice("D1CEC1EC30FD8678").Build();

        banner.LoadAd(request);
    }

    public void ShowBanner()
    {
        banner.Show();
    }

    public void HideBanner()
    {
        banner.Hide();
    }

    public void HandleOnAdLoaded_banner(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received_banner");
    }

    public void HandleOnAdFailedToLoad_banner(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd_banner event received with message: "
                            + args.Message);
    }

    public void HandleOnAdOpened_banner(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received_banner");
    }

    public void HandleOnAdClosed_banner(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received_banner");
    }

    public void HandleOnAdLeavingApplication_banner(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received_banner");
    }
}
