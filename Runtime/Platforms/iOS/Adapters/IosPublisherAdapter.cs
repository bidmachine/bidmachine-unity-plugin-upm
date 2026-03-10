using System;
using BidMachineInc.Ads.Api;

namespace BidMachineInc.Ads.Ios
{
    [Serializable]
    public sealed class IosPublisher {
        public string id;
        public string name;
        public string domain;
        public string[] categories;
    }

    public static class IosPublisherAdapter
    {
        public static IosPublisher Adapt(Publisher source)
        {
            var target = new IosPublisher
            {
                id = source.Id,
                name = source.Name,
                domain = source.Domain,
                categories = source.Categories,
            };
            return target;
        }
    }
}
