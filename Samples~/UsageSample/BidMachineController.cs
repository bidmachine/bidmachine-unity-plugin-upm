using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.UI;
using BidMachineInc.Ads.Api;
using BidMachineInc.Ads.Common;

[SuppressMessage("ReSharper", "CheckNamespace")]
public class BidMachineController : MonoBehaviour
{
    public Toggle tgTesting;

    public Toggle tgLogging;

    private TargetingParams _targetingParams;
    private PriceFloorParams _priceFloorParams;
    private CustomParams _customParams;

    private BannerView _bannerView;
    private readonly IAdListener<IBannerView> _bannerListener = new BannerListener();
    private IBannerRequest _bannerRequest;
    private readonly IAdAuctionRequestListener _bannerRequestListener = new BannerRequestListener();

    private InterstitialAd _interstitialAd;
    private readonly IInterstitialAdListener _interstitialListener = new InterstitialListener();
    private IAdRequest _interstitialRequest;
    private readonly IAdAuctionRequestListener _interstitialRequestListener = new InterstitialRequestListener();

    private RewardedAd _rewardedAd;
    private readonly IRewardedAdListener _rewardedListener = new RewardedAdListener();
    private IAdRequest _rewardedRequest;
    private readonly IAdAuctionRequestListener _rewardedRequestListener = new RewardedRequestListener();

