// ReSharper disable CheckNamespace

using UnityEditor;
using UnityEditor.Callbacks;

namespace BidMachineInc.Ads.Ios.Editor
{
    internal static class IosBuildPostprocessor
    {
        [PostProcessBuild(100)]
        public static void OnPostprocessBuild(BuildTarget target, string path)
        {
            if (target == BuildTarget.iOS)
            {
                IosPostprocessUtils.PrepareProject(path);
            }
        }
    }
}
