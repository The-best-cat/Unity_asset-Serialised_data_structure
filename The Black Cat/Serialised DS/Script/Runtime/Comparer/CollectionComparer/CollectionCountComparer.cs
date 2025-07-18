using System.Collections;
using System.Collections.Generic;

namespace TheBlackCat.SerialisedDS
{
    public class CollectionCountComparer<T> : IComparer<ICollection>
    {
        public int Compare(ICollection x, ICollection y)
        {
            if (x == null)
            {
                throw new System.ArgumentNullException(nameof(x));
            }
            if (y == null)
            {
                throw new System.ArgumentNullException(nameof(y));
            }

            if (x.Count == y.Count)
            {
                return 0;
            }
            if (x.Count > y.Count)
            {
                return 1;
            }
            return -1;
        }
    }
}