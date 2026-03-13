#if UNITY_ANDROID || BIDMACHINE_DEV
using System;
using UnityEngine;
using UnityEngine.Scripting;
using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.Android
{
    internal class AndroidInterstitialAdListener : AndroidAdListener<IInterstitialAd, IInterstitialAdListener>, ICommonInterstitialAdListener<AndroidJavaObject, AndroidJavaObject>
    {
        internal AndroidInterstitialAdListener(string className,
                                               IInterstitialAdListener listener,
                                               Func<AndroidJavaObject, IInterstitialAd> adProvider
        ) : base(className, listener, adProvider) { }

        [Preserve]
        public void onAdClosed(AndroidJavaObject ad, bool finished)
        {
            listener.onAdClosed(adProvider(ad), finished);
        }
    }
}
#endif
