using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XFrameWork.Common
{
    public enum AssetPathType
    {
        Type_Default,   // 只有使用Default时，才有可能使用Resources加载;

        Type_StreamingAssets,   // StreamingAssets文件夹;		
        Type_StreamingAssetsBundle, // StreamingAssets文件夹下存放Bundle的根目录;	
        Type_Local, // 本地路径;
        Type_LocalBundle,   // 本地路径存放Bundle的根目录;

        Type_RunAssets, // 为了以后可能存在的优化使用（优先使用local，没有则使用streamingassets）;
    }

    /// <summary>
	/// 资源类型，不同的资源类型有不同的加载优先级;
	/// </summary>
	public enum EResType
    {
        AssetType_Default = 0,

        AssetType_Audio,
        AssetType_Particle,
        AssetType_Character,
        AssetType_Player,
        AssetType_UI,
        AssetType_Icon,
        AssetType_Texture,
        AssetType_TextAsset,
        AssetType_Atlas,
        AssetType_Block,
        AssetType_Scene,
        AssetType_Cinema,
        AssetType_Spine,
        AssetType_Vedio,
        AssetType_Object,
    }

    public abstract class Loader:IManager
    {
        protected bool m_isDone = false;
        protected Int32 m_RefCount = 0;
        protected string m_strError = string.Empty;//错误信息
        protected bool m_allowDelete = false;//是否可以删除

        public delegate void LoaderComplete(Loader loader);
        internal event LoaderComplete completeHandler = null;    // 加载完成的回调函数;
        public bool unloadAllLoadedObjects { get; set; }

        public string url { get; set; } //下载地址
        public Int64 nPRI { get; set; } //优先级
        public EResType resType { get; set; } //资源类型
        public AssetPathType pathType { get; set; }

        public bool allowDelete
        {
            get
            {
                return m_allowDelete;
            }
        }

        public bool isDone
        {
            get
            {
                return m_isDone;
            }
        }

        public Int32 refCount
        {
            get { return m_RefCount; }
            set
            {
                if (m_RefCount != value)
                {
                    m_RefCount = value;
                    if(m_RefCount<=0)
                    {
                        DestroyLoader();
                    }
                }
            }
        }

        public void AddReference()
        {
            refCount += 1;
        }

        public void RemoveReference()
        {
            refCount -= 1;
        }

        public void Init()
        {

        }

        protected void DestroyLoader()
        {
            if(refCount>0||!m_allowDelete)
            {
                return;
            }
        }

        public void Clear()
        {
            
        }
    }
}
