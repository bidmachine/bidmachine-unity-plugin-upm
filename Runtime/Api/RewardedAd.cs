using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.Api
{
    public sealed class RewardedAd : IRewardedAd
    {
        private readonly IRewardedAd _client;

        public RewardedAd()
        {
            _client = BidMachineClientFactory.GetRewardedAd();
        }

        public RewardedAd(IRewardedAd client)
        {
            _client = client;
        }

        public void Show()
        {
            _client.Show();
        }

        public bool CanShow()
        {
            return _client.CanShow();
        }

        public void Destroy()
        {
            _client.Destroy();
        }

        public void SetListener(IRewardedAdListener listener)
        {
            _client.SetListener(listener);
        }

        public void Load(IAdRequest request)
        {
            _client.Load(request);
        }
    }
}
