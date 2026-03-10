using System;
using AOT;
using BidMachineInc.Ads.Api;
using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.Ios
{
    public class IosInterstitialAd : IosAd<InterstitialAdIosUnityBridge>, IInterstitialAd
    {
        private static IInterstitialAdListener _listener;

        public void Show()
        {
            AdBridge.Show();
        }

        public void SetListener(IInterstitialAdListener listener)
        {
            _listener = listener;

            AdBridge.SetLoadCallback(DidLoadAd);
            AdBridge.SetLoadFailedCallback(DidFailToLoadAd);
            AdBridge.SetPresentCallback(DidPresentAd);
            AdBridge.SetPresentFailedCallback(DidFailToPresentAd);
            AdBridge.SetImpressionCallback(DidReceiveAdImpression);
            AdBridge.SetExpiredCallback(DidExpire);
            AdBridge.SetClosedCallback(DidClose);
        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void DidLoadAd(IntPtr ad)
        {
            _listener?.onAdLoaded(new IosInterstitialAd());
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

            _listener.onAdLoadFailed(new IosInterstitialAd(), bmError);
        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void DidPresentAd(IntPtr ad)
        {
            _listener?.onAdShown(new IosInterstitialAd());
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

            _listener.onAdShowFailed(new IosInterstitialAd(), bmError);
        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void DidReceiveAdImpression(IntPtr ad)
        {
            _listener?.onAdImpression(new IosInterstitialAd());
        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void DidExpire(IntPtr ad)
        {
            _listener?.onAdExpired(new IosInterstitialAd());
        }

        [MonoPInvokeCallback(typeof(AdClosedCallback))]
        private static void DidClose(IntPtr ad, bool finished)
        {
            _listener?.onAdClosed(new IosInterstitialAd(), finished);
        }
    }
}
