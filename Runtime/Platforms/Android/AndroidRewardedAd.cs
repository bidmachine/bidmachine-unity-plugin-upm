#if UNITY_ANDROID || BIDMACHINE_DEV
using System;
using UnityEngine;
using BidMachineInc.Ads.Api;
using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.Android
{
    internal class AndroidRewardedAd : IRewardedAd
    {
        private readonly AndroidJavaObject _jObject;

        private readonly Func<AndroidJavaObject, IRewardedAd> _adFactory;

        public AndroidRewardedAd() : this(new AndroidJavaObject(AndroidConsts.RewardedAdClassName, AndroidNativeConverter.GetActivity())) { }

        public AndroidRewardedAd(AndroidJavaObject javaObject)
        {
            _jObject = javaObject;
            _adFactory = delegate(AndroidJavaObject ad)
            {
                return new RewardedAd(new AndroidRewardedAd(ad));
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

            _jObject.Call<AndroidJavaObject>("load", ((AndroidRewardedRequest)request).JavaObject);
        }

        public void SetListener(IRewardedAdListener listener)
        {
            if (listener == null) return;

            _jObject.Call<AndroidJavaObject>("setListener", new AndroidRewardedAdListener(AndroidConsts.RewardedListenerClassName, listener, _adFactory));
        }
    }
}
#endif
