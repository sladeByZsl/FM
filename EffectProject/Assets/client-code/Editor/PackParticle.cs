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
            string outputDir = rootPath + EditorCommon.BundleFxFolder + "/";

            EditorCommon.ClearDirectoryIfNotCreate(outputDir);
            List<string> objList = EditorCommon.GetPrefabs(srcDir);
            foreach (string obj in objList)
            {
                string[] deps = AssetDatabase.GetDependencies(obj);
                ResType resType;
                foreach (string dep in deps)
                {
                    Debug.Log("dep:" + dep);
                    resType = EditorCommon.GetBundleType(dep);
                    if (resType == ResType.Texture ||
                        resType == ResType.AnimatorController ||
                        resType == ResType.Model ||
                        resType == ResType.Shader)
                    {
                        AssetBundleBuild ab = GetAssetBundleBuild(dep, BundleType.Particle, resType);
                        if (!IsPathContain(dep, BundleType.Particle, resType))
                        {
                            mPackList.Add(ab);
                        }
                    }
                }

                resType = ResType.Prefab;
                AssetBundleBuild fx_ab = GetAssetBundleBuild(obj, BundleType.Particle,resType);
                if (!IsPathContain(obj, BundleType.Particle, resType))
                {
                    mPackList.Add(fx_ab);
                }
            }
        }
    }
}
