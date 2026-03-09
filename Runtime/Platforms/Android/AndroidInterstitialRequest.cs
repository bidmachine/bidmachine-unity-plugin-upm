using UnityEngine;
using BidMachineInc.Ads.Api;
using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.Android
{
    internal class AndroidInterstitialRequest : IAdRequest
    {
        public AndroidJavaObject JavaObject { get; }

        public AndroidInterstitialRequest(AndroidJavaObject javaObject)
        {
            JavaObject = javaObject;
        }

        public string GetAuctionResult()
        {
            return AndroidUnityConverter.GetAuctionResult(JavaObject.Call<AndroidJavaObject>("getAuctionResult"));
        }

        public AuctionResult GetAuctionResultObject()
        {
            return AndroidUnityConverter.GetAuctionResultObject(JavaObject.Call<AndroidJavaObject>("getAuctionResult"));
        }

        public bool IsDestroyed()
        {
            return JavaObject.Call<bool>("isDestroyed");
        }

        public bool IsExpired()
        {
            return JavaObject.Call<bool>("isExpired");
        }
    }
}
