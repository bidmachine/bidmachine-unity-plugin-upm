using System;
using System.Runtime.InteropServices;
using UnityEngine;
using BidMachineInc.Ads.Api;

namespace BidMachineInc.Ads.Ios
{
    public class InterstitialRequestIosUnityBridge : IIosAdRequestBridge
    {
        [DllImport("__Internal")]
        private static extern IntPtr BidMachineInterstitialGetAuctionResultUnmanagedPointer();

        [DllImport("__Internal")]
        private static extern bool BidMachineInterstitialIsExpired();

        [DllImport("__Internal")]
        private static extern bool BidMachineInterstitialIsDestroyed();

        public AuctionResult GetAuctionResultObject()
        {
            string resultString = GetAuctionResult();
            var result = JsonUtility.FromJson<AuctionResultWrapper>(resultString);

            return result.ToAuctionResult();
        }

        public string GetAuctionResult()
        {
            IntPtr resultPtr = BidMachineInterstitialGetAuctionResultUnmanagedPointer();

            string resultString = Marshal.PtrToStringAuto(resultPtr);
            IosPointersBridge.ReleasePointer(resultPtr);

            return resultString;
        }

        public bool IsExpired()
        {
           return BidMachineInterstitialIsExpired();
        }

        public bool IsDestroyed()
        {
           return BidMachineInterstitialIsDestroyed();
        }
    }
}
