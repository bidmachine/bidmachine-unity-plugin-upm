// ReSharper disable CheckNamespace

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using UnityEditor.iOS.Xcode;
using UnityEngine;

namespace BidMachineInc.Ads.Ios.Editor
{
    internal static class IosPostprocessUtils
    {
        private const string Suffix = ".framework";

        private static readonly string[] Frameworks =
        {
            "AdSupport",
            "AudioToolbox",
            "AVFoundation",
            "CFNetwork",
            "CoreFoundation",
            "CoreGraphics",
            "CoreImage",
            "CoreLocation",
            "CoreMedia",
            "CoreMotion",
            "CoreTelephony",
            "CoreText",
            "EventKitUI",
            "EventKit",
            "GLKit",
            "ImageIO",
            "JavaScriptCore",
            "MediaPlayer",
            "MessageUI",
            "MobileCoreServices",
            "QuartzCore",
            "SafariServices",
            "Security",
            "Social",
            "StoreKit",
            "SystemConfiguration",
            "Twitter",
            "UIKit",
            "VideoToolbox",
            "WatchConnectivity",
            "WebKit"
        };

        private static readonly string[] WeakFrameworks =
        {
            "AppTrackingTransparency"
        };

        private static readonly string[] PlatformLibs =
        {
            "libc++.dylib",
            "libz.dylib",
            "libsqlite3.dylib",
            "libxml2.2.dylib"
        };

        public static void PrepareProject(string buildPath)
        {
            Debug.Log("Preparing your Xcode project for BidMachine");

            string projectPath = PBXProject.GetPBXProjectPath(buildPath);
            var project = new PBXProject();
            project.ReadFromFile(projectPath);

            string mainTarget = project.GetUnityMainTargetGuid();
            string unityFrameworkTarget = project.GetUnityFrameworkTargetGuid();

            AddProjectFrameworks(Frameworks, project, mainTarget, false);
            AddProjectFrameworks(WeakFrameworks, project, mainTarget, true);
            AddProjectLibs(PlatformLibs, project, mainTarget);

            project.SetBuildProperty(project.ProjectGuid(), "ENABLE_BITCODE", "NO");

            project.SetBuildProperty(mainTarget, "SWIFT_VERSION", "5.5");

            project.SetBuildProperty(mainTarget, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "YES");
            project.SetBuildProperty(unityFrameworkTarget, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "NO");

            project.AddBuildProperty(mainTarget, "OTHER_LDFLAGS", "-ObjC");
            project.AddBuildProperty(mainTarget, "LIBRARY_SEARCH_PATHS", "$(SRCROOT)/Libraries");
            project.AddBuildProperty(mainTarget, "LIBRARY_SEARCH_PATHS", "$(TOOLCHAIN_DIR)/usr/lib/swift/$(PLATFORM_NAME)");
            project.AddBuildProperty(mainTarget, "LD_RUNPATH_SEARCH_PATHS", "@executable_path/Frameworks");

            project.WriteToFile(projectPath);
        }

        private static void AddProjectFrameworks(IEnumerable<string> frameworks, PBXProject project, string target, bool weak)
        {
            foreach (string framework in frameworks)
            {
                if (!project.ContainsFramework(target, framework))
                {
                    project.AddFrameworkToProject(target, framework + Suffix, weak);
                }
            }
        }

        private static void AddProjectLibs(IEnumerable<string> libs, PBXProject project, string target)
        {
            foreach (string lib in libs)
            {
                string libGuid = project.AddFile("usr/lib/" + lib, "Libraries/" + lib, PBXSourceTree.Sdk);
                project.AddFileToBuild(target, libGuid);
            }
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private static void AddKeyToPlist(string path, string key, string value)
        {
            var plist = new PlistDocument();
            plist.ReadFromFile(path);
            plist.root.SetString(key, value);
            File.WriteAllText(path, plist.WriteToString());
        }
    }
}
