using System;

namespace BidMachineInc.Ads.Api
{
    [Serializable]
    public sealed class AuctionResult
    {
        public string DealId { get; set; }
        public string DemandSource { get; set; }
        public string Cid { get; set; }

        public CustomParams CustomParams { get; set; }
        public CustomExtras CustomExtras { get; set; }

        public string CreativeId { get; set; }
        public string BidId { get; set; }
        public double Price { get; set; }

        // Deprecated aliases
        [Obsolete("Use AuctionResult.DealId instead.")]
        public string DealID { get => DealId; set => DealId = value; }
        [Obsolete("Use AuctionResult.Cid instead.")]
        public string CID { get => Cid; set => Cid = value; }
        [Obsolete("Use AuctionResult.CreativeId instead.")]
        public string CreativeID { get => CreativeId; set => CreativeId = value; }
        [Obsolete("Use AuctionResult.BidId instead.")]
        public string BidID { get => BidId; set => BidId = value; }
    }
}
