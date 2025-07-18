using System;
using System.Collections.Generic;
using System.Linq;

namespace TheBlackCat.SerialisedDS
{
    public static class ListExtension
    {
        public static void Shuffle<T>(this List<T> list)
        {
            list.Shuffle(0, list.Count);
        }

        public static void Shuffle<T>(this List<T> list, int index)
        {            
            list.Shuffle(index, list.Count - index);
        }

        public static void Shuffle<T>(this List<T> list, int index, int range)
        {
            if (index < 0 || index >= list.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            if (range < 0 || range > list.Count)
            {
                throw new ArgumentException("Range of elements to shuffle is out of range.");
            }
            if (index + range > list.Count)
            {
                throw new ArgumentException($"The number of elements starting from index {index} is less than {range}.");
            }

            int m = index + range;
            for (int i = index; i < m; i++)
            {
                int rand = UnityEngine.Random.Range(i, m);
                (list[i], list[rand]) = (list[rand], list[i]);
            }
        }

        public static T GetRandom<T>(this List<T> list)
        {
            return list.GetRandom(0, list.Count);  
        }

        public static T GetRandom<T>(this List<T> list, int index)
        {
            return list.GetRandom(index, list.Count - index);
        }

        public static T GetRandom<T>(this List<T> list, int index, int range)
        {
            if (index < 0 || index >= list.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            if (range < 0 || range > list.Count)
            {
                throw new ArgumentException("Range of elements to randomly obtain element from is out of range.");
            }
            if (index + range > list.Count)
            {
                throw new ArgumentException($"The number of elements starting from index {index} is less than {range}.");
            }

            return list[UnityEngine.Random.Range(index, index + range)];
        }

        public static List<T> GetRandoms<T>(this List<T> list, int count, bool allowDuplicate = true)
        {
            return list.GetRandoms(count, 0, list.Count, allowDuplicate);
        }

        public static List<T> GetRandoms<T>(this List<T> list, int count, int index, bool allowDuplicate = true) 
        {                     
            return list.GetRandoms(count, index, list.Count - index, allowDuplicate);
        }

        public static List<T> GetRandoms<T>(this List<T> list, int count, int index, int range, bool allowDuplicate = true)
        {
            if (index < 0 || index >= list.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            if (range < 0 || range > list.Count)
            {
                throw new ArgumentException("Range of elements to randomly obtain element from is out of range.");
            }
            if (index + range > list.Count)
            {
                throw new ArgumentException($"The number of elements starting from index {index} is less than {range}.");
            }
            if (count > range && !allowDuplicate)
            {
                throw new ArgumentException($"You don't allow duplicates, but you are trying to obtain a number of elements smaller than the range of elements.");
            }

            List<T> results;     
            
            if (index == 0 && range == list.Count && count == list.Count && !allowDuplicate)
            {
                results = new List<T>(list);                
                results.Shuffle();
                return results;
            }

            results = new List<T>(count);

            List<int> shuffledOrder = null;
            if (!allowDuplicate)
            {
                shuffledOrder = Enumerable.Range(0, range).ToList();
                shuffledOrder.Shuffle();
            }

            int m = index + range;
            for (int i = 0; i < count; i++)
            {
                if (allowDuplicate)
                {
                    results.Add(list[UnityEngine.Random.Range(index, m)]);
                }
                else
                {
                    results.Add(list[shuffledOrder[i] + index]);
                }
            }

            return results;
        }        

        public static bool SortedInAcsending<T>(this List<T> list)
        {
            return list.SortedInAcsending(Comparer<T>.Default);
        }

        public static bool SortedInAcsending<T>(this List<T> list, IComparer<T> comparer)
        {
            for (int i = 1; i < list.Count; i++)
            {
                if (comparer.GreaterThan(list[i - 1], list[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool SortedInDescending<T>(this List<T> list)
        {
            return list.SortedInDescending(Comparer<T>.Default);
        }

        public static bool SortedInDescending<T>(this List<T> list, IComparer<T> comparer)
        {
            for (int i = 1; i < list.Count; i++)
            {
                if (comparer.LessThan(list[i - 1], list[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}