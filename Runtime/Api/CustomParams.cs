using System;
using System.Collections.Generic;

namespace BidMachineInc.Ads.Api
{
    [Serializable]
    public sealed class CustomParams
    {
        public Dictionary<string, string> Params { get; } = new();

        public CustomParams() { }

        public CustomParams(Dictionary<string, string> customParams)
        {
            Params = customParams;
        }

        public CustomParams AddParam(string key, string value)
        {
            Params[key] = value;
            return this;
        }
    }
}
