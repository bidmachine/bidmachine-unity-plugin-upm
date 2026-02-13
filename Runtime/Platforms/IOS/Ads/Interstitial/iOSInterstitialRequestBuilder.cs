#if UNITY_IOS
using System;

namespace BidMachineInc.Ads.iOS
{
    public class iOSInterstitialRequestBuilder : iOSAdRequestBuilder<InterstitialRequestBuilderiOSUnityBridge, iOSInterstitialRequest> {
        public iOSInterstitialRequestBuilder() : base() { }
    }
}
#endif
