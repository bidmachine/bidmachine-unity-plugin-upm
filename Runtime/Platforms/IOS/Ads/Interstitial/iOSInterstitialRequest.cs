#if UNITY_IOS
using System;

namespace BidMachineInc.Ads.iOS
{
    public class iOSInterstitialRequest : iOSAdRequest<InterstitialRequestiOSUnityBridge> {
        public iOSInterstitialRequest() : base() { }
    }
}
#endif