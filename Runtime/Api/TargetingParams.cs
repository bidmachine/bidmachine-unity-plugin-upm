using System;
using System.Collections.Generic;

namespace BidMachineInc.Ads.Api
{
    [Serializable]
    public sealed class TargetingParams
    {
        public string UserId { get; set; }

        public Gender UserGender { get; set; }

        public int BirthdayYear { get; set; }

        public string[] Keywords { get; set; }

        public Location DeviceLocation { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Zip { get; set; }

        public string StoreUrl { get; set; }

        public string StoreCategory { get; set; }

        public string[] StoreSubCategories { get; set; }

        public string Framework => "unity";

        public bool IsPaid { get; set; }

        public ExternalUserId[] ExternalUserIds { get; set; }

        public HashSet<string> BlockedDomains { get; set; }

        public HashSet<string> BlockedCategories { get; set; }

        public HashSet<string> BlockedApplications { get; set; }

        [Serializable]
        public sealed class Location
        {
            public string Provider { get; set; }

            public double Latitude { get; set; }

            public double Longitude { get; set; }
        }

        [Serializable]
        public enum Gender
        {
            Female,
            Male,
            Omitted,
        }

        // Deprecated aliases
        [Obsolete("Use TargetingParams.UserGender instead.")]
        public Gender gender { get => UserGender; set => UserGender = value; }
        [Obsolete("Use TargetingParams.ExternalUserIds instead.")]
        public ExternalUserId[] externalUserIds { get => ExternalUserIds; set => ExternalUserIds = value; }
    }
}
