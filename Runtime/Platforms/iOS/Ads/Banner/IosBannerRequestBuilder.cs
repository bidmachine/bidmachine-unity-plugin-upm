#if UNITY_IOS || BIDMACHINE_DEV
using BidMachineInc.Ads.Api;
using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.Ios
{
    public class IosBannerRequestBuilder : IosAdRequestBuilder<BannerAdRequestBuilderIosUnityBridge, IosBannerRequest>, IBannerRequestBuilder
    {
        public IosBannerRequestBuilder(AdPlacementConfig config)
        {
            if (config.BannerAdSize != null)
            {
                SetSize(config.BannerAdSize);
            }

            if (!string.IsNullOrEmpty(config.PlacementId))
            {
                SetPlacementId(config.PlacementId);
            }

            if (config.CustomParams != null)
            {
                SetCustomParams(config.CustomParams);
            }
        }

        [System.Obsolete("Use constructor with AdPlacementConfig parameter")]
        public IosBannerRequestBuilder() { }

        public IAdRequestBuilder SetSize(BannerSize size)
        {
            RequestBuilderBridge.SetSize(size);
            return this;
        }

        public IAdRequestBuilder SetSize(BannerAdSize size)
        {
            RequestBuilderBridge.SetSize(size ?? BannerAdSize.Banner);
            return this;
        }
    }
}
#endif
