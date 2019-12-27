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
            string outputDir = rootPath + EditorCommon.BundleSceneFolder + "/";

            EditorCommon.ClearDirectoryIfNotCreate(outputDir);

            List<string> objList = EditorCommon.GetScenes(srcDir);
            foreach (string obj in objList)
            {
                Debug.Log("------------fx--------------:" + obj);
                string[] deps = AssetDatabase.GetDependencies(obj);
                ResType resType;
                foreach (string dep in deps)
                {
                    //Debug.Log("dep:" + dep);
                    resType = EditorCommon.GetBundleType(dep);
                    if (resType == ResType.Texture ||
                        resType == ResType.Shader)
                    {
                        Debug.Log("dep:" + dep);
                        AssetBundleBuild ab = GetAssetBundleBuild(dep, BundleType.Scene, resType);
                        if (!IsPathContain(dep, BundleType.Scene, resType))
                        {
                            mPackList.Add(ab);
                        }
                    }
                }

                resType = ResType.Scene;
                AssetBundleBuild fx_ab = GetAssetBundleBuild(obj, BundleType.Scene, resType);
                if (!IsPathContain(obj, BundleType.Scene, resType))
                {
                    mPackList.Add(fx_ab);
                }
            }
        }
    }
}
