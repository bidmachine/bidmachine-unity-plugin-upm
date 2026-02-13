using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.Api
{
    public sealed class RewardedAd : IRewardedAd
    {
        private readonly IRewardedAd client;

        public RewardedAd()
        {
            this.client = BidMachineClientFactory.GetRewardedAd();
        }

        public RewardedAd(IRewardedAd client)
        {
            this.client = client;
        }

        public void Show()
        {
            client.Show();
        }

        public bool CanShow()
        {
            return client.CanShow();
        }

        public void Destroy()
        {
            client.Destroy();
        }

        public void SetListener(IRewardedAdListener listener)
        {
            client.SetListener(listener);
        }

        public void Load(IAdRequest request)
        {
            client.Load(request);
        }
    }
}
