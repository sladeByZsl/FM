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

        public static AssetBundleBuild GetAssetBundleBuild(string fullPath, EditorCommon.BundleType bundleType)
        {
            AssetBundleBuild ab = new AssetBundleBuild();
            ab.assetNames = new string[] { fullPath }; //相对于Assets的目录
            ab.assetBundleName = EditorCommon.GetBundleName(fullPath, bundleType);//ab包的名字，可以带目录
            ab.assetBundleVariant = string.Empty;
            return ab;
        }

        public static bool IsPathContain(string dep, EditorCommon.BundleType bundleType)
        {
            string fileName = EditorCommon.GetBundleName(dep, bundleType);
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
