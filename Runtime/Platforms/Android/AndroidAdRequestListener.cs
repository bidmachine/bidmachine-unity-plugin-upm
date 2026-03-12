#if UNITY_ANDROID || BIDMACHINE_DEV
using System;
using UnityEngine;
using BidMachineInc.Ads.Api;
using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.Android
{
    [Obsolete("Use AndroidAuctionRequestListener instead")]
    internal class AndroidAdRequestListener : AndroidCommonAdRequestListener<string>
    {
        internal AndroidAdRequestListener(string className,
                                          ICommonAdRequestListener<IAdRequest, string, BMError> listener,
                                          Func<AndroidJavaObject, IAdRequest> factory
        ) : base(className, listener, factory) { }

        public void onRequestSuccess(AndroidJavaObject request, AndroidJavaObject auctionResult)
        {
            listener.onRequestSuccess(factory(request), AndroidUnityConverter.GetAuctionResult(auctionResult));
        }
    }
}
#endif
