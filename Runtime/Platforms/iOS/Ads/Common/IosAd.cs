#if UNITY_IOS || BIDMACHINE_DEV
using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.Ios
{
    public class IosAd<TBridge> where TBridge : IIosAdBridge, new()
    {
        protected readonly TBridge AdBridge;

        protected IosAd()
        {
           AdBridge = new TBridge();
        }

        public bool CanShow()
        {
            return AdBridge.CanShow();
        }

        public void Destroy()
        {
            AdBridge.Destroy();
        }

        public void Load(IAdRequest request)
        {
            AdBridge.Load();
        }
    }
}
#endif
