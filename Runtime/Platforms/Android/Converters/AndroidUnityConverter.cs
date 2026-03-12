#if UNITY_ANDROID || BIDMACHINE_DEV
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BidMachineInc.Ads.Api;

namespace BidMachineInc.Ads.Android
{
    internal static class AndroidUnityConverter
    {
        public static Dictionary<string, string> GetDictionary(AndroidJavaObject jObject)
        {
            var jcMap = new AndroidJavaObject("java.util.HashMap");
            var jcSet = new AndroidJavaObject("java.util.HashSet");

            IntPtr jptrKeySet = AndroidJNIHelper.GetMethodID(jcMap.GetRawClass(), "keySet", "()Ljava/util/Set;");
            IntPtr jptrSet = AndroidJNI.CallObjectMethod(jObject.GetRawObject(), jptrKeySet, new jvalue[] { });
            IntPtr jptrToArray = AndroidJNIHelper.GetMethodID(jcSet.GetRawClass(), "toArray", "()[Ljava/lang/Object;");
            IntPtr jptrArray = AndroidJNI.CallObjectMethod(jptrSet, jptrToArray, new jvalue[] { });

            var dict = new Dictionary<string, string>();
            string[] keys = AndroidJNIHelper.ConvertFromJNIArray<string[]>(jptrArray);

            IntPtr jptrGet = AndroidJNIHelper.GetMethodID(jcMap.GetRawClass(), "get", "(Ljava/lang/Object;)Ljava/lang/Object;");

            foreach (string key in keys)
            {
                string value = AndroidJNI.CallStringMethod(jObject.GetRawObject(), jptrGet, new jvalue[] { new() { l = AndroidJNI.NewStringUTF(key) } });
                dict.Add($"\"{key}\"", $"\"{value}\"");
            }

            return dict;
        }

        public static BannerSize GetBannerSize(AndroidJavaObject jBannerSize)
        {
            string size = jBannerSize.Call<string>("toString");
            return size switch
            {
                "Size_320x50" => BannerSize.Banner,
                "Size_300x250" => BannerSize.MediumRectangle,
                "Size_728x90" => BannerSize.Leaderboard,
                _ => BannerSize.Banner // Default case
            };
        }

        public static BMError GetError(AndroidJavaObject jObject)
        {
            return new BMError
            {
                Code = jObject.Call<int>("getCode"),
                Message = jObject.Call<string>("getMessage"),
            };
        }

        [Obsolete("Use GetAuctionResultObject(AndroidJavaObject) instead")]
        public static string GetAuctionResult(AndroidJavaObject jObject)
        {
            var jCustomParams = jObject.Call<AndroidJavaObject>("getCustomParams");
            var jAdDomains = jObject.Call<AndroidJavaObject>("getAdDomains");

            string deal = String.IsNullOrEmpty(jObject.Call<string>("getDeal"))
                ? "null"
                : jObject.Call<string>("getDeal").ToUpper();
            string demandSource = String.IsNullOrEmpty(jObject.Call<string>("getDemandSource"))
                ? "null"
                : jObject.Call<string>("getDemandSource");
            string cid = String.IsNullOrEmpty(jObject.Call<string>("getCid"))
                ? "null"
                : jObject.Call<string>("getCid");
            string customParams = String.Join(
                ",",
                GetDictionary(jCustomParams)
                    .Select(pair => $"{pair.Key.ToString()}:{pair.Value.ToString()}")
                    .ToArray()
            );
            string adDomains = String.Join(
                ",",
                AndroidJNIHelper
                    .ConvertFromJNIArray<string[]>(jAdDomains.GetRawObject())
                    .ToList()
                    .Select(adDomain => $"\"{adDomain}\"")
                    .ToList()
            );
            string creativeId = String.IsNullOrEmpty(jObject.Call<string>("getCreativeId"))
                ? "null"
                : jObject.Call<string>("getCreativeId");
            string id = String.IsNullOrEmpty(jObject.Call<string>("getId"))
                ? "null"
                : jObject.Call<string>("getId");
            string price = String.IsNullOrEmpty(jObject.Call<double>("getPrice").ToString())
                ? "null"
                : jObject.Call<double>("getPrice").ToString();

            return $"{{\"dealID\":\"{deal}\",\"demandSource\":\"{demandSource}\",\"cID\":\"{cid}\",\"customParams\":{{{customParams}}},\"adDomains\":[{adDomains}],\"creativeID\":\"{creativeId}\",\"bidID\":\"{id}\",\"price\":{price}}}";
        }

        public static AuctionResult GetAuctionResultObject(AndroidJavaObject jObject)
        {
            return new AuctionResult
            {
                BidId = jObject.Call<string>("getId"),
                DemandSource = jObject.Call<string>("getDemandSource"),
                Price = jObject.Call<double>("getPrice"),
                DealId = jObject.Call<string>("getDeal"),
                CreativeId = jObject.Call<string>("getCreativeId"),
                Cid = jObject.Call<string>("getCid"),
                CustomParams = new CustomParams(GetDictionary(jObject.Call<AndroidJavaObject>("getCustomParams"))),
                CustomExtras = new CustomExtras(GetDictionary(jObject.Call<AndroidJavaObject>("getNetworkParams"))),
            };
        }
    }
}
#endif
