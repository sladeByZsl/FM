using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;


namespace XFrameWork.Editor
{
    /// <summary>
    /// 1.打包会输出到DataPC/DataAndroid/DataIOS等目录下
    /// </summary>
    public partial class PackAssetBundle
    {
        public static List<AssetBundleBuild> mPackList = new List<AssetBundleBuild>();
        public static string rootPath = EditorCommon.DataPath + EditorCommon.BundleFolder + "/";

        [MenuItem("Build/BuildAll")]
        static void BuildAll()
        {
            string outputDir = EditorCommon.DataPath + EditorCommon.BundleFolder;
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }
            else
            {
                EditorCommon.ClearDirectory(outputDir);
            }

            PackParticle();
            PackScene();
            BuildPipeline.BuildAssetBundles(outputDir, mPackList.ToArray(), BuildAssetBundleOptions.DeterministicAssetBundle, EditorCommon.BuildTarget);
        }

        public static AssetBundleBuild GetAssetBundleBuild(string fullPath, BundleType bundleType, ResType resType)
        {
            AssetBundleBuild ab = new AssetBundleBuild();
            ab.assetNames = new string[] { fullPath }; //相对于Assets的目录
            ab.assetBundleName = EditorCommon.GetBundleName(fullPath, bundleType, resType);//ab包的名字，可以带目录
            ab.assetBundleVariant = string.Empty;
            return ab;
        }

        public static bool IsPathContain(string dep, BundleType bundleType, ResType resType)
        {
            string fileName = EditorCommon.GetBundleName(dep, bundleType, resType);
            for (int i = 0; i < mPackList.Count; i++)
            {
                if (mPackList[i].assetBundleName.Contains(fileName))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
