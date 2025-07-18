using System.Collections.Generic;

namespace TheBlackCat.SerialisedDS
{
    public static class ComparerExtension
    {
        public static InverseComparer<T> Invert<T>(this IComparer<T> comparer)
        {
            return new InverseComparer<T>(comparer);
        }

        public static bool LessThan<T>(this IComparer<T> comparer, T a, T b)
        {
            return comparer.Compare(a, b) < 0;
        }

        public static bool GreaterThan<T>(this IComparer<T> comparer, T a, T b)
        {
            return comparer.Compare(a, b) > 0;
        }

        public static bool LessThanOrEqualTo<T>(this IComparer<T> comparer, T a, T b)
        {
            return comparer.Compare(a, b) <= 0;
        }

        public static bool GreaterThanOrEqualTo<T>(this IComparer<T> comparer, T a, T b)
        {
            return comparer.Compare(a, b) >= 0;
        }

        public static bool EqualTo<T>(this IComparer<T> comparer, T a, T b)
        {
            return comparer.Compare(a, b) == 0;
        }
    }

}
