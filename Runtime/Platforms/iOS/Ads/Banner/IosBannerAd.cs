#if UNITY_IOS || BIDMACHINE_DEV
using System;
using AOT;
using BidMachineInc.Ads.Api;
using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.Ios
{
    public class IosBannerAd : IosAd<BannerAdIosUnityBridge>, IBannerView
    {
        private static IAdListener<IBannerView> _listener;

        public void SetListener(IAdListener<IBannerView> listener)
        {
            _listener = listener;

            AdBridge.SetLoadCallback(DidLoadAd);
            AdBridge.SetLoadFailedCallback(DidFailToLoadAd);
            AdBridge.SetPresentCallback(DidPresentAd);
            AdBridge.SetPresentFailedCallback(DidFailToPresentAd);
            AdBridge.SetImpressionCallback(DidReceiveAdImpression);
            AdBridge.SetExpiredCallback(DidExpire);
        }

        public bool Show(int yAxis, int xAxis, IBannerView ad, BannerSize size)
        {
            return AdBridge.Show(yAxis, xAxis);
        }

        public bool Show(int yAxis, int xAxis, IBannerView ad, BannerAdSize size)
        {
            return AdBridge.Show(yAxis, xAxis);
        }

        public void Hide()
        {
            AdBridge.Hide();
        }

        public BannerAdSize GetAdSize()
        {
            return AdBridge.GetBannerAdSize();
        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void DidLoadAd(IntPtr ad)
        {
            _listener?.onAdLoaded(new IosBannerAd());
        }

        [MonoPInvokeCallback(typeof(AdFailureCallback))]
        private static void DidFailToLoadAd(IntPtr ad, IntPtr error)
        {
            if (_listener == null) return;

            var bmError = new BMError
            {
                Code = IosErrorBridge.GetErrorCode(error),
                Message = IosErrorBridge.GetErrorMessage(error)
            };

            _listener.onAdLoadFailed(new IosBannerAd(), bmError);
        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void DidPresentAd(IntPtr ad)
        {
            _listener?.onAdShown(new IosBannerAd());
        }

        [MonoPInvokeCallback(typeof(AdFailureCallback))]
        private static void DidFailToPresentAd(IntPtr ad, IntPtr error)
        {
            if (_listener == null) return;

            var bmError = new BMError
            {
                Code = IosErrorBridge.GetErrorCode(error),
                Message = IosErrorBridge.GetErrorMessage(error)
            };

            _listener.onAdShowFailed(new IosBannerAd(), bmError);
        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void DidReceiveAdImpression(IntPtr ad)
        {
            _listener?.onAdImpression(new IosBannerAd());
        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void DidExpire(IntPtr ad)
        {
            _listener?.onAdExpired(new IosBannerAd());
        }
    }
}
#endif
