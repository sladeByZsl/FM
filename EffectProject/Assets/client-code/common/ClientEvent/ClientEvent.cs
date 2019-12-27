
/**************************************************************************************************
	Copyright (C) 2016 - All Rights Reserved.
--------------------------------------------------------------------------------------------------------
	当前版本：1.0;
	文	件：ClientEvent.cs;
	作	者：jiabin;
	时	间：2016 - 05 - 26;
	注	释：;
**************************************************************************************************/

using System;

namespace XFrameWork.Common
{
    public class ClientEvent
    {

        private System.Object[] m_EventParams = null;//动态参数;
        private Int32 m_EventId = -1;
        private float m_TimeLock = 0;

        internal float timelock
        {
            get { return m_TimeLock; }
            set
            {
                m_TimeLock = value;
            }
        }

        public Int32 eventId { get { return m_EventId; } }

        internal void Reset()
        {
            m_EventParams = null;
            m_EventId = -1;
            m_TimeLock = 0f;
        }

        internal void SetEvent(Int32 eventId, float timeLock = 0f, params System.Object[] arms)
        {
            m_EventId = eventId;
            //m_TimeLock = UnityEngine.Time.time + timeLock;
            m_TimeLock = timeLock;
            m_EventParams = arms;
        }


        public T GetParameter<T>(int index)
        {
            if (m_EventParams == null || index >= m_EventParams.Length)
            {
                //UnityEngine.Debug.LogError("Error: The Event Parameter index > mParameters.Count!!!");
                return default(T);
            }

          
            if (m_EventParams[index] != null && !(m_EventParams[index] is T))
            {
                //UnityEngine.Debug.LogError("Error: The Event Parameter Type Error!!!"+typeof(T)+",src:"+ m_EventParams[index].GetType());
                //UnityEngine.Debug.LogError("Error: The Event Parameter Type Error!!!");
                return default(T);
            }

            return (T)m_EventParams[index];
        }


        public bool GetParameter<T>(int index, ref T value)
        {
            if (m_EventParams == null || index >= m_EventParams.Length)
            {
                value = default(T);
                return false;
            }

            if (m_EventParams[index] != null && !(m_EventParams[index] is T))
            {
                value = default(T);
                return false;
            }
            value = (T)m_EventParams[index];
            return true;
        }

        public System.Object[] GetParams()
        {
            return m_EventParams;
        }

        public int GetLength()
        {
            if (m_EventParams == null)
                return 0;
            return m_EventParams.Length;
        }
    }
}