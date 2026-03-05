// ReSharper disable CheckNamespace

using UnityEditor;
using UnityEditor.PackageManager;

namespace BidMachineInc.Ads.Editor
{
    internal static class RemoveHelper
    {
        private const string PackageName = "io.bidmachine.ads";

        public static void RemovePlugin()
        {
            if (!EditorUtility.DisplayDialog("Remove BidMachine plugin",
                    "Are you sure you want to remove the BidMachine plugin from your project?",
                    "Yes",
                    "Cancel")) return;

            Client.Remove(PackageName);
        }
    }
}
