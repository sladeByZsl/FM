
/**************************************************************************************************
	Copyright (C) 2016 - All Rights Reserved.
--------------------------------------------------------------------------------------------------------
	当前版本：1.0;
	文	件：EditorCommon.cs;
	作	者：jiabin;
	时	间：2017 - 02 - 09;
	注	释：;
**************************************************************************************************/

using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.IO;

namespace XFrameWork.Editor
{
    public enum ResType
    {
        Texture,
        AnimatorController,
        Model,
        Shader,
        Font,
        Prefab,
        Scene,
        Unknown,
    }

    public enum BundleType
    {
        None,
        Particle,
        UIPrefab,
        UIIcon,
        UITexture,
        UISpine,
        UIParticle,
        Audio,
        Cinema,
        Scene,
        Spine,
        Video,
        Object,
    }

    public class EditorCommon
    {
        public static int PackMode = 0;
        public const string BundleExtensions = ".ab";

        public const string AssetBundleFolder = "AssetBundles";
        public static string DataPath
        {
            get
            {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
                return string.Format("{0}/../", Application.dataPath);
#else
			return string.Format("{0}/", Application.persistentDataPath);
#endif
            }
        }

#if UNITY_ANDROID
	public static string BundleFolder = "DataAndroid";
#elif UNITY_IOS
	public static string BundleFolder = "DataIOS";
#elif UNITY_STANDALONE_WIN
        public static string BundleFolder = "DataPC";
#elif UNITY_STANDALONE_OSX
	public static string BundleFolder = "DataMAC";
#else
	public static string BundleFolder = "DataPC";
#endif


#if UNITY_ANDROID
	public static string BundleFolder = "BuildTarget.Android";
#elif UNITY_IOS
	public static string BundleFolder = "BuildTarget.iOS";
#elif UNITY_STANDALONE_WIN
        public static BuildTarget BuildTarget = BuildTarget.StandaloneWindows64;
#elif UNITY_STANDALONE_OSX
	public static string BundleFolder = "BuildTarget.StandaloneOSX";
#else
	public static string BundleFolder = "BuildTarget.StandaloneWindows64";
#endif


        public static string BundleFxFolder = "particle";
        public static string BundleSceneFolder = "scene";

        public static List<string> GetPrefabs(string folder)
        {
            return GetAllFiles(folder, "*.prefab");
        }
        public static List<string> GetBlocks(string folder)
        {
            return GetAllFiles(folder, "*.obs");
        }

        public static List<string> GetScenes(string folder)
        {
            return GetAllFiles(folder, "*.unity");
        }
        public static List<string> GetBundles(string folder)
        {
            return GetAllFiles(folder, "*" + ".bundle");
        }
        public static List<string> GetTextures(string folder)
        {
            return GetAllFiles(folder, "*.png", "*.jpg");
        }
        public static List<string> GetSpines(string folder)
        {
            return GetAllFiles(folder, "*.asset");
        }
        public static List<string> GetAudio(string folder)
        {
            return GetAllFiles(folder, "*.wav", "*.ogg", "*.mp3");
        }
        public static List<string> GetLua(string folder)
        {
            return GetAllFiles(folder, "*.lua");
        }
        public static List<string> GetConf(string folder)
        {
            return GetAllFiles(folder, "*.dat", "*.bin");
        }

        public static List<string> GetVedio(string folder)
        {
            return GetAllFiles(folder, "*.mp4");
        }

        public static List<string> GetAllFiles(string folder, SearchOption option, params string[] searchPatterns)
        {
            List<string> searchPatternList = new List<string>();
            if (string.IsNullOrEmpty(folder))
            {
                return searchPatternList;
            }

            if (!Directory.Exists(folder))
            {
                return searchPatternList;
            }

            if (searchPatterns == null || searchPatterns.Length < 1)
            {
                searchPatternList.Add("");
            }
            else
            {
                foreach (string searchPattern in searchPatterns)
                {
                    searchPatternList.Add(searchPattern);
                }
            }

            List<string> fileList = new List<string>();

            GetFileList(ref fileList, searchPatternList, folder, option);
            return fileList;
        }

