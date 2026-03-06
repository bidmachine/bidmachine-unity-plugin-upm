using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.Api
{
    public sealed class RewardedRequest : IAdRequest
    {
        private readonly IAdRequest _client;

        public RewardedRequest(IAdRequest client)
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

        public sealed class Builder : IAdRequestBuilder
        {
            private readonly IAdRequestBuilder _client = BidMachineClientFactory.GetRewardedRequestBuilder();

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

            public IAdRequestBuilder SetCustomParams(CustomParams customParams)
            {
                _client.SetCustomParams(customParams);
                return this;
            }

            public IAdRequestBuilder SetListener(IAdRequestListener listener)
            {
                _client.SetListener(listener);
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
