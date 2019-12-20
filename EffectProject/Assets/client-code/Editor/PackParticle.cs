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
            string outputDir = EditorCommon.DataPath + EditorCommon.BundleFolder + "/" + EditorCommon.BundleFxFolder + "/";

            EditorCommon.ClearDirectoryIfNotCreate(outputDir);

            List<string> fxList = EditorCommon.GetPrefabs(srcDir);
            foreach (string fx in fxList)
            {
                string[] deps = AssetDatabase.GetDependencies(fx);
                foreach (string dep in deps)
                {
                    Debug.Log("dep:" + dep);
                    if (EditorCommon.IsTexture(dep) ||
                        EditorCommon.IsAnimatorController(dep) ||
                        EditorCommon.IsModel(dep) ||
                        EditorCommon.isShader(dep))
                    {
                        AssetBundleBuild ab = GetAssetBundleBuild(dep, EditorCommon.BundleType.Particle);
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
        }
    }
}
