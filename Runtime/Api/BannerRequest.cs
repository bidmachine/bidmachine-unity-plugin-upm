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

        [System.Obsolete("GetSize() is deprecated. Use GetBannerAdSize() instead.")]
        public BannerSize GetSize()
        {
            return _client.GetSize();
        }

        public BannerAdSize GetBannerAdSize()
        {
            return _client.GetBannerAdSize();
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
            private readonly IBannerRequestBuilder _client;

            public Builder(AdPlacementConfig config)
            {
                _client = BidMachineClientFactory.GetBannerRequestBuilder(config);
            }

            [System.Obsolete("Use Builder(AdPlacementConfig) constructor instead.")]
            public Builder()
            {
                _client = BidMachineClientFactory.GetBannerRequestBuilder();
            }

            [System.Obsolete("SetSize(BannerSize) is deprecated. Use AdPlacementConfig.BannerBuilder(BannerAdSize) instead.")]
            public IAdRequestBuilder SetSize(BannerSize size)
            {
                _client.SetSize(size);
                return this;
            }

            [System.Obsolete("SetSize(BannerAdSize) is deprecated. Use AdPlacementConfig.BannerBuilder(BannerAdSize) instead.")]
            public IAdRequestBuilder SetSize(BannerAdSize size)
            {
                _client.SetSize(size);
                return this;
            }

            [System.Obsolete("SetAdContentType() is deprecated in SDK 3.5.0+.")]
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

            [System.Obsolete("SetCustomParams() is deprecated. Use AdPlacementConfig.BannerBuilder().WithCustomParams() instead.")]
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

            [System.Obsolete("SetPlacementId() is deprecated. Use AdPlacementConfig.BannerBuilder().WithPlacementId() instead.")]
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
