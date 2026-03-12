#if UNITY_ANDROID || BIDMACHINE_DEV
using System;
using UnityEngine;
using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.Android
{
    internal class AndroidRewardedAdListener : AndroidAdListener<IRewardedAd, IRewardedAdListener>, ICommonRewardedAdListener<AndroidJavaObject, AndroidJavaObject>
    {
        internal AndroidRewardedAdListener(string className,
                                           IRewardedAdListener listener,
                                           Func<AndroidJavaObject, IRewardedAd> adProvider
        ) : base(className, listener, adProvider) { }

        public void onAdClosed(AndroidJavaObject ad, bool finished)
        {
            listener.onAdClosed(adProvider(ad), finished);
        }

        public void onAdRewarded(AndroidJavaObject ad)
        {
            listener.onAdRewarded(adProvider(ad));
        }
    }
}
#endif
