using System.Collections.Generic;
using UnityEngine;

namespace TheBlackCat.SerialisedDS
{
    public class ListComparer<T> : IComparer<List<T>>, IComparer<ListWrapper<T>>
    {
        private Comparer<T> comparer;

        public ListComparer(Comparer<T> comparer = null)
        {
            this.comparer = comparer ?? Comparer<T>.Default;
        }

        public int Compare(List<T> x, List<T> y)
        {
            int max = Mathf.Max(x.Count, y.Count);
            for (int i = 0; i < max; i++)
            {
                if (i >= x.Count)
                {
                    return -1;
                }
                if (i >= y.Count)
                {
                    return 1;
                }

                if (comparer.LessThan(x[i], y[i]))
                {
                    return -1;
                }
                if (comparer.LessThan(y[i], x[i]))
                {
                    return 1;
                }
            }
            return 0;
        }

        public int Compare(ListWrapper<T> x, ListWrapper<T> y)
        {
            return Compare((List<T>)x, (List<T>)y);
        }
    }
}
