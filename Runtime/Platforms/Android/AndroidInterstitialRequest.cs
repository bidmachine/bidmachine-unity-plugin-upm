#if PLATFORM_ANDROID
using BidMachineInc.Ads.Api;
using BidMachineInc.Ads.Common;
using UnityEngine;

namespace BidMachineInc.Ads.Android
{
    internal class AndroidInterstitialRequest : IAdRequest
    {
        private readonly AndroidJavaObject jObject;

        public AndroidJavaObject JavaObject => jObject;

        public AndroidInterstitialRequest(AndroidJavaObject javaObject) =>
            this.jObject = javaObject;

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
