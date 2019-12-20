using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;



/// <summary>
/// 1.打包会输出到DataPC/DataAndroid/DataIOS等目录下
/// </summary>
public class PackAssetBundle
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

        //PackParticle();
        PackScene();
        BuildPipeline.BuildAssetBundles(outputDir, mPackList.ToArray(), BuildAssetBundleOptions.DeterministicAssetBundle, EditorCommon.BuildTarget);
    }


    /// <summary>
    /// 打包特效
    /// 特效包含的资源:prefab,texture,animator,model,shader,material
    /// 特效的prefab,material会打一个包,texture,animator,model,shader独立打包
    /// 打包目录：Arts/effect/prefab
    /// 输出目录：DataPC/particle
    /// </summary>
    static void PackParticle()
    {
        string srcDir = "Assets/Arts/effect/prefab";
        string outputDir = EditorCommon.DataPath + EditorCommon.BundleFolder + "/" + EditorCommon.BundleFxFolder + "/";

        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }
        else
        {
            EditorCommon.ClearDirectory(outputDir);
        }
      
        List<string> fxList = EditorCommon.GetPrefabs(srcDir);
        foreach (string fx in fxList)
        {
            //Debug.Log("fx:" + fx);
            string[] deps = AssetDatabase.GetDependencies(fx);
            foreach (string dep in deps)
            {
                Debug.Log("dep:" + dep);
                if (EditorCommon.IsTexture(dep) ||
                    EditorCommon.IsAnimatorController(dep) ||
                    EditorCommon.IsModel(dep) ||
                    EditorCommon.isShader(dep))
                {
                    //Debug.Log("dep:" + dep);
                    AssetBundleBuild ab = GetAssetBundleBuild(dep,EditorCommon.BundleType.Particle);
                    if (!IsPathContain(dep, EditorCommon.BundleType.Particle))
                    {
                        mPackList.Add(ab);
                    }
                }
            }

            AssetBundleBuild fx_ab = GetAssetBundleBuild(fx, EditorCommon.BundleType.Particle);
            if (!IsPathContain(fx, EditorCommon.BundleType.Particle))
            {
                mPackList.Add(fx_ab);
            }
        }

        //List<AssetBundleBuild> list = new List<AssetBundleBuild>();
        //AssetBundleBuild assetBundleBuild = new AssetBundleBuild();
        //assetBundleBuild.assetBundleName = "1.ab";
        //assetBundleBuild.assetNames = new string[] { ""};

        //list.Add(assetBundleBuild);
        //Debug.LogError("dir:" + dir);
        //BuildPipeline.BuildAssetBundles(outputDir, list.ToArray(), BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
        //BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.DeterministicAssetBundle, BuildTarget.StandaloneWindows64);
    }

    static void PackScene()
    {
        string srcDir = "Assets/Scenes";
        string outputDir = EditorCommon.DataPath + EditorCommon.BundleFolder + "/" + EditorCommon.BundleSceneFolder + "/";

        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }
        else
        {
            EditorCommon.ClearDirectory(outputDir);
        }

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
