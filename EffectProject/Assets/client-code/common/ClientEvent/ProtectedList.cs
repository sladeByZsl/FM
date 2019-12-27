using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XFrameWork.Common
{
    public class ProtectedList<T>
    {
        public int Count
        {
            get
            {
                return _list.Count;
            }
        }

        protected List<T> _list = new List<T>();
        protected List<T> _tempAdds = null;
        protected List<T> _tempDels = null;
        protected bool _isLoop = false;

        public void Traverse(Action<T> cb)
        {
            _isLoop = true;
            for (int i = 0; i < _list.Count; i++)
            {
                var it = _list[i];
                if (_tempDels != null)
                {
                    if (!_tempDels.Contains(it))
                    {
                        cb(it);
                    }
                }
                else
                {
                    cb(it);
                }
            }
            _isLoop = false;

            if (_tempAdds != null && _tempAdds.Count > 0)
            {
                foreach (var it in _tempAdds)
                {
                    _list.Add(it);
                }
                _tempAdds.Clear();
            }
            if (_tempDels != null && _tempDels.Count > 0)
            {
                foreach (var it in _tempDels)
                {
                    _list.Remove(it);
                }
                _tempDels.Clear();
            }
        }

        public void Add(T t)
        {
            if (_isLoop)
            {
                if (_tempAdds == null)
                {
                    _tempAdds = new List<T>();
                }
                _tempAdds.Add(t);
            }
            else
            {
                _list.Add(t);
            }
        }

        public bool Contains(T t)
        {
            return _list.Contains(t);
        }

        public void Remove(T t)
        {
            if (_isLoop)
            {
                if (_tempDels == null)
                {
                    _tempDels = new List<T>();
                }
                _tempDels.Add(t);
            }
            else
            {
                _list.Remove(t);
            }
        }
    }
}
