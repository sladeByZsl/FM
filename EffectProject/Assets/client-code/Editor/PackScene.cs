using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace XFrameWork.Editor
{
    public partial class PackAssetBundle
    {
        static void PackScene()
        {
            string srcDir = "Assets/Scenes";
            string outputDir = EditorCommon.DataPath + EditorCommon.BundleFolder + "/" + EditorCommon.BundleSceneFolder + "/";

            EditorCommon.ClearDirectoryIfNotCreate(outputDir);

            List<string> fxList = EditorCommon.GetScenes(srcDir);
            foreach (string fx in fxList)
            {
                Debug.Log("------------fx--------------:" + fx);
                string[] deps = AssetDatabase.GetDependencies(fx);
                foreach (string dep in deps)
                {
                    //Debug.Log("dep:" + dep);
                    if (EditorCommon.IsTexture(dep) ||
                        EditorCommon.isShader(dep))
                    {
                        Debug.Log("dep:" + dep);
                        AssetBundleBuild ab = GetAssetBundleBuild(dep, EditorCommon.BundleType.Scene);
                        if (!IsPathContain(dep, EditorCommon.BundleType.Scene))
                        {
                            mPackList.Add(ab);
                        }
                    }
                }

                AssetBundleBuild fx_ab = GetAssetBundleBuild(fx, EditorCommon.BundleType.Scene);
                if (!IsPathContain(fx, EditorCommon.BundleType.Scene))
                {
                    mPackList.Add(fx_ab);
                }
            }
        }
    }
}