        public static List<string> GetAllFiles(string folder, params string[] searchPatterns)
        {
            List<string> searchPatternList = new List<string>();
            if (string.IsNullOrEmpty(folder))
            {
                return searchPatternList;
            }

            if (!Directory.Exists(folder))
            {
                return searchPatternList;
            }

            if (searchPatterns == null || searchPatterns.Length < 1)
            {
                searchPatternList.Add("");
            }
            else
            {
                foreach (string searchPattern in searchPatterns)
                {
                    searchPatternList.Add(searchPattern);
                }
            }

            List<string> fileList = new List<string>();

            GetFileList(ref fileList, searchPatternList, folder);

            return fileList;
        }

        private static void GetFileList(ref List<string> fileList, List<string> searchPatternList, string folder, SearchOption option = SearchOption.AllDirectories)
        {
            foreach (string searchPattern in searchPatternList)
            {
                string[] files = null;
                if (string.IsNullOrEmpty(searchPattern))
                {
                    files = Directory.GetFiles(folder, "", option);
                }
                else
                {
                    files = Directory.GetFiles(folder, searchPattern, option);
                }

                foreach (string file in files)
                {
                    if (fileList.Contains(file))
                    {
                        continue;
                    }

                    string path = file.Replace("\\", "/");
                    fileList.Add(path);
                }
            }
        }
        public static bool IsPrefab(string path)
        {
            return IsRes(path, ".prefab");
        }

        public static bool IsTexture(string path)
        {
            return IsRes(path, ".jpg", ".tga", ".dds", ".png", ".dxt", ".psd", ".bmp", ".cubemap");
        }

        public static bool IsFont(string path)
        {
            return IsRes(path, ".ttf");
        }

        public static bool IsAnimatorController(string path)
        {
            return IsRes(path, ".controller");
        }

        public static bool IsModel(string path)
        {
            return IsRes(path, ".fbx");
        }
        public static bool isMat(string path)
        {
            return IsRes(path, ".mat");
        }
        public static bool isShader(string path)
        {
            return IsRes(path, ".shader");
        }

        public static ResType GetBundleType(string path)
        {
            if (IsTexture(path))
            {
                return ResType.Texture;
            }
            else if (IsAnimatorController(path))
            {
                return ResType.AnimatorController;
            }
            else if (IsModel(path))
            {
                return ResType.Model;
            }
            else if (isShader(path))
            {
                return ResType.Shader;
            }
            else if (IsFont(path))
            {
                return ResType.Font;
            }
            else if (IsPrefab(path))
            {
                return ResType.Prefab;
            }
            else
            {
                return ResType.Unknown;
            }
        }


        public static bool IsCS(string path)
        {
            return IsRes(path, ".cs");
        }

        public static bool IsRes(string path, params string[] extensions)
        {
            if (string.IsNullOrEmpty(path) || extensions == null || extensions.Length < 1)
            {
                return false;
            }

            string extension = System.IO.Path.GetExtension(path);
            if (string.IsNullOrEmpty(extension))
            {
                return false;
            }
            extension = extension.ToLower();

            for (int i = 0; i < extensions.Length; i++)
            {
                if (string.Equals(extensions[i], extension))
                {
                    return true;
                }
            }

            return false;
        }


        public static string GetBundleName(string path, BundleType bundleType,ResType resType)
        {
            string fileName = string.Empty;
            string abFileName = string.Empty;
            if (resType == ResType.Texture || 
                resType == ResType.AnimatorController ||
                resType == ResType.Model ||
                resType == ResType.Shader)
            {
                string extension= System.IO.Path.GetExtension(path);
                path.Replace(".", "_");
                fileName = path;//path.Replace(extension,"_"+ resType.ToString().ToLower());
                abFileName = string.Format("{0}{1}", fileName, BundleExtensions);
            }
            else
            {
                fileName = System.IO.Path.GetFileNameWithoutExtension(path);
                abFileName = string.Format("{0}/{1}{2}", bundleType.ToString().ToLower(), fileName, BundleExtensions);
            }
            return abFileName;
        }

        public static void ClearDirectory(string dir)
        {
            if (Directory.Exists(dir))
            {
                string[] entries = System.IO.Directory.GetFileSystemEntries(dir);
                foreach (string entry in entries)
                {

                    if (Directory.Exists(entry))
                    {
                        Directory.Delete(entry, true);
                    }
                    else
                    {
                        File.Delete(entry);
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(dir);
            }

        }

        public static void ClearDirectoryIfNotCreate(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            else
            {
                ClearDirectory(dir);
            }
        }
    }
}