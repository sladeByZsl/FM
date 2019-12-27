using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XFrameWork.Common;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        string path = localPath + "DataPC/";
        AssetManager.Initialize(path, "DataPC",
            () => { OnInitSuccess(); },
            (error) => { Debug.LogError(error); });

    }

    public void OnInitSuccess()
    {
        Debug.LogError("success");
        BundleManager.Load("particle/fx_01_cb_bonus_bao_small_green.ab");
    }

    public static string localPath
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

    /*
    // Start is called before the first frame update
    void Start()
    {
        AssetBundle manifsetAB = AssetBundle.LoadFromFile(localPath + "DataPC/DataPC");
        AssetBundleManifest assetBundleManifest = manifsetAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        //AssetBundleManifest mainfest = AssetBundle.LoadFromFile<AssetBundleManifest>("");
        //mainfest.GetAllDependencies()
        string[] array = assetBundleManifest.GetAllDependencies("scene/map01.ab");

        //string[] tmp = new string[]{ "scene/level_unlitcolor(nofog)_alphatest.ab",
        //"scene/unlitcolor(nofog).ab",
        //"scene/level_unlitcolor(fog)_alphatest.ab",
        //"scene/terrain_4map.ab"};

        List<AssetBundle> mList = new List<AssetBundle>();
        for (int i = 0; i < array.Length; i++)
        {
            AssetBundle tmp_ab = AssetBundle.LoadFromFile(localPath + "DataPC/" + array[i]);
            if (tmp_ab != null)
            {
                mList.Add(tmp_ab);
                //Object[] objectArray = tmp_ab.LoadAllAssets();
                Debug.LogError(array[i]);
            }
        }

        //for (int i = 0; i < tmp.Length; i++)
        //{
        //    AssetBundle tmp_ab = AssetBundle.LoadFromFile(localPath + "DataPC/" + tmp[i]);
        //    if (tmp_ab != null)
        //    {
        //        mList.Add(tmp_ab);
        //        //Object[] objectArray = tmp_ab.LoadAllAssets();
        //        Debug.LogError(tmp[i]);
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            string path = localPath + "DataPC/scene/map01.ab";
            AssetBundle ab = AssetBundle.LoadFromFile(path);
            SceneManager.LoadScene("map01");

            //GameObject obj = ab.LoadAsset<GameObject>("fx_01_cb_bonus_bao_small_green");


            //Instantiate(obj);
        }
    }

    public static string localPath
    {
        get
        {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
            return string.Format("{0}/../", Application.dataPath);
#else
			return string.Format("{0}/", Application.persistentDataPath);
#endif
        }
    }*/
}
