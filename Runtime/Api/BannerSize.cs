namespace BidMachineInc.Ads.Api
{
    public enum BannerSize
    {
        Banner = 0,
        MediumRectangle = 1,
        Leaderboard = 2,

        // Deprecated aliases
        [System.Obsolete("Use BannerSize.Banner instead.")]
        Size_320x50 = 0,
        [System.Obsolete("Use BannerSize.MediumRectangle instead.")]
        Size_300x250 = 1,
        [System.Obsolete("Use BannerSize.Leaderboard instead.")]
        Size_728x90 = 2,
    }
}
