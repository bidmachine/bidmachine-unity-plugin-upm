using System;
using UnityEngine;
using BidMachineInc.Ads.Api;
using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.Android
{
    internal class AndroidAdRequestBuilder : IAdRequestBuilder
    {
        private readonly string _listenerClassName;

        private readonly Func<AndroidJavaObject, IAdRequest> _factory;

        public AndroidJavaObject JavaObject { get; }

        public AndroidAdRequestBuilder(string className, string listenerClassName, Func<AndroidJavaObject, IAdRequest> factory)
        {
            JavaObject = new AndroidJavaObject(className);
            _listenerClassName = listenerClassName;
            _factory = factory;
        }

        public IAdRequestBuilder SetAdContentType(AdContentType adContentType)
        {
            string contentTypeString = adContentType.ToString();
            if (!Enum.IsDefined(typeof(AdContentType), adContentType))
            {
                contentTypeString = nameof(AdContentType.All);
            }

            var jcAdContentType = new AndroidJavaClass("io.bidmachine.AdContentType");
            var jAdContentType = jcAdContentType.GetStatic<AndroidJavaObject>(contentTypeString);

            JavaObject.Call<AndroidJavaObject>("setAdContentType", jAdContentType);

            return this;
        }

        public IAdRequestBuilder SetBidPayload(string bidPayLoad)
        {
            if (String.IsNullOrEmpty(bidPayLoad))
            {
                return this;
            }

            JavaObject.Call<AndroidJavaObject>("setBidPayload", AndroidNativeConverter.GetObject(bidPayLoad));

            return this;
        }

        public IAdRequestBuilder SetListener(IAdRequestListener listener)
        {
            if (listener == null) return this;

            JavaObject.Call<AndroidJavaObject>("setListener", new AndroidAdRequestListener(_listenerClassName, listener, _factory));

            return this;
        }

        public IAdRequestBuilder SetListener(IAdAuctionRequestListener listener)
        {
            if (listener == null) return this;

            JavaObject.Call<AndroidJavaObject>("setListener", new AndroidAuctionRequestListener(_listenerClassName, listener, _factory));

            return this;
        }

        public IAdRequestBuilder SetLoadingTimeOut(int loadingTimeout)
        {
            JavaObject.Call<AndroidJavaObject>("setLoadingTimeOut", AndroidNativeConverter.GetObject(loadingTimeout));

            return this;
        }

        public IAdRequestBuilder SetNetworks(string networks)
        {
            if (String.IsNullOrEmpty(networks))
            {
                return this;
            }

            string[] networksArray = networks.Split(',');
            if (networksArray.Length == 0)
            {
                return this;
            }

            string networksJson = JsonUtility.ToJson(networksArray);
            if (String.IsNullOrEmpty(networksJson))
            {
                return this;
            }

            JavaObject.Call<AndroidJavaObject>("setNetworks", AndroidNativeConverter.GetObject(networksJson));

            return this;
        }

        public IAdRequestBuilder SetPlacementId(string placementId)
        {
            if (String.IsNullOrEmpty(placementId)) return this;

            JavaObject.Call<AndroidJavaObject>("setPlacementId", AndroidNativeConverter.GetObject(placementId));

            return this;
        }

        public IAdRequestBuilder SetPriceFloorParams(PriceFloorParams priceFloorParams)
        {
            if (priceFloorParams == null) return this;

            JavaObject.Call<AndroidJavaObject>("setPriceFloorParams", AndroidNativeConverter.GetPriceFloorParams(priceFloorParams));

            return this;
        }

        public IAdRequestBuilder SetCustomParams(CustomParams customParams)
        {
            if (customParams == null) return this;

            JavaObject.Call<AndroidJavaObject>("setCustomParams", AndroidNativeConverter.GetCustomParams(customParams));

            return this;
        }

        public IAdRequestBuilder SetTargetingParams(TargetingParams targetingParams)
        {
            if (targetingParams == null) return this;

            JavaObject.Call<AndroidJavaObject>("setTargetingParams", AndroidNativeConverter.GetTargetingParams(targetingParams));

            return this;
        }

        public IAdRequest Build()
        {
            return _factory(JavaObject.Call<AndroidJavaObject>("build"));
        }
    }
}
