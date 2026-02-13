#if UNITY_IOS
using System;
using System.Runtime.InteropServices;

namespace BidMachineInc.Ads.iOS
{
    public class iOSPointersBridge
    {
        [DllImport("__Internal")]
        private static extern void BidMachineReleasePointer(IntPtr ptr);

        public static void ReleasePointer(IntPtr ptr)
        {
            BidMachineReleasePointer(ptr);
        }
    }
}
#endif
