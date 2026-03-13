using System;
using System.Collections.Generic;
using BidMachineInc.Ads.Api;
using BidMachineInc.Ads.Common;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

#pragma warning disable 649

public class BidMachineController : MonoBehaviour
{
    public Toggle tgTesting;

    public Toggle tgLogging;

    private TargetingParams targetingParams;
    private PriceFloorParams priceFloorParams;
    private CustomParams customParams;

    private BannerView bannerView;
    private readonly IAdListener<IBannerView> bannerListener = new BannerListener();
    private IBannerRequest bannerRequest;
    private readonly IAdAuctionRequestListener bannerRequestListener = new BannerRequestListener();

    private InterstitialAd interstitialAd;
    private readonly IInterstitialAdListener interstitialListener =
        new InterstitialListener();
    private IAdRequest interstitialRequest;
    private readonly IAdAuctionRequestListener interstitialRequestListener =
        new InterstitialRequestListener();

    private RewardedAd rewardedAd;
    private readonly IRewardedAdListener rewardedListener = new RewardedAdListener();
    private IAdRequest rewardedRequest;
    private readonly IAdAuctionRequestListener rewardedRequestListener = new RewardedRequestListener();

    private void Start()
    {
        tgTesting.isOn = true;
        tgLogging.isOn = true;

        priceFloorParams = new PriceFloorParams();
        priceFloorParams.AddPriceFloor(Guid.NewGuid().ToString(), 0.01);

        customParams = new CustomParams();
        customParams.AddParam("mediation_mode", "pdb_is");

        targetingParams = new TargetingParams
        {
            UserId = "UserId",
            gender = TargetingParams.Gender.Female,
            BirthdayYear = 1990,
            Keywords = new string[] { "keyword1", "keyword1" },
            DeviceLocation = new TargetingParams.Location
            {
                Provider = "GPS",
                Latitude = 43.9006,
                Longitude = 27.5590
            },
            Country = "Country",
            City = "City",
            Zip = "zip_code",
            StoreUrl = "https://play.google.com/store/apps/details?id=com.example.app",
            StoreCategory = "Category",
            StoreSubCategories = new string[] { "SubCategory1", "SubCategory2" },
            IsPaid = true,
            externalUserIds = new ExternalUserId[]
            {
                new() { SourceId = "ad_network_1", Value = "value_1" },
                new() { SourceId = "ad_network_2", Value = "value_2" }
            },
            BlockedDomains = new HashSet<string> { "domain1.com", "domain2.com" },
            BlockedCategories = new HashSet<string> { "category1", "category2" },
            BlockedApplications = new HashSet<string> { "com.unwanted.app1", "com.unwanted.app2" }
        };
    }

    public void BidMachineInitialize()
    {
        // BidMachine.SetTargetingParams(targetingParams);
        // BidMachine.SetPublisher(
        //     new Publisher
        //     {
        //         Id = "1",
        //         Name = "AdTest",
        //         Domain = "us",
        //         Categories = new[] { "sports", "technology" }
        //     }
        // );
        // BidMachine.SetEndpoint("https://test.com");
        BidMachine.SetSubjectToGDPR(true);
        BidMachine.SetCoppa(true);
        BidMachine.SetConsentConfig(true, "test consent string");
        BidMachine.SetUSPrivacyString("test_string");
        BidMachine.SetLoggingEnabled(tgLogging.isOn);
        BidMachine.SetTestMode(tgTesting.isOn);
        BidMachine.Initialize("122");
    }

    public void IsInitialized()
    {
        Debug.Log($"isInitialized: {BidMachine.IsInitialized()}");
    }

    #region Banner & MREC Ads

    public void LoadBanner()
    {
        LoadBanner(BannerAdSize.Banner);
    }

    public void LoadMrec()
    {
        LoadBanner(BannerAdSize.MediumRectangle);
    }

    public void LoadBanner(BannerAdSize bannerSize)
    {
        if (bannerRequest != null)
        {
            return;
        }

        var config = AdPlacementConfig.BannerBuilder(bannerSize)
            .WithPlacementId("placement_bannerRequest")
            .WithCustomParams(customParams)
            .Build();

        bannerRequest = (IBannerRequest)new BannerRequest.Builder(config)
            .SetPriceFloorParams(priceFloorParams)
            .SetTargetingParams(targetingParams)
            .SetLoadingTimeOut(10 * 1000)
            // .SetBidPayload("123")
            // .SetNetworks("admob")
            .SetListener(bannerRequestListener)
            .Build();

        if (bannerView != null)
        {
            bannerView.SetListener(null);
            bannerView.Destroy();
            bannerView = null;
        }

        bannerView = new BannerView();
        bannerView.SetListener(bannerListener);
        bannerView.Load(bannerRequest);
    }

