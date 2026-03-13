#if UNITY_IOS || BIDMACHINE_DEV
using BidMachineInc.Ads.Api;
using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.Ios
{
    public class IosBannerRequest : IosAdRequest<BannerRequestIosUnityBridge>, IBannerRequest
    {
        public BannerSize GetSize()
        {
            return RequestBridge.GetSize();
        }

        public BannerAdSize GetBannerAdSize()
        {
            return RequestBridge.GetBannerAdSize();
        }
    }
}
#endif
