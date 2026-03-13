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

        public static AndroidJavaObject GetBannerAdSize(BannerAdSize bannerAdSize)
        {
            var safeBannerAdSize = bannerAdSize ?? BannerAdSize.Banner;

            try
            {
                var jcBannerAdSize = new AndroidJavaClass("io.bidmachine.BannerAdSize");
                if (safeBannerAdSize.IsAdaptive)
                {
                    return jcBannerAdSize.CallStatic<AndroidJavaObject>("adaptive", GetObject(safeBannerAdSize.Width), GetObject(safeBannerAdSize.Height));
                }

                if (safeBannerAdSize.Width == 320 && safeBannerAdSize.Height == 50)
                {
                    return jcBannerAdSize.GetStatic<AndroidJavaObject>("Banner");
                }

                if (safeBannerAdSize.Width == 728 && safeBannerAdSize.Height == 90)
                {
                    return jcBannerAdSize.GetStatic<AndroidJavaObject>("Leaderboard");
                }

                if (safeBannerAdSize.Width == 300 && safeBannerAdSize.Height == 250)
                {
                    return jcBannerAdSize.GetStatic<AndroidJavaObject>("MediumRectangle");
                }
            }
            catch
            {
            }

            return GetBannerSize(safeBannerAdSize.ToLegacyBannerSize());
        }

        public static AndroidJavaObject GetAdPlacementConfig(AdPlacementConfig config)
        {
            AndroidJavaObject builder;

            switch (config.PlacementType)
            {
                case AdPlacementType.Banner:
                    {
                        var jcAdPlacementConfig = new AndroidJavaClass("io.bidmachine.AdPlacementConfig");
                        builder = jcAdPlacementConfig.CallStatic<AndroidJavaObject>("bannerBuilder", GetBannerAdSize(config.BannerAdSize ?? BannerAdSize.Banner));
                        break;
                    }

                case AdPlacementType.Interstitial:
                    {
                        var jcAdPlacementConfig = new AndroidJavaClass("io.bidmachine.AdPlacementConfig");
                        var jcAdContentType = new AndroidJavaClass("io.bidmachine.AdContentType");
                        var jAdContentType = jcAdContentType.GetStatic<AndroidJavaObject>(config.AdContentType.ToString());

                        builder = jcAdPlacementConfig.CallStatic<AndroidJavaObject>("interstitialBuilder", jAdContentType);
                        break;
                    }

                case AdPlacementType.Rewarded:
                    {
                        var jcAdPlacementConfig = new AndroidJavaClass("io.bidmachine.AdPlacementConfig");
                        var jcAdContentType = new AndroidJavaClass("io.bidmachine.AdContentType");
                        var jAdContentType = jcAdContentType.GetStatic<AndroidJavaObject>(config.AdContentType.ToString());

                        builder = jcAdPlacementConfig.CallStatic<AndroidJavaObject>("rewardedBuilder", jAdContentType);
                        break;
                    }

                default:
                    throw new System.ArgumentException($"Unknown placement type: {config.PlacementType}");
            }

            if (!string.IsNullOrEmpty(config.PlacementId))
            {
                builder = builder.Call<AndroidJavaObject>("withPlacementId", GetObject(config.PlacementId));
            }

            if (config.CustomParams != null)
            {
                builder = builder.Call<AndroidJavaObject>("withCustomParams", GetCustomParams(config.CustomParams));
            }

            return builder.Call<AndroidJavaObject>("build");
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
            jObject = jObject.Call<AndroidJavaObject>("setId", publisher.Id);
            jObject = jObject.Call<AndroidJavaObject>("setName", publisher.Name);
            jObject = jObject.Call<AndroidJavaObject>("setDomain", publisher.Domain);
            jObject = jObject.Call<AndroidJavaObject>("addCategories", GetArrayList(publisher.Categories));

            return jObject.Call<AndroidJavaObject>("build");
        }

        public static AndroidJavaObject GetTargetingParams(TargetingParams targetingParams)
        {
            var jObject = new AndroidJavaObject("io.bidmachine.TargetingParams");

            if (targetingParams == null) return jObject;

            jObject = jObject.Call<AndroidJavaObject>("setUserId", targetingParams.UserId);

            var jcGender = new AndroidJavaClass("io.bidmachine.utils.Gender");
            var jGender = jcGender.GetStatic<AndroidJavaObject>(targetingParams.UserGender.ToString());
            jObject = jObject.Call<AndroidJavaObject>("setGender", jGender);

            jObject = jObject.Call<AndroidJavaObject>("setBirthdayYear", GetObject(targetingParams.BirthdayYear));
            jObject = jObject.Call<AndroidJavaObject>("setKeywords", (object)targetingParams.Keywords);

            var location = targetingParams.DeviceLocation;
            if (location != null)
            {
                var jLocation = new AndroidJavaObject("android.location.Location", location.Provider);
                jLocation.Call("setLatitude", location.Latitude);
                jLocation.Call("setLongitude", location.Longitude);
                jObject = jObject.Call<AndroidJavaObject>("setDeviceLocation", jLocation);
            }

            jObject = jObject.Call<AndroidJavaObject>("setCountry", targetingParams.Country);

            jObject = jObject.Call<AndroidJavaObject>("setCity", targetingParams.City);

            jObject = jObject.Call<AndroidJavaObject>("setZip", targetingParams.Zip);

            jObject = jObject.Call<AndroidJavaObject>("setStoreUrl", targetingParams.StoreUrl);

            jObject = jObject.Call<AndroidJavaObject>("setStoreCategory", targetingParams.StoreCategory);

            jObject = jObject.Call<AndroidJavaObject>("setStoreSubCategories", (object)targetingParams.StoreSubCategories);

            jObject = jObject.Call<AndroidJavaObject>("setFramework", targetingParams.Framework);

            jObject = jObject.Call<AndroidJavaObject>("setPaid", GetObject(targetingParams.IsPaid));

            var externalUserIds = targetingParams.ExternalUserIds;
            if (externalUserIds != null)
            {
                var jArrayList = new AndroidJavaObject("java.util.ArrayList");
                foreach (var externalUserId in externalUserIds)
                {
                    var jExternalUserId = new AndroidJavaObject("io.bidmachine.ExternalUserId", externalUserId.SourceId, externalUserId.Value);
                    jArrayList.Call<bool>("add", jExternalUserId);
                }
                jObject = jObject.Call<AndroidJavaObject>("setExternalUserIds", jArrayList);
            }

            var blockedApplications = targetingParams.BlockedApplications;
            if (blockedApplications != null)
            {
                foreach (string app in blockedApplications)
                {
                    jObject = jObject.Call<AndroidJavaObject>("addBlockedApplication", app);
                }
            }

            var blockedCategories = targetingParams.BlockedCategories;
            if (blockedCategories != null)
            {
                foreach (string category in blockedCategories)
                {
                    jObject = jObject.Call<AndroidJavaObject>("addBlockedAdvertiserIABCategory", category);
                }
            }

            var blockedDomains = targetingParams.BlockedDomains;
            if (blockedDomains != null)
            {
                foreach (string domain in blockedDomains)
                {
                    jObject = jObject.Call<AndroidJavaObject>("addBlockedAdvertiserDomain", domain);
                }
            }

            return jObject;
        }
    }
}
#endif
