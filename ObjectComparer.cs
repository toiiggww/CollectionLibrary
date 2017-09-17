using System;
using System.Collections.Generic;

namespace TEArts.Etc.CollectionLibrary
{
    public class ObjectComparer<T> : IEqualityComparer<T>
    {
        private ObjectComparer()
        {
            Comparer = null;
        }

        public ObjectComparer(Func<T, T, bool> fun)
        {
            Comparer = fun;
        }

        private Func<T, T, bool> Comparer { get; set; }

        public bool Equals(T x, T y)
        {
            if (Comparer == null)
            {
                throw new InvalidOperationException(nameof(Comparer));
            }
            return x != null & y != null ? Comparer(x, y) : x == null & y == null;
        }

        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
    }
}
