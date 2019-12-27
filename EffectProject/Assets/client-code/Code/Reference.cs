using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFrameWork.Common
{
    public class Reference
    {
        public int refCount;

        public bool IsUnused()
        {
            return refCount <= 0;
        }

        public void Retain()
        {
            refCount++;
        }

        public void Release()
        {
            refCount--;
        }
    }
}
