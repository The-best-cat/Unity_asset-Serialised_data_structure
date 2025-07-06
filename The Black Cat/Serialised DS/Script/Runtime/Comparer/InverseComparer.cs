using System.Collections.Generic;

namespace TheBlackCat.SerialisedDS
{
    public class InverseComparer<T> : IComparer<T>
    {
        private readonly IComparer<T> comparer;

        public InverseComparer(IComparer<T> comparer = null)
        {
            this.comparer = comparer ?? Comparer<T>.Default;
        }

        public int Compare(T x, T y)
        {
            return comparer.Compare(y, x);
        }
    }
}