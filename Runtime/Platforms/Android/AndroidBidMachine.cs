using System;
using UnityEngine;
using BidMachineInc.Ads.Api;
using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.Android
{
    internal class AndroidBidMachine : IBidMachine
    {
        private AndroidJavaClass _jClass;

        private AndroidJavaClass GetBidMachineClass()
        {
            return _jClass ??= new AndroidJavaClass("io.bidmachine.BidMachine");
        }

        public void Initialize(string sellerId)
        {
            if (String.IsNullOrEmpty(sellerId)) return;

            GetBidMachineClass().CallStatic("initialize", AndroidNativeConverter.GetActivity(), sellerId);
        }

        public bool IsInitialized()
        {
            return GetBidMachineClass().CallStatic<bool>("isInitialized");
        }

        public void SetEndpoint(string url)
        {
            if (String.IsNullOrEmpty(url)) return;

            GetBidMachineClass().CallStatic("setEndpoint", url);
        }

        public void SetLoggingEnabled(bool logging)
        {
            GetBidMachineClass().CallStatic("setLoggingEnabled", logging);
        }

        public void SetTestMode(bool testMode)
        {
            GetBidMachineClass().CallStatic("setTestMode", testMode);
        }

        public void SetTargetingParams(TargetingParams targetingParams)
        {
            if (targetingParams == null) return;

            GetBidMachineClass().CallStatic("setTargetingParams", AndroidNativeConverter.GetTargetingParams(targetingParams));
        }

        public void SetConsentConfig(bool consent, string consentConfig)
        {
            if (String.IsNullOrEmpty(consentConfig)) return;

            GetBidMachineClass().CallStatic("setConsentConfig", consent, consentConfig);
        }

        public void SetSubjectToGDPR(bool subjectToGdpr)
        {
            GetBidMachineClass().CallStatic("setSubjectToGDPR", AndroidNativeConverter.GetObject(subjectToGdpr));
        }

        public void SetCoppa(bool coppa)
        {
            GetBidMachineClass().CallStatic("setCoppa", AndroidNativeConverter.GetObject(coppa));
        }

        public void SetUSPrivacyString(string usPrivacyString)
        {
            if (String.IsNullOrEmpty(usPrivacyString)) return;

            GetBidMachineClass().CallStatic("setUSPrivacyString", usPrivacyString);
        }

        public void SetGPP(string gppString, int[] gppIds)
        {
            if (String.IsNullOrEmpty(gppString)) return;

            var clientGppIds = AndroidNativeConverter.GetArrayList(gppIds);

            GetBidMachineClass().CallStatic("setGPP", gppString, clientGppIds);
        }

        public void SetPublisher(Publisher publisher)
        {
            if (publisher == null) return;

            GetBidMachineClass().CallStatic("setPublisher", AndroidNativeConverter.GetPublisher(publisher));
        }
    }
}
