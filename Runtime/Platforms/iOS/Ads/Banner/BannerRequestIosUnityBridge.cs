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
    }
}