    public void ShowBannerView()
    {
        var size = bannerRequest.GetBannerAdSize();
        bannerView?.Show(
            BidMachine.BannerVerticalBottom,
            BidMachine.BannerHorizontalCenter,
            bannerView,
            size
        );
    }

    public void DestroyBanner()
    {
        if (bannerView == null)
        {
            return;
        }

        bannerView.SetListener(null);
        bannerView.Destroy();
        bannerView = null;
        bannerRequest = null;
    }

    #endregion

    #region Interstitial Ad

    public void LoadInterstitialAd()
    {
        if (interstitialRequest != null)
        {
            return;
        }

        var config = AdPlacementConfig.InterstitialBuilder(AdContentType.All)
            .WithPlacementId("placement_interstitialRequest")
            .WithCustomParams(customParams)
            .Build();

        interstitialRequest = new InterstitialRequest.Builder(config)
            .SetPriceFloorParams(priceFloorParams)
            .SetTargetingParams(targetingParams)
            .SetLoadingTimeOut(10 * 1000)
            // .SetBidPayload("123")
            // .SetNetworks("admob")
            .SetListener(interstitialRequestListener)
            .Build();

        if (interstitialAd != null)
        {
            interstitialAd.SetListener(null);
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        interstitialAd = new InterstitialAd();
        interstitialAd.SetListener(interstitialListener);
        interstitialAd.Load(interstitialRequest);
    }

    public void ShowInterstitialAd()
    {
        if (interstitialAd == null || !interstitialAd.CanShow())
        {
            return;
        }

        interstitialAd.Show();
    }

    public void DestroyInterstitial()
    {
        if (interstitialAd == null)
        {
            return;
        }

        interstitialAd.SetListener(null);
        interstitialAd.Destroy();
        interstitialAd = null;
        interstitialRequest = null;
    }

    #endregion

    #region Rewarded Video Ad

    public void LoadRewardedAd()
    {
        if (rewardedRequest != null)
        {
            return;
        }

        var config = AdPlacementConfig.RewardedBuilder(AdContentType.Video)
            .WithPlacementId("placement_rewardedRequest")
            .WithCustomParams(customParams)
            .Build();

        rewardedRequest = new RewardedRequest.Builder(config)
            .SetPriceFloorParams(priceFloorParams)
            .SetTargetingParams(targetingParams)
            .SetLoadingTimeOut(10 * 1000)
            // .SetBidPayload("123")
            // .SetNetworks("admob")
            .SetListener(rewardedRequestListener)
            .Build();

        if (rewardedAd != null)
        {
            rewardedAd.SetListener(null);
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        rewardedAd = new RewardedAd();
        rewardedAd.SetListener(rewardedListener);
        rewardedAd.Load(rewardedRequest);
    }

    public void ShowRewardedAd()
    {
        if (rewardedAd == null || !rewardedAd.CanShow())
        {
            return;
        }

        rewardedAd.Show();
    }

    public void DestroyRewardedVideo()
    {
        if (rewardedAd == null)
        {
            return;
        }

        rewardedAd.SetListener(null);
        rewardedAd.Destroy();
        rewardedAd = null;
        rewardedRequest = null;
    }

    #endregion

    #region Listeners

    private class BannerListener : IAdListener<IBannerView>
    {
        public void onAdExpired(IBannerView ad)
        {
            Debug.Log("BidMachine: BannerAd - OnAdExpired");
        }

        public void onAdImpression(IBannerView ad)
        {
            Debug.Log("BidMachine: BannerAd - OnAdImpression");
        }

        public void onAdLoaded(IBannerView ad)
        {
            Debug.Log("BidMachine: BannerView: OnAdLoaded");
        }

        public void onAdLoadFailed(IBannerView ad, BMError error)
        {
            Debug.Log($"BidMachine: BannerView: OnAdLoadFailed");
        }

        public void onAdShowFailed(IBannerView ad, BMError error)
        {
            Debug.Log($"BidMachine: BannerView: OnAdShowFailed");
        }

        public void onAdShown(IBannerView ad)
        {
            Debug.Log("BidMachine: BannerView: OnAdShown");
        }
    }

    private class BannerRequestListener : IAdAuctionRequestListener
    {
        public void onRequestExpired(IAdRequest request)
        {
            Debug.Log("BidMachine: BannerRequest: OnAdRequestExpired");
        }

        public void onRequestFailed(IAdRequest request, BMError error)
        {
            Debug.Log($"BidMachine: BannerRequest: OnAdRequestFailed");
        }

        public void onRequestSuccess(IAdRequest request, AuctionResult auctionResult)
        {
            Debug.Log($"BidMachine: BannerRequest: OnAdRequestSuccess");
        }
    }

    private class InterstitialListener : IInterstitialAdListener
    {
        public void onAdClosed(IInterstitialAd ad, bool finished)
        {
            Debug.Log($"BidMachine: InterstitialAd: OnAdClosed");
        }

        public void onAdExpired(IInterstitialAd ad)
        {
            Debug.Log("BidMachine: InterstitialAd: OnAdExpired");
        }

        public void onAdImpression(IInterstitialAd ad)
        {
            Debug.Log("BidMachine: InterstitialAd: OnAdImpression");
        }

        public void onAdLoaded(IInterstitialAd ad)
        {
            Debug.Log("BidMachine: InterstitialAd: OnAdLoaded");
        }

        public void onAdLoadFailed(IInterstitialAd ad, BMError error)
        {
            Debug.Log($"BidMachine: InterstitialAd: OnAdLoadFailed");
        }

        public void onAdShowFailed(IInterstitialAd ad, BMError error)
        {
            Debug.Log($"BidMachine: InterstitialAd: OnAdShowFailed");
        }

        public void onAdShown(IInterstitialAd ad)
        {
            Debug.Log("BidMachine: InterstitialAd: OnAdShown");
        }
    }

    private class InterstitialRequestListener : IAdAuctionRequestListener
    {
        public void onRequestExpired(IAdRequest request)
        {
            Debug.Log("BidMachine: InterstitialRequest: OnAdRequestExpired");
        }

        public void onRequestFailed(IAdRequest request, BMError error)
        {
            Debug.Log($"BidMachine: InterstitialRequest: OnAdRequestFailed");
        }

        public void onRequestSuccess(IAdRequest request, AuctionResult auctionResult)
        {
            Debug.Log($"BidMachine: InterstitialRequest: OnAdRequestSuccess");
        }
    }

    private class RewardedAdListener : IRewardedAdListener
    {
        public void onAdClosed(IRewardedAd ad, bool finished)
        {
            Debug.Log($"BidMachine: RewardedAd: OnAdClosed");
        }

        public void onAdExpired(IRewardedAd ad)
        {
            Debug.Log("BidMachine: RewardedAd: OnAdExpired");
        }

        public void onAdImpression(IRewardedAd ad)
        {
            Debug.Log("BidMachine: RewardedAd: OnAdImpression");
        }

        public void onAdLoaded(IRewardedAd ad)
        {
            Debug.Log("BidMachine: RewardedAd: OnAdLoaded");
        }

        public void onAdLoadFailed(IRewardedAd ad, BMError error)
        {
            Debug.Log($"BidMachine: RewardedAd: OnAdLoadFailed");
        }

        public void onAdShowFailed(IRewardedAd ad, BMError error)
        {
            Debug.Log($"BidMachine: RewardedAd: OnAdShowFailed");
        }

        public void onAdShown(IRewardedAd ad)
        {
            Debug.Log("BidMachine: RewardedAd: OnAdShown");
        }

        public void onAdRewarded(IRewardedAd ad)
        {
            Debug.Log($"BidMachine: RewardedAd: OnAdRewarded");
        }
    }

    private class RewardedRequestListener : IAdAuctionRequestListener
    {
        public void onRequestExpired(IAdRequest request)
        {
            Debug.Log("BidMachine: RewardedRequest: OnAdRequestExpired");
        }

        public void onRequestFailed(IAdRequest request, BMError error)
        {
            Debug.Log($"BidMachine: RewardedRequest: OnAdRequestFailed");
        }

        public void onRequestSuccess(IAdRequest request, AuctionResult auctionResult)
        {
            Debug.Log($"BidMachine: RewardedRequest: OnAdRequestSuccess");
        }
    }

    #endregion
}
