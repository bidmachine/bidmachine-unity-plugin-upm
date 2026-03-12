#if UNITY_ANDROID || BIDMACHINE_DEV
using UnityEngine;
using BidMachineInc.Ads.Api;
using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.Android
{
    internal class AndroidBannerView : IBannerView
    {
        private readonly AndroidJavaObject _jObject;

        private AndroidJavaClass _jcBannerShowHelper;
        private AndroidJavaObject _joBannerShowHelper;

        public AndroidBannerView()
        {
            _jObject = new AndroidJavaObject(AndroidConsts.BannerViewClassName, AndroidNativeConverter.GetActivity());
        }

        public AndroidBannerView(AndroidJavaObject javaObject) => _jObject = javaObject;

        public bool Show(int yAxis, int xAxis, IBannerView view, BannerSize size)
        {
            var jSize = AndroidNativeConverter.GetBannerSize(size);
            return GetBannerShowHelper()
                .Call<bool>(
                    "show",
                    AndroidNativeConverter.GetActivity(),
                    _jObject,
                    jSize,
                    xAxis,
                    yAxis
                );
        }

        public void Hide()
        {
            GetBannerShowHelper().Call("hide");
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
            _jObject.Call<AndroidJavaObject>("load", ((AndroidBannerRequest)request).JavaObject);
        }

        public void SetListener(IAdListener<IBannerView> listener)
        {
            if (listener == null) return;

            _jObject.Call<AndroidJavaObject>(
                "setListener",
                new AndroidAdListener<IBannerView, IAdListener<IBannerView>>(
                    AndroidConsts.BannerListenerClassName,
                    listener,
                    delegate(AndroidJavaObject ad)
                    {
                        return new BannerView(new AndroidBannerView(ad));
                    }
                )
            );
        }

        private AndroidJavaObject GetBannerShowHelper()
        {
            _jcBannerShowHelper ??= new AndroidJavaClass("io.bidmachine.ads.extensions.unity.banner.BannerShowHelper");
            _joBannerShowHelper ??= _jcBannerShowHelper.CallStatic<AndroidJavaObject>("get");

            return _joBannerShowHelper;
        }
    }
}
#endif
