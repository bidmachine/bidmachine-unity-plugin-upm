#if UNITY_ANDROID || BIDMACHINE_DEV
using UnityEngine;
using BidMachineInc.Ads.Api;
using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.Android
{
    internal class AndroidBannerRequestBuilder : IBannerRequestBuilder
    {
        private readonly AndroidAdRequestBuilder _requestBuilder;

        public AndroidBannerRequestBuilder()
        {
            _requestBuilder = new AndroidAdRequestBuilder(AndroidConsts.BannerRequestBuilderClassName, AndroidConsts.BannerRequestListenerClassName,
                delegate(AndroidJavaObject request)
                {
                    return new AndroidBannerRequest(request);
                }
            );
        }

        public IAdRequestBuilder SetSize(BannerSize size)
        {
            _requestBuilder.JavaObject.Call<AndroidJavaObject>("setSize", AndroidNativeConverter.GetBannerSize(size));

            return this;
        }

        public IAdRequestBuilder SetAdContentType(AdContentType contentType)
        {
            return _requestBuilder.SetAdContentType(contentType);
        }

        public IAdRequestBuilder SetTargetingParams(TargetingParams targetingParams)
        {
            return _requestBuilder.SetTargetingParams(targetingParams);
        }

        public IAdRequestBuilder SetPriceFloorParams(PriceFloorParams priceFloorParams)
        {
            return _requestBuilder.SetPriceFloorParams(priceFloorParams);
        }

        public IAdRequestBuilder SetCustomParams(CustomParams customParams)
        {
            return _requestBuilder.SetCustomParams(customParams);
        }

        public IAdRequestBuilder SetListener(IAdRequestListener listener)
        {
            return _requestBuilder.SetListener(listener);
        }

        public IAdRequestBuilder SetListener(IAdAuctionRequestListener listener)
        {
            return _requestBuilder.SetListener(listener);
        }

        public IAdRequestBuilder SetLoadingTimeOut(int loadingTimeout)
        {
            return _requestBuilder.SetLoadingTimeOut(loadingTimeout);
        }

        public IAdRequestBuilder SetPlacementId(string placementId)
        {
            return _requestBuilder.SetPlacementId(placementId);
        }

        public IAdRequestBuilder SetBidPayload(string bidPayload)
        {
            return _requestBuilder.SetBidPayload(bidPayload);
        }

        public IAdRequestBuilder SetNetworks(string networks)
        {
            return _requestBuilder.SetNetworks(networks);
        }

        public IAdRequest Build()
        {
            return _requestBuilder.Build();
        }
    }
}
#endif
