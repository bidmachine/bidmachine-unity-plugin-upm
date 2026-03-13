#if UNITY_ANDROID || BIDMACHINE_DEV
using System;
using UnityEngine;
using UnityEngine.Scripting;
using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.Android
{
    internal class AndroidRewardedAdListener : AndroidAdListener<IRewardedAd, IRewardedAdListener>, ICommonRewardedAdListener<AndroidJavaObject, AndroidJavaObject>
    {
        internal AndroidRewardedAdListener(string className,
                                           IRewardedAdListener listener,
                                           Func<AndroidJavaObject, IRewardedAd> adProvider
        ) : base(className, listener, adProvider) { }

        [Preserve]
        public void onAdClosed(AndroidJavaObject ad, bool finished)
        {
            listener.onAdClosed(adProvider(ad), finished);
        }

        [Preserve]
        public void onAdRewarded(AndroidJavaObject ad)
        {
            listener.onAdRewarded(adProvider(ad));
        }
    }
}
#endif
