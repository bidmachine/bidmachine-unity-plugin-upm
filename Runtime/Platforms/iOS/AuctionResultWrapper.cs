#if UNITY_IOS || BIDMACHINE_DEV
using System;
using BidMachineInc.Ads.Api;

namespace BidMachineInc.Ads.Ios
{
    using CustomValuesList = KeyValueList<string, string>;

    [Serializable]
    internal class AuctionResultWrapper
    {
        public string dealId;
        public string demandSource;
        public string cid;

        public CustomValuesList customParams;
        public CustomValuesList customExtras;

        public string creativeID;
        public string bidId;
        public double price;

        public AuctionResult ToAuctionResult()
        {
            var auctionResult = new AuctionResult
            {
                DealId = dealId,
                DemandSource = demandSource,
                Cid = cid,
                CustomExtras = new CustomExtras(),
                CreativeId = creativeID,
                BidId = bidId,
                Price = price,
                CustomParams = new CustomParams()
            };

            foreach (var kvp in customParams.items)
            {
                auctionResult.CustomParams.AddParam(kvp.key, kvp.value);
            }

            foreach (var kvp in customExtras.items)
            {
                auctionResult.CustomExtras.AddExtra(kvp.key, kvp.value);
            }

            return auctionResult;
        }
    }
}
#endif
