#if PLATFORM_ANDROID
using System;
using BidMachineInc.Ads.Api;
using BidMachineInc.Ads.Common;
using UnityEngine;

namespace BidMachineInc.Ads.Android
{
    internal class AndroidAuctionRequestListener : AndroidCommonAdRequestListener<AuctionResult>
    {
        internal AndroidAuctionRequestListener(
            string className,
            ICommonAdRequestListener<IAdRequest, AuctionResult, BMError> listener,
            Func<AndroidJavaObject, IAdRequest> factory
        )
            : base(className, listener, factory) { }

        public void onRequestSuccess(AndroidJavaObject request, AndroidJavaObject auctionResult)
        {
            listener.onRequestSuccess(
                factory(request),
                AndroidUnityConverter.GetAuctionResultObject(auctionResult)
            );
        }
    }
}
#endif
