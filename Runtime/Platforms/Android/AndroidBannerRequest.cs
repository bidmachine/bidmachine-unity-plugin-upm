#if PLATFORM_ANDROID
using UnityEngine;
using BidMachineInc.Ads.Api;
using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.Android
{
    internal class AndroidBannerRequest : IBannerRequest
    {
        private readonly AndroidJavaObject jObject;

        public AndroidJavaObject JavaObject => jObject;

        public AndroidBannerRequest(AndroidJavaObject jObject)
        {
            this.jObject = jObject;
        }

        public BannerSize GetSize()
        {
            return AndroidUnityConverter.GetBannerSize(jObject.Call<AndroidJavaObject>("getSize"));
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
