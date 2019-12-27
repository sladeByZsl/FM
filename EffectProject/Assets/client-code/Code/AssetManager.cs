using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace XFrameWork.Common
{
    public class AssetManager: MonoBehaviour
    {
        public static void Initialize(string path,string platform,Action onSuccess, Action<string> onError)
        {
            var instance = FindObjectOfType<AssetManager>();
            if (instance == null)
            {
                instance = new GameObject("AssetManager").AddComponent<AssetManager>();
                DontDestroyOnLoad(instance.gameObject);
            }

            if (Utility.assetBundleMode)
            {
                BundleManager.Initialize(path, platform, onSuccess, onError);
            }
            else
            {
                if (onSuccess != null)
                    onSuccess();
            }
        }

        public void Update()
        {
            BundleManager.Update();
        }
    }
}
