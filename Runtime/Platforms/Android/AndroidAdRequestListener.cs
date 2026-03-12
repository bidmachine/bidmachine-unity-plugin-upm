#if UNITY_ANDROID || BIDMACHINE_DEV
using System;
using UnityEngine;
using UnityEngine.Scripting;
using BidMachineInc.Ads.Api;
using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.Android
{
    internal class AndroidAdRequestListener : AndroidCommonAdRequestListener<string>
    {
        internal AndroidAdRequestListener(string className,
                                          ICommonAdRequestListener<IAdRequest, string, BMError> listener,
                                          Func<AndroidJavaObject, IAdRequest> factory
        ) : base(className, listener, factory) { }

        [Preserve]
        public void onRequestSuccess(AndroidJavaObject request, AndroidJavaObject auctionResult)
        {
            listener.onRequestSuccess(factory(request), AndroidUnityConverter.GetAuctionResult(auctionResult));
        }
    }
}
#endif