    private void Start()
    {
        tgTesting.isOn = true;
        tgLogging.isOn = true;

        _priceFloorParams = new PriceFloorParams();
        _priceFloorParams.AddPriceFloor(Guid.NewGuid().ToString(), 0.01);

        _customParams = new CustomParams();
        _customParams.AddParam("mediation_mode", "pdb_is");

        _targetingParams = new TargetingParams
        {
            UserId = "UserId",
            UserGender = TargetingParams.Gender.Female,
            BirthdayYear = 1990,
            Keywords = new[] { "keyword1", "keyword1" },
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
            StoreSubCategories = new[] { "SubCategory1", "SubCategory2" },
            IsPaid = true,
            ExternalUserIds = new ExternalUserId[]
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
        BidMachine.SetEndpoint("https://test.com");
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

    private void LoadBanner(BannerAdSize bannerSize)
    {
        if (_bannerRequest != null) return;

        var config = AdPlacementConfig.BannerBuilder(bannerSize)
            .WithPlacementId("placement_bannerRequest")
            .WithCustomParams(_customParams)
            .Build();

        _bannerRequest = (IBannerRequest)new BannerRequest.Builder(config)
            .SetPriceFloorParams(_priceFloorParams)
            .SetTargetingParams(_targetingParams)
            .SetLoadingTimeOut(10 * 1000)
            // .SetBidPayload("123")
            // .SetNetworks("admob")
            .SetListener(_bannerRequestListener)
            .Build();

        if (_bannerView != null)
        {
            _bannerView.SetListener(null);
            _bannerView.Destroy();
            _bannerView = null;
        }

        _bannerView = new BannerView();
        _bannerView.SetListener(_bannerListener);
        _bannerView.Load(_bannerRequest);
    }

    public void ShowBannerView()
    {
        var size = _bannerRequest.GetBannerAdSize();
        _bannerView?.Show(
            BidMachine.BannerVerticalBottom,
            BidMachine.BannerHorizontalCenter,
            _bannerView,
            size
        );
    }

    public void DestroyBanner()
    {
        if (_bannerView == null) return;

        _bannerView.SetListener(null);
        _bannerView.Destroy();
        _bannerView = null;
        _bannerRequest = null;
    }

    #endregion

    #region Interstitial Ad

    public void LoadInterstitialAd()
    {
        if (_interstitialRequest != null) return;

        var config = AdPlacementConfig.InterstitialBuilder(AdContentType.All)
            .WithPlacementId("placement_interstitialRequest")
            .WithCustomParams(_customParams)
            .Build();

        _interstitialRequest = new InterstitialRequest.Builder(config)
            .SetPriceFloorParams(_priceFloorParams)
            .SetTargetingParams(_targetingParams)
            .SetLoadingTimeOut(10 * 1000)
            // .SetBidPayload("123")
            // .SetNetworks("admob")
            .SetListener(_interstitialRequestListener)
            .Build();

        if (_interstitialAd != null)
        {
            _interstitialAd.SetListener(null);
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        _interstitialAd = new InterstitialAd();
        _interstitialAd.SetListener(_interstitialListener);
        _interstitialAd.Load(_interstitialRequest);
    }

    public void ShowInterstitialAd()
    {
        if (_interstitialAd == null || !_interstitialAd.CanShow()) return;

        _interstitialAd.Show();
    }

    public void DestroyInterstitial()
    {
        if (_interstitialAd == null) return;

        _interstitialAd.SetListener(null);
        _interstitialAd.Destroy();
        _interstitialAd = null;
        _interstitialRequest = null;
    }

    #endregion

    #region Rewarded Video Ad

    public void LoadRewardedAd()
    {
        if (_rewardedRequest != null) return;

        var config = AdPlacementConfig.RewardedBuilder(AdContentType.Video)
            .WithPlacementId("placement_rewardedRequest")
            .WithCustomParams(_customParams)
            .Build();

        _rewardedRequest = new RewardedRequest.Builder(config)
            .SetPriceFloorParams(_priceFloorParams)
            .SetTargetingParams(_targetingParams)
            .SetLoadingTimeOut(10 * 1000)
            // .SetBidPayload("123")
            // .SetNetworks("admob")
            .SetListener(_rewardedRequestListener)
            .Build();

        if (_rewardedAd != null)
        {
            _rewardedAd.SetListener(null);
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        _rewardedAd = new RewardedAd();
        _rewardedAd.SetListener(_rewardedListener);
        _rewardedAd.Load(_rewardedRequest);
    }

    public void ShowRewardedAd()
    {
        if (_rewardedAd == null || !_rewardedAd.CanShow()) return;

        _rewardedAd.Show();
    }

    public void DestroyRewardedVideo()
    {
        if (_rewardedAd == null) return;

        _rewardedAd.SetListener(null);
        _rewardedAd.Destroy();
        _rewardedAd = null;
        _rewardedRequest = null;
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
            Debug.Log("BidMachine: BannerView: OnAdLoadFailed");
        }

        public void onAdShowFailed(IBannerView ad, BMError error)
        {
            Debug.Log("BidMachine: BannerView: OnAdShowFailed");
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
            Debug.Log("BidMachine: BannerRequest: OnAdRequestFailed");
        }

        public void onRequestSuccess(IAdRequest request, AuctionResult auctionResult)
        {
            Debug.Log("BidMachine: BannerRequest: OnAdRequestSuccess");
        }
    }

    private class InterstitialListener : IInterstitialAdListener
    {
        public void onAdClosed(IInterstitialAd ad, bool finished)
        {
            Debug.Log("BidMachine: InterstitialAd: OnAdClosed");
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
            Debug.Log("BidMachine: InterstitialAd: OnAdLoadFailed");
        }

        public void onAdShowFailed(IInterstitialAd ad, BMError error)
        {
            Debug.Log("BidMachine: InterstitialAd: OnAdShowFailed");
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
            Debug.Log("BidMachine: InterstitialRequest: OnAdRequestFailed");
        }

        public void onRequestSuccess(IAdRequest request, AuctionResult auctionResult)
        {
            Debug.Log("BidMachine: InterstitialRequest: OnAdRequestSuccess");
        }
    }

    private class RewardedAdListener : IRewardedAdListener
    {
        public void onAdClosed(IRewardedAd ad, bool finished)
        {
            Debug.Log("BidMachine: RewardedAd: OnAdClosed");
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
            Debug.Log("BidMachine: RewardedAd: OnAdLoadFailed");
        }

        public void onAdShowFailed(IRewardedAd ad, BMError error)
        {
            Debug.Log("BidMachine: RewardedAd: OnAdShowFailed");
        }

        public void onAdShown(IRewardedAd ad)
        {
            Debug.Log("BidMachine: RewardedAd: OnAdShown");
        }

        public void onAdRewarded(IRewardedAd ad)
        {
            Debug.Log("BidMachine: RewardedAd: OnAdRewarded");
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
            Debug.Log("BidMachine: RewardedRequest: OnAdRequestFailed");
        }

        public void onRequestSuccess(IAdRequest request, AuctionResult auctionResult)
        {
            Debug.Log("BidMachine: RewardedRequest: OnAdRequestSuccess");
        }
    }

    #endregion
}
