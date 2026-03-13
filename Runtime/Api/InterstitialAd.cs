using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.Api
{
    public sealed class InterstitialAd : IInterstitialAd
    {
        private readonly IInterstitialAd _client;

        public InterstitialAd()
        {
            _client = BidMachineClientFactory.GetInterstitialAd();
        }

        public InterstitialAd(IInterstitialAd client)
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

        public void SetListener(IInterstitialAdListener listener)
        {
            _client.SetListener(listener);
        }

        public void Load(IAdRequest request)
        {
            _client.Load(request);
        }
    }
}
