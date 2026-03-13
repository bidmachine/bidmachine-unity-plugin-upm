#if UNITY_IOS || BIDMACHINE_DEV
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using BidMachineInc.Ads.Api;

namespace BidMachineInc.Ads.Ios
{
    public class BannerRequestIosUnityBridge : IIosAdRequestBridge
    {
        [DllImport("__Internal")]
        private static extern IntPtr BidMachineBannerGetAuctionResultUnmanagedPointer();

        [DllImport("__Internal")]
        private static extern bool BidMachineBannerIsExpired();

        [DllImport("__Internal")]
        private static extern bool BidMachineBannerIsDestroyed();

        [DllImport("__Internal")]
        private static extern int BidMachineBannerGetSize();

        [DllImport("__Internal")]
        private static extern int BidMachineBannerGetAdSize();

        public AuctionResult GetAuctionResultObject()
        {
            string resultString = GetAuctionResult();
            var result = JsonUtility.FromJson<AuctionResultWrapper>(resultString);

            return result.ToAuctionResult();
        }

        public string GetAuctionResult()
        {
            IntPtr resultPtr = BidMachineBannerGetAuctionResultUnmanagedPointer();

            string resultString = Marshal.PtrToStringAuto(resultPtr);
            IosPointersBridge.ReleasePointer(resultPtr);

            return resultString;
        }

        public bool IsExpired()
        {
           return BidMachineBannerIsExpired();
        }

        public bool IsDestroyed()
        {
           return BidMachineBannerIsDestroyed();
        }

        public BannerSize GetSize()
        {
            int rawValue = BidMachineBannerGetSize();

            return rawValue switch
            {
                0 => BannerSize.Banner,
                1 => BannerSize.MediumRectangle,
                2 => BannerSize.Leaderboard,
                _ => BannerSize.Banner
            };
        }

        public BannerAdSize GetBannerAdSize()
        {
            int rawValue = BidMachineBannerGetAdSize();

            return rawValue switch
            {
                0 => BannerAdSize.Banner,
                1 => BannerAdSize.MediumRectangle,
                2 => BannerAdSize.Leaderboard,
                _ => BannerAdSize.Banner,
            };
        }
    }
}
#endif
