#if (!UNITY_ANDROID && !UNITY_IOS) || BIDMACHINE_DEV
using UnityEngine;
using BidMachineInc.Ads.Api;
using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.Dummy
{
    internal static class Consts
    {
        internal const string DummyMessage = "on not supported platform. To test advertising, install your application on the Android/iOS device.";
    }

    public class DummyBidMachine : IBidMachine
    {
        public void Initialize(string sellerId)
        {
            Debug.LogWarning($"Call to Initialize(string) {Consts.DummyMessage}");
        }

        public bool IsInitialized()
        {
            Debug.LogWarning($"Call to IsInitialized() {Consts.DummyMessage}");
            return false;
        }

        public void SetConsentConfig(bool consent, string consentConfig)
        {
            Debug.LogWarning($"Call to SetConsentConfig(bool, string) {Consts.DummyMessage}");
        }

        public void SetCoppa(bool coppa)
        {
            Debug.LogWarning($"Call to SetCoppa(bool) {Consts.DummyMessage}");
        }

        public void SetEndpoint(string url)
        {
            Debug.LogWarning($"Call to SetEndpoint(string) {Consts.DummyMessage}");
        }

        public void SetGPP(string gppString, int[] gppIds)
        {
            Debug.LogWarning($"Call to SetGPP(string, int[]) {Consts.DummyMessage}");
        }

        public void SetLoggingEnabled(bool logging)
        {
            Debug.LogWarning($"Call to SetLoggingEnabled(bool) {Consts.DummyMessage}");
        }

        public void SetPublisher(Publisher publisher)
        {
            Debug.LogWarning($"Call to SetPublisher(Publisher) {Consts.DummyMessage}");
        }

        public void SetSubjectToGDPR(bool subjectToGdpr)
        {
            Debug.LogWarning($"Call to SetSubjectToGDPR(bool) {Consts.DummyMessage}");
        }

        public void SetTargetingParams(TargetingParams targetingParams)
        {
            Debug.LogWarning($"Call to SetTargetingParams(TargetingParams) {Consts.DummyMessage}");
        }

        public void SetTestMode(bool test)
        {
            Debug.LogWarning($"Call to SetTestMode(bool) {Consts.DummyMessage}");
        }

        public void SetUSPrivacyString(string usPrivacyString)
        {
            Debug.LogWarning($"Call to SetUSPrivacyString(string) {Consts.DummyMessage}");
        }
    }

    internal class DummyBannerAd : IBannerView
    {
        private BannerAdSize _bannerAdSize = BannerAdSize.Banner;

        public bool CanShow()
        {
            Debug.LogWarning($"Call to CanShow() {Consts.DummyMessage}");
            return false;
        }

        public void Destroy()
        {
            Debug.LogWarning($"Call to Destroy() {Consts.DummyMessage}");
        }

        public void Hide()
        {
            Debug.LogWarning($"Call to Hide() {Consts.DummyMessage}");
        }

        public void Load(IAdRequest request)
        {
            Debug.LogWarning($"Call to Load(IBannerRequest) {Consts.DummyMessage}");
        }

        public void SetListener(IAdListener<IBannerView> listener)
        {
            Debug.LogWarning($"Call to SetListener(IBannerListener) {Consts.DummyMessage}");
        }

        public bool Show(int yAxis, int xAxis, IBannerView ad, BannerSize size)
        {
            _bannerAdSize = BannerAdSize.FromLegacyBannerSize(size);
            Debug.LogWarning($"Call to Show(int, int, IBannerAd, BannerSize) {Consts.DummyMessage}");
            return false;
        }

        public bool Show(int yAxis, int xAxis, IBannerView ad, BannerAdSize size)
        {
            var bannerAdSize = size ?? BannerAdSize.Banner;
            return Show(yAxis, xAxis, ad, bannerAdSize.ToLegacyBannerSize());
        }

        public BannerAdSize GetAdSize()
        {
            return _bannerAdSize;
        }
    }

    internal class DummyBannerRequestBuilder : IBannerRequestBuilder
    {
        public IAdRequest Build()
        {
            Debug.LogWarning($"Call to Build() {Consts.DummyMessage}");
            return null;
        }

        public IAdRequestBuilder SetAdContentType(AdContentType contentType)
        {
            Debug.LogWarning($"Call to SetAdContentType(AdContentType) {Consts.DummyMessage}");
            return this;
        }

        public IAdRequestBuilder SetBidPayload(string bidPayload)
        {
            Debug.LogWarning($"Call to SetBidPayload(string) {Consts.DummyMessage}");
            return this;
        }

        public IAdRequestBuilder SetListener(IAdAuctionRequestListener listener)
        {
            Debug.LogWarning($"Call to SetListener(IAdAuctionRequestListener) {Consts.DummyMessage}");
            return this;
        }

        public IAdRequestBuilder SetLoadingTimeOut(int loadingTimeout)
        {
            Debug.LogWarning($"Call to SetLoadingTimeOut(int) {Consts.DummyMessage}");
            return this;
        }

        public IAdRequestBuilder SetNetworks(string networks)
        {
            Debug.LogWarning($"Call to SetNetworks(string) {Consts.DummyMessage}");
            return this;
        }

        public IAdRequestBuilder SetPlacementId(string placementId)
        {
            Debug.LogWarning($"Call to SetPlacementId(string) {Consts.DummyMessage}");
            return this;
        }

        public IAdRequestBuilder SetPriceFloorParams(PriceFloorParams priceFloorParams)
        {
            Debug.LogWarning($"Call to SetPriceFloorParams(PriceFloorParams) {Consts.DummyMessage}");
            return this;
        }

        public IAdRequestBuilder SetCustomParams(CustomParams customParams)
        {
            Debug.LogWarning($"Call to SetCustomParams(CustomParams) {Consts.DummyMessage}");
            return this;
        }

        public IAdRequestBuilder SetTargetingParams(TargetingParams targetingParams)
        {
            Debug.LogWarning($"Call to SetTargetingParams(TargetingParams) {Consts.DummyMessage}");
            return this;
        }

        public IAdRequestBuilder SetSize(BannerSize size)
        {
            Debug.LogWarning($"Call to SetSize(BannerSize) {Consts.DummyMessage}");
            return this;
        }

        public IAdRequestBuilder SetSize(BannerAdSize size)
        {
            Debug.LogWarning($"Call to SetSize(BannerAdSize) {Consts.DummyMessage}");
            return this;
        }
    }

    internal class DummyInterstitialAd : IInterstitialAd
    {
        public bool CanShow()
        {
            Debug.LogWarning($"Call to CanShow() {Consts.DummyMessage}");
            return false;
        }

        public void Destroy()
        {
            Debug.LogWarning($"Call to Destroy() {Consts.DummyMessage}");
        }

        public void Load(IAdRequest request)
        {
            Debug.LogWarning($"Call to Load(IInterstitialRequest) {Consts.DummyMessage}");
        }

        public void SetListener(IInterstitialAdListener listener)
        {
            Debug.LogWarning($"Call to SetListener(IInterstitialAdListener) {Consts.DummyMessage}");
        }

        public void Show()
        {
            Debug.LogWarning($"Call to Show() {Consts.DummyMessage}");
        }
    }

    internal class DummyInterstitialRequestBuilder : IAdRequestBuilder
    {
        public IAdRequest Build()
        {
            Debug.LogWarning($"Call to Build() {Consts.DummyMessage}");
            return null;
        }

        public IAdRequestBuilder SetAdContentType(AdContentType contentType)
        {
            Debug.LogWarning($"Call to SetAdContentType(AdContentType) {Consts.DummyMessage}");
            return this;
        }

        public IAdRequestBuilder SetBidPayload(string bidPayload)
        {
            Debug.LogWarning($"Call to SetBidPayload(string) {Consts.DummyMessage}");
            return this;
        }

        public IAdRequestBuilder SetListener(IAdAuctionRequestListener listener)
        {
            Debug.LogWarning($"Call to SetListener(IAdAuctionRequestListener) {Consts.DummyMessage}");
            return this;
        }

        public IAdRequestBuilder SetLoadingTimeOut(int loadingTimeout)
        {
            Debug.LogWarning($"Call to SetLoadingTimeOut(int) {Consts.DummyMessage}");
            return this;
        }

        public IAdRequestBuilder SetNetworks(string networks)
        {
            Debug.LogWarning($"Call to SetNetworks(string) {Consts.DummyMessage}");
            return this;
        }

        public IAdRequestBuilder SetPlacementId(string placementId)
        {
            Debug.LogWarning($"Call to SetPlacementId(string) {Consts.DummyMessage}");
            return this;
        }

        public IAdRequestBuilder SetPriceFloorParams(PriceFloorParams priceFloorParams)
        {
            Debug.LogWarning($"Call to SetPriceFloorParams(PriceFloorParams) {Consts.DummyMessage}");
            return this;
        }

        public IAdRequestBuilder SetCustomParams(CustomParams customParams)
        {
            Debug.LogWarning($"Call to SetCustomParams(CustomParams) {Consts.DummyMessage}");
            return this;
        }

        public IAdRequestBuilder SetTargetingParams(TargetingParams targetingParams)
        {
            Debug.LogWarning($"Call to SetTargetingParams(TargetingParams) {Consts.DummyMessage}");
            return this;
        }
    }

    internal class DummyRewardedAd : IRewardedAd
    {
        public bool CanShow()
        {
            Debug.LogWarning($"Call to CanShow() {Consts.DummyMessage}");
            return false;
        }

        public void Destroy()
        {
            Debug.LogWarning($"Call to Destroy() {Consts.DummyMessage}");
        }

        public void Load(IAdRequest request)
        {
            Debug.LogWarning($"Call to Load(IRewardedRequest) {Consts.DummyMessage}");
        }

        public void SetListener(IRewardedAdListener listener)
        {
            Debug.LogWarning($"Call to SetListener(IRewardedListener) {Consts.DummyMessage}");
        }

        public void Show()
        {
            Debug.LogWarning($"Call to Show() {Consts.DummyMessage}");
        }
    }

    internal class DummyRewardedRequestBuilder : IAdRequestBuilder
    {
        public IAdRequest Build()
        {
            Debug.LogWarning($"Call to Build() {Consts.DummyMessage}");
            return null;
        }

        public IAdRequestBuilder SetAdContentType(AdContentType contentType)
        {
            Debug.LogWarning($"Call to SetAdContentType(AdContentType) {Consts.DummyMessage}");
            return this;
        }

        public IAdRequestBuilder SetBidPayload(string bidPayload)
        {
            Debug.LogWarning($"Call to SetBidPayload(string) {Consts.DummyMessage}");
            return this;
        }

        public IAdRequestBuilder SetListener(IAdAuctionRequestListener listener)
        {
            Debug.LogWarning($"Call to SetListener(IAdAuctionRequestListener) {Consts.DummyMessage}");
            return this;
        }

        public IAdRequestBuilder SetLoadingTimeOut(int loadingTimeout)
        {
            Debug.LogWarning($"Call to SetLoadingTimeOut(int) {Consts.DummyMessage}");
            return this;
        }

        public IAdRequestBuilder SetNetworks(string networks)
        {
            Debug.LogWarning($"Call to SetNetworks(string) {Consts.DummyMessage}");
            return this;
        }

        public IAdRequestBuilder SetPlacementId(string placementId)
        {
            Debug.LogWarning($"Call to SetPlacementId(string) {Consts.DummyMessage}");
            return this;
        }

        public IAdRequestBuilder SetPriceFloorParams(PriceFloorParams priceFloorParams)
        {
            Debug.LogWarning($"Call to SetPriceFloorParams(PriceFloorParams) {Consts.DummyMessage}");
            return this;
        }

        public IAdRequestBuilder SetCustomParams(CustomParams customParams)
        {
            Debug.LogWarning($"Call to SetCustomParams(CustomParams) {Consts.DummyMessage}");
            return this;
        }

        public IAdRequestBuilder SetTargetingParams(TargetingParams targetingParams)
        {
            Debug.LogWarning($"Call to SetTargetingParams(TargetingParams) {Consts.DummyMessage}");
            return this;
        }
    }
}
#endif
