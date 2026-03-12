using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads
{
    internal static class BidMachineClientFactory
    {
        internal static IBidMachine GetBidMachine()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return new Android.AndroidBidMachine();
#elif UNITY_IOS && !UNITY_EDITOR
            return new Ios.IosBidMachine();
#else
            return new Dummy.DummyBidMachine();
#endif
        }

        internal static IBannerView GetBannerView()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return new Android.AndroidBannerView();
#elif UNITY_IOS && !UNITY_EDITOR
            return new Ios.IosBannerAd();
#else
            return new Dummy.DummyBannerAd();
#endif
        }

        internal static IBannerRequestBuilder GetBannerRequestBuilder()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return new Android.AndroidBannerRequestBuilder();
#elif UNITY_IOS && !UNITY_EDITOR
            return new Ios.IosBannerRequestBuilder();
#else
            return new Dummy.DummyBannerRequestBuilder();
#endif
        }

        internal static IInterstitialAd GetInterstitialAd()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return new Android.AndroidInterstitialAd();
#elif UNITY_IOS && !UNITY_EDITOR
            return new Ios.IosInterstitialAd();
#else
            return new Dummy.DummyInterstitialAd();
#endif
        }

        internal static IAdRequestBuilder GetInterstitialRequestBuilder()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return new Android.AndroidInterstitialRequestBuilder();
#elif UNITY_IOS && !UNITY_EDITOR
            return new Ios.IosInterstitialRequestBuilder();
#else
            return new Dummy.DummyInterstitialRequestBuilder();
#endif
        }

        internal static IRewardedAd GetRewardedAd()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return new Android.AndroidRewardedAd();
#elif UNITY_IOS && !UNITY_EDITOR
            return new Ios.IosRewardedAd();
#else
            return new Dummy.DummyRewardedAd();
#endif
        }

        internal static IAdRequestBuilder GetRewardedRequestBuilder()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return new Android.AndroidRewardedRequestBuilder();
#elif UNITY_IOS && !UNITY_EDITOR
            return new Ios.IosRewardedRequestBuilder();
#else
            return new Dummy.DummyRewardedRequestBuilder();
#endif
        }
    }
}
