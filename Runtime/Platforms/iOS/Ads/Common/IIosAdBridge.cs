#if UNITY_IOS || BIDMACHINE_DEV
using BidMachineInc.Ads.Api;

namespace BidMachineInc.Ads.Ios
{
    public interface IIosAdBridge
    {
        public bool CanShow();

        public void Destroy();

        public void Load();

        public void SetLoadCallback(AdCallback onLoad);

        public void SetLoadFailedCallback(AdFailureCallback onFailedToLoad);

        public void SetPresentCallback(AdCallback onPresent);

        public void SetPresentFailedCallback(AdFailureCallback onFailedToPresent);

        public void SetImpressionCallback(AdCallback onImpression);

        public void SetExpiredCallback(AdCallback onExpired);
    }

    public interface IIosBannerAdBridge : IIosAdBridge
    {
        public bool Show(int yAxis, int xAxis);

        public void Hide();

        public BannerAdSize GetBannerAdSize();
    }

    public interface IIosFullscreenAdBridge : IIosAdBridge
    {
        public void Show();

        public void SetClosedCallback(AdClosedCallback onClosed);
    }

    public interface IIosRewardedAdBridge : IIosFullscreenAdBridge
    {
        public void SetRewardedCallback(AdCallback onRewarded);
    }
}
#endif
