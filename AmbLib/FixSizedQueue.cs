using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ambiesoft
{
    public class FixSizedQueue<T> : System.Collections.Generic.Queue<T>
    {
        public int Size { get; private set; }

        public FixSizedQueue(int size)
        {
            Size = size;
        }

        public new T Enqueue(T obj)
        {
            base.Enqueue(obj);
            T ret = default(T);
            while (base.Count > Size)
            {
                ret = base.Dequeue();
            }
            return ret;
        }

        public bool Filled
        {
            get
            {
                return base.Count >= Size;
            }
        }
    }
}
