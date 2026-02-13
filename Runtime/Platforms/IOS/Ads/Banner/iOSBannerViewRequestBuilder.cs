#if UNITY_IOS
using UnityEngine;
using BidMachineInc.Ads.Api;
using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.iOS
{
    public class iOSBannerRequestBuilder : iOSAdRequestBuilder<BannerAdRequestBuilderiOSUnityBridge, iOSBannerRequest>, IBannerRequestBuilder {
        public iOSBannerRequestBuilder() : base() { }

        public IAdRequestBuilder SetSize(BannerSize size)
        {
            requestBuilderBridge.SetSize(size);
            return this;
        }
    }
}
#endif