using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.Api
{
    public sealed class InterstitialRequest : IAdRequest
    {
        private readonly IAdRequest _client;

        public InterstitialRequest(IAdRequest client)
        {
            _client = client;
        }

        public string GetAuctionResult()
        {
            return _client.GetAuctionResult();
        }

        public AuctionResult GetAuctionResultObject()
        {
            return _client.GetAuctionResultObject();
        }

        public bool IsDestroyed()
        {
            return _client.IsDestroyed();
        }

        public bool IsExpired()
        {
            return _client.IsExpired();
        }

        public class Builder : IAdRequestBuilder
        {
            private readonly IAdRequestBuilder _client;

            public Builder(AdPlacementConfig config)
            {
                _client = BidMachineClientFactory.GetInterstitialRequestBuilder(config);
            }

            [System.Obsolete("Use Builder(AdPlacementConfig) constructor instead.")]
            public Builder()
            {
                _client = BidMachineClientFactory.GetInterstitialRequestBuilder();
            }

            [System.Obsolete("SetAdContentType() is deprecated. Use AdPlacementConfig.InterstitialBuilder(AdContentType) instead.")]
            public IAdRequestBuilder SetAdContentType(AdContentType contentType)
            {
                _client.SetAdContentType(contentType);
                return this;
            }

            public IAdRequestBuilder SetTargetingParams(TargetingParams targetingParams)
            {
                _client.SetTargetingParams(targetingParams);
                return this;
            }

            public IAdRequestBuilder SetPriceFloorParams(PriceFloorParams priceFloorParams)
            {
                _client.SetPriceFloorParams(priceFloorParams);
                return this;
            }

            [System.Obsolete("SetCustomParams() is deprecated. Use AdPlacementConfig.InterstitialBuilder().WithCustomParams() instead.")]
            public IAdRequestBuilder SetCustomParams(CustomParams customParams)
            {
                _client.SetCustomParams(customParams);
                return this;
            }

            public IAdRequestBuilder SetListener(IAdAuctionRequestListener listener)
            {
                _client.SetListener(listener);
                return this;
            }

            public IAdRequestBuilder SetLoadingTimeOut(int loadingTimeout)
            {
                _client.SetLoadingTimeOut(loadingTimeout);
                return this;
            }

            [System.Obsolete("SetPlacementId() is deprecated. Use AdPlacementConfig.InterstitialBuilder().WithPlacementId() instead.")]
            public IAdRequestBuilder SetPlacementId(string placementId)
            {
                _client.SetPlacementId(placementId);
                return this;
            }

            public IAdRequestBuilder SetBidPayload(string bidPayload)
            {
                _client.SetBidPayload(bidPayload);
                return this;
            }

            public IAdRequestBuilder SetNetworks(string networks)
            {
                _client.SetNetworks(networks);
                return this;
            }

            public IAdRequest Build()
            {
                return _client.Build();
            }
        }
    }
}
