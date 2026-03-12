#if UNITY_IOS || BIDMACHINE_DEV
using BidMachineInc.Ads.Api;
using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.Ios
{
    public interface IIosAdRequestBridge
    {
        public string GetAuctionResult();

        public AuctionResult GetAuctionResultObject();

        public bool IsDestroyed();

        public bool IsExpired();
    }

    public class IosAdRequest<TBridge> : IAdRequest where TBridge : IIosAdRequestBridge, new()
    {
        protected readonly TBridge RequestBridge;

        protected IosAdRequest()
        {
           RequestBridge = new TBridge();
        }

        public string GetAuctionResult()
        {
            return RequestBridge.GetAuctionResult();
        }

        public AuctionResult GetAuctionResultObject()
        {
            return RequestBridge.GetAuctionResultObject();
        }

        public bool IsDestroyed()
        {
            return RequestBridge.IsDestroyed();
        }

        public bool IsExpired()
        {
            return RequestBridge.IsExpired();
        }
    }
}
#endif
