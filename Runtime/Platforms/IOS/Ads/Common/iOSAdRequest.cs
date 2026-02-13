#if UNITY_IOS
using UnityEngine;
using System;
using BidMachineInc.Ads.Api;
using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.iOS
{
    public interface IiOSAdRequestBridge {
        public string GetAuctionResult();

        public AuctionResult GetAuctionResultObject();
        
        public bool IsDestroyed();

        public bool IsExpired();
    }

    public class iOSAdRequest<Bridge> : IAdRequest where Bridge : IiOSAdRequestBridge, new() {
        public readonly Bridge requestBridge;

        public iOSAdRequest() {
           requestBridge = new Bridge();
        }

        public string GetAuctionResult()
        {  
            return requestBridge.GetAuctionResult();
        }

        public AuctionResult GetAuctionResultObject()
        {
            return requestBridge.GetAuctionResultObject();
        }

        public bool IsDestroyed()
        {
            return requestBridge.IsDestroyed();
        }

        public bool IsExpired()
        {
            return requestBridge.IsExpired();
        }
    }
}
#endif