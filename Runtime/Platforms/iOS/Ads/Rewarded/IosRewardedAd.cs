#if UNITY_IOS || BIDMACHINE_DEV
using System;
using AOT;
using BidMachineInc.Ads.Api;
using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.Ios
{
    public class IosRewardedAd : IosAd<RewardedAdIosUnityBridge>, IRewardedAd 
    {
        private static IRewardedAdListener _listener;

        public void Show()
        {
            AdBridge.Show();
        }

        public void SetListener(IRewardedAdListener listener)
        {
            _listener = listener;

            AdBridge.SetLoadCallback(DidLoadAd);
            AdBridge.SetLoadFailedCallback(DidFailToLoadAd);
            AdBridge.SetPresentCallback(DidPresentAd);
            AdBridge.SetPresentFailedCallback(DidFailToPresentAd);
            AdBridge.SetImpressionCallback(DidReceiveAdImpression);
            AdBridge.SetExpiredCallback(DidExpire);
            AdBridge.SetClosedCallback(DidClose);
            AdBridge.SetRewardedCallback(DidReward);
        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void DidLoadAd(IntPtr ad)
        {
            _listener?.onAdLoaded(new IosRewardedAd());
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

            _listener.onAdLoadFailed(new IosRewardedAd(), bmError);
        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void DidPresentAd(IntPtr ad)
        {
            _listener?.onAdShown(new IosRewardedAd());
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

            _listener.onAdShowFailed(new IosRewardedAd(), bmError);
        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void DidReceiveAdImpression(IntPtr ad)
        {
            _listener?.onAdImpression(new IosRewardedAd());
        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void DidExpire(IntPtr ad)
        {
            _listener?.onAdExpired(new IosRewardedAd());
        }

        [MonoPInvokeCallback(typeof(AdClosedCallback))]
        private static void DidClose(IntPtr ad, bool finished)
        {
            _listener?.onAdClosed(new IosRewardedAd(), finished);
        }

        [MonoPInvokeCallback(typeof(AdCallback))]
        private static void DidReward(IntPtr ad)
        {
            _listener?.onAdRewarded(new IosRewardedAd());
        }
    }
}
#endif
