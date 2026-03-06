using System;
using System.Collections.Generic;

namespace BidMachineInc.Ads.Api
{
    [Serializable]
    public sealed class CustomExtras
    {
        public Dictionary<string, string> Extras { get; } = new();

        public CustomExtras() { }

        public CustomExtras(Dictionary<string, string> customExtras)
        {
            Extras = customExtras;
        }

        public CustomExtras AddExtra(string key, string value)
        {
            Extras[key] = value;
            return this;
        }
    }
}
