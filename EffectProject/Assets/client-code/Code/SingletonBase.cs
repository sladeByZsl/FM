using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFrameWork.Common
{
    public class SingletonBase<T> where T : class, new()
    {
        private static T _instance;
        private static readonly object syslock;

        static SingletonBase()
        {
            syslock = new object();
        }

        public static T GetInstance()
        {
            if (_instance == null)
            {
                object syslock = SingletonBase<T>.syslock;
                lock (syslock)
                {
                    if (_instance == null)
                    {
                        _instance = Activator.CreateInstance<T>();
                    }
                }
            }
            return _instance;
        }
    }


    public interface IManager
    {
        void Init();
        void Clear();
    }
}
