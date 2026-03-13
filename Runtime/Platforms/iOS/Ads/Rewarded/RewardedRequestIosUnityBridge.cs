#if UNITY_IOS || BIDMACHINE_DEV
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using BidMachineInc.Ads.Api;

namespace BidMachineInc.Ads.Ios
{
    public class RewardedRequestIosUnityBridge : IIosAdRequestBridge
    {
        [DllImport("__Internal")]
        private static extern IntPtr BidMachineRewardedGetAuctionResultUnmanagedPointer();

        [DllImport("__Internal")]
        private static extern bool BidMachineRewardedIsExpired();

        [DllImport("__Internal")]
        private static extern bool BidMachineRewardedIsDestroyed();

        public AuctionResult GetAuctionResultObject()
        {
            string resultString = GetAuctionResult();
            var result = JsonUtility.FromJson<AuctionResultWrapper>(resultString);

            return result.ToAuctionResult();
        }

        public string GetAuctionResult()
        {
            IntPtr resultPtr = BidMachineRewardedGetAuctionResultUnmanagedPointer();

            string resultString = Marshal.PtrToStringAuto(resultPtr);
            IosPointersBridge.ReleasePointer(resultPtr);

            return resultString;
        }

        public bool IsExpired()
        {
           return BidMachineRewardedIsExpired();
        }

        public bool IsDestroyed()
        {
           return BidMachineRewardedIsDestroyed();
        }
    }
}
#endif
