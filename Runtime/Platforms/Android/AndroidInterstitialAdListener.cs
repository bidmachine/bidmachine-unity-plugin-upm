using System;
using UnityEngine;
using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.Android
{
    internal class AndroidInterstitialAdListener : AndroidAdListener<IInterstitialAd, IInterstitialAdListener>, ICommonInterstitialAdListener<AndroidJavaObject, AndroidJavaObject>
    {
        internal AndroidInterstitialAdListener(string className,
                                               IInterstitialAdListener listener,
                                               Func<AndroidJavaObject, IInterstitialAd> adProvider
        ) : base(className, listener, adProvider) { }

        public void onAdClosed(AndroidJavaObject ad, bool finished)
        {
            listener.onAdClosed(adProvider(ad), finished);
        }
    }
}
