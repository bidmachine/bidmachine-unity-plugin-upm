using BidMachineInc.Ads.Api;

namespace BidMachineInc.Ads.Common
{
    public interface IRewardedAd : IAd<IRewardedAdListener>
    {
        void Show();
    }

    public interface ICommonRewardedAdListener<TAd, TAdError>
        : ICommonFullscreenAdListener<TAd, TAdError>
    {
        void onAdRewarded(TAd ad) { }
    }

    public interface IRewardedAdListener : ICommonRewardedAdListener<IRewardedAd, BMError> { }
}
