using System;

namespace BidMachineInc.Ads.Api
{
    [Serializable]
    public sealed class ExternalUserId
    {
        public string SourceId { get; set; }

        public string Value { get; set; }
    }
}
