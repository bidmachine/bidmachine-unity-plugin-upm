// ReSharper disable CheckNamespace

using UnityEditor;
using UnityEngine;

namespace BidMachineInc.Ads.Editor
{
    internal static class BidMachineSettings
    {
        [MenuItem("BidMachine/SDK Documentation", false, 1)]
        public static void OpenDocumentation()
        {
            Application.OpenURL("https://developers.bidmachine.io/sdk/overview");
        }

        [MenuItem("BidMachine/Remove Plugin", false, 12)]
        public static void RemoveBidMachinePlugin()
        {
            RemoveHelper.RemovePlugin();
        }
    }
}
