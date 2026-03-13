#if UNITY_IOS || BIDMACHINE_DEV
using BidMachineInc.Ads.Api;

namespace BidMachineInc.Ads.Ios
{
    public class IosInterstitialRequestBuilder : IosAdRequestBuilder<InterstitialRequestBuilderIosUnityBridge, IosInterstitialRequest>
    {
        public IosInterstitialRequestBuilder(AdPlacementConfig config)
        {
            if (config.AdContentType != AdContentType.All)
            {
                SetAdContentType(config.AdContentType);
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
        public IosInterstitialRequestBuilder() { }
    }
}
#endif
