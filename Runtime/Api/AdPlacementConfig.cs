namespace BidMachineInc.Ads.Api
{
    public sealed class AdPlacementConfig
    {
        internal BannerAdSize BannerAdSize { get; private set; }
        internal AdContentType AdContentType { get; private set; }
        internal string PlacementId { get; private set; }
        internal CustomParams CustomParams { get; private set; }
        internal AdPlacementType PlacementType { get; private set; }

        private AdPlacementConfig() { }

        public static BannerPlacementBuilder BannerBuilder(BannerAdSize size)
        {
            return new BannerPlacementBuilder(size);
        }

        public static InterstitialPlacementBuilder InterstitialBuilder(AdContentType contentType)
        {
            return new InterstitialPlacementBuilder(contentType);
        }

        public static RewardedPlacementBuilder RewardedBuilder(AdContentType contentType)
        {
            return new RewardedPlacementBuilder(contentType);
        }

        public sealed class BannerPlacementBuilder
        {
            private readonly AdPlacementConfig _config;

            internal BannerPlacementBuilder(BannerAdSize size)
            {
                _config = new AdPlacementConfig
                {
                    BannerAdSize = size,
                    PlacementType = AdPlacementType.Banner,
                };
            }

            public BannerPlacementBuilder WithPlacementId(string placementId)
            {
                _config.PlacementId = placementId;
                return this;
            }

            public BannerPlacementBuilder WithCustomParams(CustomParams customParams)
            {
                _config.CustomParams = customParams;
                return this;
            }

            public AdPlacementConfig Build()
            {
                return _config;
            }
        }

        public sealed class InterstitialPlacementBuilder
        {
            private readonly AdPlacementConfig _config;

            internal InterstitialPlacementBuilder(AdContentType contentType)
            {
                _config = new AdPlacementConfig
                {
                    AdContentType = contentType,
                    PlacementType = AdPlacementType.Interstitial,
                };
            }

            public InterstitialPlacementBuilder WithPlacementId(string placementId)
            {
                _config.PlacementId = placementId;
                return this;
            }

            public InterstitialPlacementBuilder WithCustomParams(CustomParams customParams)
            {
                _config.CustomParams = customParams;
                return this;
            }

            public AdPlacementConfig Build()
            {
                return _config;
            }
        }

        public sealed class RewardedPlacementBuilder
        {
            private readonly AdPlacementConfig _config;

            internal RewardedPlacementBuilder(AdContentType contentType)
            {
                _config = new AdPlacementConfig
                {
                    AdContentType = contentType,
                    PlacementType = AdPlacementType.Rewarded,
                };
            }

            public RewardedPlacementBuilder WithPlacementId(string placementId)
            {
                _config.PlacementId = placementId;
                return this;
            }

            public RewardedPlacementBuilder WithCustomParams(CustomParams customParams)
            {
                _config.CustomParams = customParams;
                return this;
            }

            public AdPlacementConfig Build()
            {
                return _config;
            }
        }
    }

    internal enum AdPlacementType
    {
        Banner,
        Interstitial,
        Rewarded,
    }
}
