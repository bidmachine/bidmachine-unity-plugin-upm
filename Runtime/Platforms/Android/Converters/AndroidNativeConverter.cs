#if UNITY_ANDROID || BIDMACHINE_DEV
using UnityEngine;
using BidMachineInc.Ads.Api;

namespace BidMachineInc.Ads.Android
{
    internal static class AndroidNativeConverter
    {
        private static AndroidJavaObject _activity;

        private static AndroidJavaObject GetActivityInternal()
        {
            using var jcUnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            return jcUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        }

        public static AndroidJavaObject GetActivity()
        {
            return _activity ??= GetActivityInternal();
        }

        public static object GetObject(object obj)
        {
            return obj switch
            {
                char => new AndroidJavaObject("java.lang.Character", obj),
                bool => new AndroidJavaObject("java.lang.Boolean", obj),
                int => new AndroidJavaObject("java.lang.Integer", obj),
                long => new AndroidJavaObject("java.lang.Long", obj),
                float => new AndroidJavaObject("java.lang.Float", obj),
                double => new AndroidJavaObject("java.lang.Double", obj),
                _ => obj,
            };
        }

        public static AndroidJavaObject GetArrayList<T>(T[] objs)
        {
            var jArrayList = new AndroidJavaObject("java.util.ArrayList");
            foreach (var obj in objs)
            {
                jArrayList.Call<bool>("add", GetObject(obj));
            }
            return jArrayList;
        }

        public static AndroidJavaObject GetBannerSize(BannerSize bannerSize)
        {
            return bannerSize switch
            {
                BannerSize.MediumRectangle => GetBannerSize("Size_300x250"),
                BannerSize.Leaderboard => GetBannerSize("Size_728x90"),
                _ => GetBannerSize("Size_320x50"),
            };
        }

        private static AndroidJavaObject GetBannerSize(string sizeName)
        {
            var jcBannerSize = new AndroidJavaClass("io.bidmachine.banner.BannerSize");
            return jcBannerSize.GetStatic<AndroidJavaObject>(sizeName);
        }

        public static AndroidJavaObject GetPriceFloorParams(PriceFloorParams priceFloorParams)
        {
            var jObject = new AndroidJavaObject("io.bidmachine.PriceFloorParams");

            if (priceFloorParams == null || priceFloorParams.PriceFloors == null) return jObject;

            foreach (var priceFloor in priceFloorParams.PriceFloors)
            {
                jObject.Call<AndroidJavaObject>("addPriceFloor", GetObject(priceFloor.Key), priceFloor.Value);
            }

            return jObject;
        }

        public static AndroidJavaObject GetCustomParams(CustomParams customParams)
        {
            var jObject = new AndroidJavaObject("io.bidmachine.CustomParams");

            if (customParams == null || customParams.Params == null) return jObject;

            foreach (var customParam in customParams.Params)
            {
                jObject.Call<AndroidJavaObject>("addParam", GetObject(customParam.Key), customParam.Value);
            }

            return jObject;
        }

        public static AndroidJavaObject GetPublisher(Publisher publisher)
        {
            var jObject = new AndroidJavaObject("io.bidmachine.Publisher$Builder");
            jObject.Call("setId", publisher.Id);
            jObject.Call("setName", publisher.Name);
            jObject.Call("setDomain", publisher.Domain);
            jObject.Call("addCategories", GetArrayList(publisher.Categories));

            return jObject.Call<AndroidJavaObject>("build");
        }

        public static AndroidJavaObject GetTargetingParams(TargetingParams targetingParams)
        {
            var jObject = new AndroidJavaObject("io.bidmachine.TargetingParams");

            if (targetingParams == null) return jObject;

            jObject.Call("setUserId", targetingParams.UserId);

            var jcGender = new AndroidJavaClass("io.bidmachine.utils.Gender");
            var jGender = jcGender.GetStatic<AndroidJavaObject>(targetingParams.UserGender.ToString());
            jObject.Call("setGender", jGender);

            jObject.Call("setBirthdayYear", GetObject(targetingParams.BirthdayYear));
            jObject.Call("setKeywords", (object)targetingParams.Keywords);

            var location = targetingParams.DeviceLocation;
            if (location != null)
            {
                var jLocation = new AndroidJavaObject("android.location.Location", location.Provider);
                jLocation.Call("setLatitude", location.Latitude);
                jLocation.Call("setLongitude", location.Longitude);
                jObject.Call("setDeviceLocation", jLocation);
            }

            jObject.Call("setCountry", targetingParams.Country);

            jObject.Call("setCity", targetingParams.City);

            jObject.Call("setZip", targetingParams.Zip);

            jObject.Call("setStoreUrl", targetingParams.StoreUrl);

            jObject.Call("setStoreCategory", targetingParams.StoreCategory);

            jObject.Call("setStoreSubCategories", (object)targetingParams.StoreSubCategories);

            jObject.Call("setFramework", targetingParams.Framework);

            jObject.Call("setPaid", GetObject(targetingParams.IsPaid));

            var externalUserIds = targetingParams.ExternalUserIds;
            if (externalUserIds != null)
            {
                var jArrayList = new AndroidJavaObject("java.util.ArrayList");
                foreach (var externalUserId in externalUserIds)
                {
                    var jExternalUserId = new AndroidJavaObject("io.bidmachine.ExternalUserId", externalUserId.SourceId, externalUserId.Value);
                    jArrayList.Call<bool>("add", jExternalUserId);
                }
                jObject.Call("setExternalUserIds", jArrayList);
            }

            var blockedApplications = targetingParams.BlockedApplications;
            if (blockedApplications != null)
            {
                foreach (string app in blockedApplications)
                {
                    jObject.Call("addBlockedApplication", app);
                }
            }

            var blockedCategories = targetingParams.BlockedCategories;
            if (blockedCategories != null)
            {
                foreach (string category in blockedCategories)
                {
                    jObject.Call("addBlockedAdvertiserIABCategory", category);
                }
            }

            var blockedDomains = targetingParams.BlockedDomains;
            if (blockedDomains != null)
            {
                foreach (string domain in blockedDomains)
                {
                    jObject.Call("addBlockedAdvertiserDomain", domain);
                }
            }

            return jObject;
        }
    }
}
#endif
