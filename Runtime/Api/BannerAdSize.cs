namespace BidMachineInc.Ads.Api
{
    public sealed class BannerAdSize
    {
        public static readonly BannerAdSize Banner = new BannerAdSize(320, 50, false);
        public static readonly BannerAdSize Leaderboard = new BannerAdSize(728, 90, false);
        public static readonly BannerAdSize MediumRectangle = new BannerAdSize(300, 250, false);

        public int Width { get; private set; }
        public int Height { get; private set; }
        public bool IsAdaptive { get; private set; }

        private BannerAdSize(int width, int height, bool isAdaptive)
        {
            Width = width;
            Height = height;
            IsAdaptive = isAdaptive;
        }

        public static BannerAdSize Adaptive(int width, int maxHeight)
        {
            return new BannerAdSize(width, maxHeight, true);
        }

        public static int GetMaxAdaptiveHeight(int width)
        {
            if (width >= 728) return 90;
            if (width >= 468) return 60;
            if (width >= 320) return 50;
            return 50;
        }

        public override string ToString()
        {
            return IsAdaptive ? $"Adaptive_{Width}x{Height}" : $"Size_{Width}x{Height}";
        }

        internal BannerSize ToLegacyBannerSize()
        {
            if (Width == 320 && Height == 50) return BannerSize.Banner;
            if (Width == 300 && Height == 250) return BannerSize.MediumRectangle;
            if (Width == 728 && Height == 90) return BannerSize.Leaderboard;

            return BannerSize.Banner;
        }

        internal static BannerAdSize FromLegacyBannerSize(BannerSize bannerSize)
        {
            return bannerSize switch
            {
                BannerSize.MediumRectangle => MediumRectangle,
                BannerSize.Leaderboard => Leaderboard,
                _ => Banner,
            };
        }
    }
}
