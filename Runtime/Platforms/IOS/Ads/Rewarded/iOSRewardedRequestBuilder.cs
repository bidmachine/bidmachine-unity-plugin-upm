#if UNITY_IOS
using System;

namespace BidMachineInc.Ads.iOS
{
    public class iOSRewardedRequestBuilder : iOSAdRequestBuilder<RewardedRequestBuilderiOSUnityBridge, iOSRewardedRequest> {
        public iOSRewardedRequestBuilder() : base() { }
    }
}
#endif