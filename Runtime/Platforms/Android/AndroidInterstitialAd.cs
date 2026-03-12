#if UNITY_ANDROID || BIDMACHINE_DEV
using System;
using UnityEngine;
using BidMachineInc.Ads.Api;
using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.Android
{
    internal class AndroidInterstitialAd : IInterstitialAd
    {
        private readonly AndroidJavaObject _jObject;

        private readonly Func<AndroidJavaObject, IInterstitialAd> _adFactory;

        public AndroidInterstitialAd() : this(new AndroidJavaObject(AndroidConsts.InterstitialAdClassName, AndroidNativeConverter.GetActivity())) { }

        public AndroidInterstitialAd(AndroidJavaObject javaObject)
        {
            _jObject = javaObject;
            _adFactory = delegate(AndroidJavaObject ad)
            {
                return new InterstitialAd(new AndroidInterstitialAd(ad));
            };
        }

        public void Show()
        {
            _jObject.Call("show");
        }

        public bool CanShow()
        {
            return _jObject.Call<bool>("canShow");
        }

        public void Destroy()
        {
            _jObject.Call("destroy");
        }

        public void Load(IAdRequest request)
        {
            if (request == null) return;

            _jObject.Call<AndroidJavaObject>("load", ((AndroidInterstitialRequest)request).JavaObject);
        }

        public void SetListener(IInterstitialAdListener listener)
        {
            if (listener == null) return;

            _jObject.Call<AndroidJavaObject>("setListener", new AndroidInterstitialAdListener(AndroidConsts.InterstitialListenerClassName, listener, _adFactory));
        }
    }
}
#endif
