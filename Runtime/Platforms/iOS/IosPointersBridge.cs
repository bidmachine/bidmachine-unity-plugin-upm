using System;
using System.Runtime.InteropServices;

namespace BidMachineInc.Ads.Ios
{
    public static class IosPointersBridge
    {
        [DllImport("__Internal")]
        private static extern void BidMachineReleasePointer(IntPtr ptr);

        public static void ReleasePointer(IntPtr ptr)
        {
            BidMachineReleasePointer(ptr);
        }
    }
}
