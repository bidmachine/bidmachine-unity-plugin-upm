using BidMachineInc.Ads.Common;

namespace BidMachineInc.Ads.Api
{
    public sealed class BidMachine
    {
        public const string UnityPluginVersion = "3.6.0";

        public const int BannerHorizontalCenter = 1;
        public const int BannerHorizontalLeft = 3;
        public const int BannerHorizontalRight = 5;

        public const int BannerVerticalCenter = 16;
        public const int BannerVerticalTop = 48;
        public const int BannerVerticalBottom = 80;

        private static IBidMachine _client;

        private static IBidMachine GetInstance()
        {
            return _client ??= BidMachineClientFactory.GetBidMachine();
        }

        /// <summary>
        /// Initializes BidMachine SDK.
        /// </summary>
        /// <param name="id">your seller id.</param>
        public static void Initialize(string id)
        {
            GetInstance().Initialize(id);
        }

        /// <summary>
        /// Checks if BidMachine SDK was initialized.
        /// </summary>
        /// <returns><c>true</c> if BidMachine SDK was already initialized.</returns>
        public static bool IsInitialized()
        {
            return GetInstance().IsInitialized();
        }

        /// <summary>
        /// Sets BidMachine SDK endpoint.
        /// </summary>
        /// <param name="url">BidMachine endpoint URL.</param>
        public static void SetEndpoint(string url)
        {
            GetInstance().SetEndpoint(url);
        }

        /// <summary>
        /// Sets BidMachine SDK logs enabled.
        /// </summary>
        /// <param name="enabled">if <c>true</c> SDK will print all information about ad requests.</param>
        public static void SetLoggingEnabled(bool enabled)
        {
            GetInstance().SetLoggingEnabled(enabled);
        }

        /// <summary>
        /// Sets BidMachine SDK test mode.
        /// </summary>
        /// <param name="testMode">if <c>true</c> SDK will run in test mode.</param>
        public static void SetTestMode(bool testMode)
        {
            GetInstance().SetTestMode(testMode);
        }

        /// <summary>
        /// Sets default targeting params for all ad requests.
        /// </summary>
        /// <param name="targetingParams">an object of type <see cref="TargetingParams"/>.</param>
        public static void SetTargetingParams(TargetingParams targetingParams)
        {
            GetInstance().SetTargetingParams(targetingParams);
        }

        /// <summary>
        /// Sets consent config.
        /// </summary>
        /// <param name="consent">user has given consent to the processing of personal data relating to him or her. https://www.eugdpr.org/</param>
        /// <param name="consentString">GDPR consent string if applicable, complying with the IAB standard:
        /// <a href="https://github.com/InteractiveAdvertisingBureau/GDPR-Transparency-and-Consent-Framework/blob/master/Consent%20string%20and%20vendor%20list%20formats%20v1.1%20Final.md">Consent String Format</a>
        /// in the <a href="https://github.com/InteractiveAdvertisingBureau/GDPR-Transparency-and-Consent-Framework">Transparency and Consent Framework</a> technical specifications.</param>
        public static void SetConsentConfig(bool consent, string consentString)
        {
            GetInstance().SetConsentConfig(consent, consentString);
        }

        /// <summary>
        /// Sets subject to GDPR.
        /// </summary>
        /// <param name="subject">flag indicating if GDPR regulations should be applied.
        /// <a href="https://wikipedia.org/wiki/General_Data_Protection_Regulation">The General Data Protection Regulation (GDPR)</a> is a regulation of the European Union.</param>
        public static void SetSubjectToGDPR(bool subject)
        {
            GetInstance().SetSubjectToGDPR(subject);
        }

        /// <summary>
        /// Sets subject to COPPA.
        /// </summary>
        /// <param name="coppa">flag indicating if COPPA regulations should be applied.
        /// <a href="https://wikipedia.org/wiki/Children%27s_Online_Privacy_Protection_Act">The Children's Online Privacy Protection Act (COPPA)</a> was established by the U.S. Federal Trade Commission.</param>
        public static void SetCoppa(bool coppa)
        {
            GetInstance().SetCoppa(coppa);
        }

        /// <summary>
        /// Sets US Privacy string.
        /// </summary>
        /// <param name="usPrivacyString">CCPA string if applicable, complying with the IAB standard:
        /// <a href="https://github.com/InteractiveAdvertisingBureau/USPrivacy/blob/master/CCPA/US%20Privacy%20String.md">CCPA String Format</a></param>
        public static void SetUSPrivacyString(string usPrivacyString)
        {
            GetInstance().SetUSPrivacyString(usPrivacyString);
        }

        /// <summary>
        /// Sets GPP string.
        /// </summary>
        /// <param name="gppString">GPP string if applicable, complying with the IAB standard:
        /// <a href="https://github.com/InteractiveAdvertisingBureau/Global-Privacy-Platform/blob/main/Core/Consent%20String%20Specification.md">GPP String Format</a></param>
        /// <param name="gppIds">GPP ids.</param>
        public static void SetGPP(string gppString, int[] gppIds)
        {
            GetInstance().SetGPP(gppString, gppIds);
        }

        /// <summary>
        /// Sets publisher information.
        /// </summary>
        /// <param name="publisher">an object of type <see cref="Publisher"/> which contains all information about the publisher.</param>
        public static void SetPublisher(Publisher publisher)
        {
            GetInstance().SetPublisher(publisher);
        }

        // Deprecated aliases
        [System.Obsolete("Use BidMachine.UnityPluginVersion instead.")]
        public static string BIDMACHINE_UNITY_PLUGIN_VERSION = UnityPluginVersion;
        [System.Obsolete("Use BidMachine.BannerHorizontalCenter instead.")]
        public static int BANNER_HORIZONTAL_CENTER = BannerHorizontalCenter;
        [System.Obsolete("Use BidMachine.BannerHorizontalLeft instead.")]
        public static int BANNER_HORIZONTAL_LEFT = BannerHorizontalLeft;
        [System.Obsolete("Use BidMachine.BannerHorizontalRight instead.")]
        public static int BANNER_HORIZONTAL_RIGHT = BannerHorizontalRight;
        [System.Obsolete("Use BidMachine.BannerVerticalCenter instead.")]
        public static int BANNER_VERTICAL_CENTER = BannerVerticalCenter;
        [System.Obsolete("Use BidMachine.BannerVerticalTop instead.")]
        public static int BANNER_VERTICAL_TOP = BannerVerticalTop;
        [System.Obsolete("Use BidMachine.BannerVerticalBottom instead.")]
        public static int BANNER_VERTICAL_BOTTOM = BannerVerticalBottom;
    }
}
