#if PLATFORM_ANDROID
using UnityEngine;
using BidMachineInc.Ads.Common;
using BidMachineInc.Ads.Api;

namespace BidMachineInc.Ads.Android
{
    internal class AndroidRewardedRequest : IAdRequest
    {
        private readonly AndroidJavaObject jObject;

        public AndroidJavaObject JavaObject => jObject;

        public AndroidRewardedRequest(AndroidJavaObject javaObject)
        {
            jObject = javaObject;
        }

        public string GetAuctionResult()
        {
            return AndroidUnityConverter.GetAuctionResult(
                jObject.Call<AndroidJavaObject>("getAuctionResult")
            );
        }

        public AuctionResult GetAuctionResultObject()
        {
            return AndroidUnityConverter.GetAuctionResultObject(
                jObject.Call<AndroidJavaObject>("getAuctionResult")
            );
        }

        public bool IsDestroyed()
        {
            return jObject.Call<bool>("isDestroyed");
        }

        public bool IsExpired()
        {
            return jObject.Call<bool>("isExpired");
        }
    }
}
#endif
