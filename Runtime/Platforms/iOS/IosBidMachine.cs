#if UNITY_IOS || BIDMACHINE_DEV
using UnityEngine;
using BidMachineInc.Ads.Api;
using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.Ios
{
    public class IosBidMachine : IBidMachine
    {
        private BidMachineIosUnityBridge _bridge;

        private BidMachineIosUnityBridge Bridge()
        {
            return _bridge ??= new BidMachineIosUnityBridge();
        }

        public void Initialize(string sellerId)
        {
            Bridge().Initialize(sellerId);
        }

        public bool IsInitialized()
        {
            return Bridge().IsInitialized();
        }

        public void SetEndpoint(string url)
        {
            Bridge().SetEndpoint(url);
        }

        public void SetLoggingEnabled(bool logging)
        {
            Bridge().SetLoggingEnabled(logging);
        }

        public void SetTestMode(bool test)
        {
            Bridge().SetTestMode(test);
        }

        public void SetTargetingParams(TargetingParams targetingParams)
        {
            var parameters = IosTargetingAdapter.Adapt(targetingParams);
            string jsonString = JsonUtility.ToJson(parameters);

            Bridge().SetTargetingParams(jsonString);
        }

        public void SetConsentConfig(bool consent, string consentConfig)
        {
            Bridge().SetConsentConfig(consent, consentConfig);
        }

        public void SetSubjectToGDPR(bool subjectToGdpr)
        {
            Bridge().SetSubjectToGdpr(subjectToGdpr);
        }

        public void SetCoppa(bool coppa)
        {
            Bridge().SetCoppa(coppa);
        }

        public void SetUSPrivacyString(string usPrivacyString)
        {
            Bridge().SetUsPrivacyString(usPrivacyString);
        }

        public void SetGPP(string gppString, int[] gppIds)
        {
            Bridge().SetGpp(gppString, gppIds);
        }

        public void SetPublisher(Publisher publisher)
        {
            var iosPublisher = IosPublisherAdapter.Adapt(publisher);
            string jsonString = JsonUtility.ToJson(iosPublisher);
            Bridge().SetPublisher(jsonString);
        }
    }
}
#endif
