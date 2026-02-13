#if UNITY_IOS
using System;
using BidMachineInc.Ads.Common;
using BidMachineInc.Ads.Api;

namespace BidMachineInc.Ads.iOS
{
    [Serializable]
    public sealed class iOSPublisher {
        public string Id;
        public string Name;
        public string Domain;
        public string[] Categories;
    }

    public sealed class iOSPublisherAdapter
    {
        public static iOSPublisher Adapt(Publisher source)
        {
            iOSPublisher target = new iOSPublisher
            {
                Id = source.Id,
                Name = source.Name,
                Domain = source.Domain,
                Categories = source.Categories,
            };
            return target;
        }
    }
}
#endif