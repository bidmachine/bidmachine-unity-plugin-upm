#if UNITY_ANDROID || BIDMACHINE_DEV
using System;
using UnityEngine;
using UnityEngine.Scripting;
using BidMachineInc.Ads.Api;
using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.Android
{
    internal class AndroidAuctionRequestListener : AndroidCommonAdRequestListener<AuctionResult>
    {
        internal AndroidAuctionRequestListener(string className,
                                               ICommonAdRequestListener<IAdRequest, AuctionResult, BMError> listener,
                                               Func<AndroidJavaObject, IAdRequest> factory
        ) : base(className, listener, factory) { }

        [Preserve]
        public void onRequestSuccess(AndroidJavaObject request, AndroidJavaObject auctionResult)
        {
            listener.onRequestSuccess(factory(request), AndroidUnityConverter.GetAuctionResultObject(auctionResult));
        }
    }
}
#endif
