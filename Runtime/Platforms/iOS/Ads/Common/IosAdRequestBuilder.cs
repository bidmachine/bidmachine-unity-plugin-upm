#if UNITY_IOS || BIDMACHINE_DEV
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;
using BidMachineInc.Ads.Api;
using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.Ios
{
    using PriceFloorList = KeyValueList<string, double>;
    using CustomParamsList = KeyValueList<string, string>;

    public interface IIosAdRequestBuilderBridge
    {
        public void Build();

        public void SetPriceFloorParams(string jsonString);

        public void SetPlacementId(string placementId);

        public void SetBidPayload(string bidPayload);

        public void SetLoadingTimeOut(int loadingTimeout);

        public void SetNetworks(string networks);

        public void SetCustomParams(string jsonString);

        public void SetAdContentType(string contentType);

        public void SetAdRequestDelegate(AdRequestSuccessCallback onSuccess, AdRequestFailedCallback onFailed, AdRequestExpiredCallback onExpired);
    }

    public class IosAdRequestBuilder<TBridge, TRequest> : IAdRequestBuilder
        where TBridge : IIosAdRequestBuilderBridge, new()
        where TRequest: IAdRequest, new()
    {

        private static IAdRequestListener RequestListener;
        private static IAdAuctionRequestListener AuctionListener;

        protected readonly TBridge RequestBuilderBridge;

        protected IosAdRequestBuilder()
        {
           RequestBuilderBridge = new TBridge();
        }

        public IAdRequest Build()
        {
            RequestBuilderBridge.Build();
            return new TRequest();
        }

        public IAdRequestBuilder SetAdContentType(AdContentType contentType)
        {
            string contentTypeString = contentType.ToString();
            if (!Enum.IsDefined(typeof(AdContentType), contentType))
            {
                contentTypeString = nameof(AdContentType.All);
            }
            RequestBuilderBridge.SetAdContentType(contentTypeString);
            return this;
        }

        public IAdRequestBuilder SetBidPayload(string bidPayload)
        {
            RequestBuilderBridge.SetBidPayload(bidPayload);
            return this;
        }

        public IAdRequestBuilder SetLoadingTimeOut(int loadingTimeout)
        {
            RequestBuilderBridge.SetLoadingTimeOut(loadingTimeout);
            return this;
        }

        public IAdRequestBuilder SetNetworks(string networks)
        {
            RequestBuilderBridge.SetNetworks(networks);
            return this;
        }

        public IAdRequestBuilder SetPlacementId(string placementId)
        {
            RequestBuilderBridge.SetPlacementId(placementId);
            return this;
        }

        public IAdRequestBuilder SetPriceFloorParams(PriceFloorParams priceFloorParams)
        {
            var list = new PriceFloorList(priceFloorParams.PriceFloors);
            string jsonString = JsonUtility.ToJson(list);

            RequestBuilderBridge.SetPriceFloorParams(jsonString);
            return this;
        }

        public IAdRequestBuilder SetTargetingParams(TargetingParams targetingParams)
        {
            // TODO: Delete from interface?
            return this;
        }

        public IAdRequestBuilder SetCustomParams(CustomParams customParams)
        {
            var list = new CustomParamsList(customParams.Params);
            string jsonString = JsonUtility.ToJson(list);

            RequestBuilderBridge.SetCustomParams(jsonString);
            return this;
        }

        public IAdRequestBuilder SetListener(IAdRequestListener listener)
        {
            RequestListener = listener;
            SetAdRequestDelegate();

            return this;
        }

        public IAdRequestBuilder SetListener(IAdAuctionRequestListener listener)
        {
            AuctionListener = listener;
            SetAdRequestDelegate();

            return this;
        }

        [MonoPInvokeCallback(typeof(AdRequestSuccessCallback))]
        private static void DidLoadRequest(IntPtr ad, IntPtr auctionResultUnmanagedPointer)
        {
            if (!HasAnyListener())
            {
                IosPointersBridge.ReleasePointer(auctionResultUnmanagedPointer);
                return;
            }

            string auctionString = Marshal.PtrToStringAuto(auctionResultUnmanagedPointer);
            var request = new TRequest();

            RequestListener?.onRequestSuccess(request, auctionString);
            if (AuctionListener != null)
            {
                var auctionResultWrapped = JsonUtility.FromJson<AuctionResultWrapper>(auctionString);
                var auctionResult = auctionResultWrapped.ToAuctionResult();

                AuctionListener.onRequestSuccess(request, auctionResult);
            }

            IosPointersBridge.ReleasePointer(auctionResultUnmanagedPointer);
        }

        [MonoPInvokeCallback(typeof(AdRequestFailedCallback))]
        private static void DidFailRequest(IntPtr error)
        {
            if (!HasAnyListener()) return;

            var request = new TRequest();
            var bmError = new BMError
            {
                Code = IosErrorBridge.GetErrorCode(error),
                Message = IosErrorBridge.GetErrorMessage(error)
            };

            RequestListener?.onRequestFailed(request, bmError);
            AuctionListener?.onRequestFailed(request, bmError);
        }

        [MonoPInvokeCallback(typeof(AdRequestExpiredCallback))]
        private static void DidExpireRequest(IntPtr ad)
        {
            if (!HasAnyListener()) return;

            var request = new TRequest();

            RequestListener?.onRequestExpired(request);
            AuctionListener?.onRequestExpired(request);
        }

        private void SetAdRequestDelegate()
        {
            RequestBuilderBridge.SetAdRequestDelegate(DidLoadRequest, DidFailRequest, DidExpireRequest);
        }

        private static bool HasAnyListener()
        {
            return AuctionListener != null || RequestListener != null;
        }
    }

    [Serializable]
    internal class KeyValue<TK, TV>
    {
        public TK key;
        public TV value;

        public KeyValue(TK key, TV value)
        {
            this.key = key;
            this.value = value;
        }
    }

    [Serializable]
    internal class KeyValueList<TK, TV>
    {
        public List<KeyValue<TK, TV>> items = new();

        public KeyValueList(Dictionary<TK, TV> dict)
        {
            foreach (var kvp in dict)
            {
                items.Add(new KeyValue<TK, TV>(kvp.Key, kvp.Value));
            }
        }
    }
}
#endif
