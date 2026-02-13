using BidMachineInc.Ads.Api;

namespace BidMachineInc.Ads.Common
{
    public interface ICommonFullscreenAdListener<TAd, TAdError> : IAdListener<TAd>
    {
        void onAdClosed(TAd ad, bool finished) { }
    }

    public interface IFullscreenAdListener<TAd> : ICommonFullscreenAdListener<TAd, BMError> { }
}
