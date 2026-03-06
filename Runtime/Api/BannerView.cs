using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.Api
{
    public sealed class BannerView : IBannerView
    {
        private readonly IBannerView _client;

        public BannerView()
        {
            _client = BidMachineClientFactory.GetBannerView();
        }

        public BannerView(IBannerView client)
        {
            _client = client;
        }

        public bool Show(int yAxis, int xAxis, IBannerView view, BannerSize size)
        {
            return _client.Show(yAxis, xAxis, view, size);
        }

        public void Hide()
        {
            _client.Hide();
        }

        public bool CanShow()
        {
            return _client.CanShow();
        }

        public void Destroy()
        {
            _client.Destroy();
        }

        public void SetListener(IAdListener<IBannerView> listener)
        {
            _client.SetListener(listener);
        }

        public void Load(IAdRequest request)
        {
            _client.Load(request);
        }
    }
}
