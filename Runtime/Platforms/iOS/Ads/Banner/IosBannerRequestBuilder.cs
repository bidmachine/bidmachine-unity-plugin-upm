using BidMachineInc.Ads.Api;
using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.Ios
{
    public class IosBannerRequestBuilder : IosAdRequestBuilder<BannerAdRequestBuilderIosUnityBridge, IosBannerRequest>, IBannerRequestBuilder
    {
        public IAdRequestBuilder SetSize(BannerSize size)
        {
            RequestBuilderBridge.SetSize(size);
            return this;
        }
    }
}
