using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.Api
{
    public sealed class BannerRequest : IBannerRequest
    {
        private readonly IBannerRequest _client;

        public BannerRequest(IBannerRequest client)
        {
            _client = client;
        }

        public BannerSize GetSize()
        {
            return _client.GetSize();
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

        public class Builder : IBannerRequestBuilder
        {
            private readonly IBannerRequestBuilder _client = BidMachineClientFactory.GetBannerRequestBuilder();

            public IAdRequestBuilder SetSize(BannerSize size)
            {
                _client.SetSize(size);
                return this;
            }

            public IAdRequestBuilder SetAdContentType(AdContentType contentType)
            {
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
