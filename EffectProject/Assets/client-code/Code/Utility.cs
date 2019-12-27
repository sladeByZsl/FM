using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace XFrameWork.Common
{
    public delegate Object LoadDelegate(string path, Type type);

    public delegate string GetPlatformDelegate();

    public static class Utility
    {
        public const string AssetBundles = "AssetBundles";
        public const string AssetsManifestAsset = "Assets/Manifest.asset";
        public static bool assetBundleMode = true;
        public static LoadDelegate loadDelegate = null;
        public static GetPlatformDelegate getPlatformDelegate = null;

        public static string dataPath { get; set; }
        public static string downloadURL { get; set; }

        public static string GetPlatform()
        {
            return getPlatformDelegate != null
                ? getPlatformDelegate()
                : GetPlatformForAssetBundles(Application.platform);
        }

        private static string GetPlatformForAssetBundles(RuntimePlatform platform)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (platform)
            {
                case RuntimePlatform.Android:
                    return "Android";
                case RuntimePlatform.IPhonePlayer:
                    return "iOS";
                case RuntimePlatform.WebGLPlayer:
                    return "WebGL";
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                    return "Windows";
                case RuntimePlatform.OSXPlayer:
                case RuntimePlatform.OSXEditor:
                    return "OSX";
                default:
                    return null;
            }
        }

        public static string updatePath
        {
            get
            {
                return Path.Combine(Application.persistentDataPath, Path.Combine(AssetBundles, GetPlatform())) +
                       Path.DirectorySeparatorChar;
            }
        }

        public static string GetRelativePath4Update(string path)
        {
            return updatePath + path;
        }

        public static string GetDownloadURL(string filename)
        {
            return Path.Combine(Path.Combine(downloadURL, GetPlatform()), filename);
        }

        public static string GetWebUrlFromDataPath(string filename)
        {
            var path = Path.Combine(dataPath, Path.Combine(AssetBundles, GetPlatform())) + Path.DirectorySeparatorChar + filename;
#if UNITY_IOS || UNITY_EDITOR
            path = "file://" + path;
#elif UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
            path = "file:///" + path;
#endif
            return path;
        }

        public static string GetWebUrlFromStreamingAssets(string filename)
        {
            var path = updatePath + filename;
            if (!File.Exists(path))
            {
                path = Application.streamingAssetsPath + "/" + filename;
            }
#if UNITY_IOS || UNITY_EDITOR
            path = "file://" + path;
#elif UNITY_STANDALONE_WIN
            path = "file:///" + path;
#endif
            return path;
        }
    }
}
