
/**************************************************************************************************
	Copyright (C) 2016 - All Rights Reserved.
--------------------------------------------------------------------------------------------------------
	当前版本：1.0;
	文	件：ClientEventManager.cs;
	作	者：jiabin;
	时	间：2016 - 05 - 24;
	注	释：;
**************************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XFrameWork.Common
{
    public class ClientEventHelper
    {
        public static void RegisterEvent(Int32 eventId, ClientEventManager.EventDelegate handler)
        {
            ClientEventManager.instance.RegisterEvent(eventId, handler);
        }

        public static void UnRegisterEvent(Int32 eventId, ClientEventManager.EventDelegate handler)
        {
            ClientEventManager.instance.UnRegisterEvent(eventId, handler);
        }

        public static void PushEvent(Int32 eventId, params System.Object[] arms)
        {
            ClientEventManager.instance.PushImmediateEvent(eventId, arms);
        }
    }

	public class ClientEventManager :SingletonBase<ClientEventManager>
	{
        public static ClientEventManager instance = GetInstance();
		public delegate void EventDelegate(ClientEvent eve);

		protected List<ClientEvent> m_EventCache = new List<ClientEvent>();
		protected List<ClientEvent> m_ProcessEvents = new List<ClientEvent>();

		protected Dictionary<Int32, ProtectedList<EventDelegate>> m_FunMap = new Dictionary<Int32, ProtectedList<EventDelegate>>();   // 回调函数的处理列表;
        //public static ClientEventManager instance = ClientEventManager.GetInstance();
        /// <summary>
        /// 发送一个消息;
        /// </summary>
        /// <param name="eventId">消息ID</param>
        /// <param name="isImmediately">是否立即发送</param>
        /// <param name="timeLock">延迟时间</param>
        /// <param name="arms">动态参数</param>
        public void PushEvent(Int32 eventId, bool isImmediately = true, float timeLock = 0f, params System.Object[] arms)
		{
			ClientEvent eve = GetEventFromCache();
			if (eve == null)
			{
				return;
			}

			eve.SetEvent(eventId, timeLock, arms);

			if (isImmediately)
			{
				ActionEvent(eve);
				return;
			}

			m_ProcessEvents.Add(eve);
		}

        public void PushImmediateEvent(GameEventID eventId, params System.Object[] arms)
        {
            PushImmediateEvent((int)eventId, arms);
        }

        public void PushImmediateEvent(Int32 eventId, params System.Object[] arms)
        {
            PushEvent(eventId, true, 0, arms);
        }


        public void RegisterEvent(Int32 eventId, EventDelegate fun)
		{
			if (fun == null)
			{
				return;
			}

            ProtectedList<EventDelegate> funList = GetFunList(eventId);
			if (funList == null)
			{
				return;
			}

			if (!funList.Contains(fun))
			{
                funList.Add(fun);
			}
		}

		public void UnRegisterEvent(Int32 eventId, EventDelegate fun, bool check = true)
		{
			if (fun == null)
			{
				return;
			}

            ProtectedList<EventDelegate> funList = GetFunList(eventId);
			if (funList == null)
			{
				return;
			}

            funList.Remove(fun);
        }

		public void Update(float deltaTime)
		{
			if (m_ProcessEvents == null || m_ProcessEvents.Count < 1)
			{
				return;
			}

			//float fCurTime = UnityEngine.Time.time;
			for (int i = 0; i < m_ProcessEvents.Count;)
			{
				ClientEvent eve = m_ProcessEvents[i];
				if (eve == null)
				{
					m_ProcessEvents.RemoveAt(i);
					continue;
				}
                eve.timelock -= deltaTime;
                if (eve.timelock>0)
				{
					i++;
					continue;
				}

				ActionEvent(eve);
				m_ProcessEvents.RemoveAt(i);
			}
		}

		protected void ActionEvent(ClientEvent eve)
		{
			if (eve == null)
			{
				return;
			}

            ProtectedList<EventDelegate> funList = GetFunList(eve.eventId);
            if (funList.Count > 0)
            {
                Action<EventDelegate> cb = (fun) =>
                {
                    if (fun != null)
                    {
                        try
                        {
                            fun(eve);
                        }
                        catch (System.Exception ex)
                        {
                            UnityEngine.Debug.LogError(string.Format("{0} --- {1} --- {2} --- {3}",eve.eventId, fun.Target, fun.Method != null ? fun.Method.Name : "NULL", ex.ToString()));
                        }
                    }
                };
                funList.Traverse(cb);
            }           

            AddEvent2Cache(eve);
        }

		protected void AddEvent2Cache(ClientEvent eve)
		{
			if (eve == null)
			{
				return;
			}

			eve.Reset();
			if (!m_EventCache.Contains(eve))
			{
				m_EventCache.Add(eve);
			}
		}

		protected ClientEvent GetEventFromCache()
		{
			ClientEvent eve = null;

			if (m_EventCache.Count > 0)
			{
				eve = m_EventCache[0];
				m_EventCache.RemoveAt(0);
			}
			else
			{
				eve = new ClientEvent();
			}

			return eve;
		}

		protected ProtectedList<EventDelegate> GetFunList(Int32 eventId)
		{
            ProtectedList<EventDelegate> funList = null;

			if (!m_FunMap.TryGetValue(eventId, out funList))
			{
				funList = new ProtectedList<EventDelegate>();
				m_FunMap.Add(eventId, funList);
			}

			return funList;
		}
    }
}