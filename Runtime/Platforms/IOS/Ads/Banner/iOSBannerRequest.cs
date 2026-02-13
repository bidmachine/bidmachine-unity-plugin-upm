#if UNITY_IOS
using System;
using BidMachineInc.Ads.Common;
using BidMachineInc.Ads.Api;

namespace BidMachineInc.Ads.iOS
{
    public class iOSBannerRequest : iOSAdRequest<BannerRequestiOSUnityBridge>, IBannerRequest {
        public iOSBannerRequest() : base() { }

        public BannerSize GetSize() 
        {
            return requestBridge.GetSize();
        }
    }
}
#endif