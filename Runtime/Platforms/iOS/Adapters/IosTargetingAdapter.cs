#if UNITY_IOS || BIDMACHINE_DEV
using System;
using System.Collections.Generic;
using BidMachineInc.Ads.Api;

namespace BidMachineInc.Ads.Ios
{
    [Serializable]
    public sealed class IosLocation
    {
        public string provider;
        public double latitude;
        public double longitude;
    }

    [Serializable]
    public sealed class IosExternalUserId
    {
        public string sourceId;
        public string value;
    }

    [Serializable]
    public sealed class IosTargetingParameters
    {
        public string userId;
        public TargetingParams.Gender gender;
        public int birthdayYear;
        public string[] keywords;
        public IosLocation deviceLocation;
        public string country;
        public string city;
        public string zip;
        public string storeUrl;
        public string storeCategory;
        public string[] storeSubCategories;
        public string framework;
        public bool isPaid;

        public IosExternalUserId[] externalUserIds;
        public string[] blockedDomains;
        public string[] blockedCategories;
        public string[] blockedApplications;
    }

    public static class IosTargetingAdapter
    {
        public static IosTargetingParameters Adapt(TargetingParams source)
        {
            var deviceLocation = new IosLocation
            {
                provider = source.DeviceLocation.Provider,
                latitude = source.DeviceLocation.Latitude,
                longitude = source.DeviceLocation.Longitude,
            };

            var iosExternalUsers = new IosExternalUserId[source.ExternalUserIds.Length];
            for (int i = 0; i < source.ExternalUserIds.Length; i++)
            {
                iosExternalUsers[i] = new IosExternalUserId
                {
                    sourceId = source.ExternalUserIds[i].SourceId,
                    value = source.ExternalUserIds[i].Value,
                };
            }

            string[] blockedDomains = source.BlockedDomains.AsArray();
            string[] blockedCategories = source.BlockedCategories.AsArray();
            string[] blockedApplications = source.BlockedApplications.AsArray();

            var target = new IosTargetingParameters
            {
                userId = source.UserId,
                gender = source.UserGender,
                birthdayYear = source.BirthdayYear,
                keywords = source.Keywords,
                deviceLocation = deviceLocation,
                country = source.Country,
                city = source.City,
                zip = source.Zip,
                storeUrl = source.StoreUrl,
                storeCategory = source.StoreCategory,
                storeSubCategories = source.StoreSubCategories,
                framework = source.Framework,
                isPaid = source.IsPaid,
                externalUserIds = iosExternalUsers,
                blockedDomains = blockedDomains,
                blockedCategories = blockedCategories,
                blockedApplications = blockedApplications,
            };
            return target;
        }
    }

    internal static class HashSetExtensions
    {
        internal static T[] AsArray<T>(this HashSet<T> hashSet)
        {
            var array = new T[hashSet.Count];
            hashSet.CopyTo(array);
            return array;
        }
    }
}
#endif
