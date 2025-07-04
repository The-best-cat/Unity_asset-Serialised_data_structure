using System.Collections.Generic;

namespace TheBlackCat.SerialisedDS
{
    public static class ComparerExtension
    {
        public static InverseComparer<T> Invert<T>(this IComparer<T> comparer)
        {
            return new InverseComparer<T>(comparer);
        }
    }
}
