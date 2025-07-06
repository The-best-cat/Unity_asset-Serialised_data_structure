using System.Collections.Generic;

#if UNITY_EDITOR
namespace TheBlackCat.SerialisedDS
{
    public class KeyOccurenceHelper<K, V> : IKeyOccurenceHelper
    {
        private List<SerializedKeyValuePair<K, V>> serialisedList;
        private Dictionary<K, List<int>> occurences;

        public KeyOccurenceHelper(List<SerializedKeyValuePair<K, V>> serialisedList, IEqualityComparer<K> comparer)
        {
            this.serialisedList = serialisedList;
            occurences = new Dictionary<K, List<int>>(comparer);
        }

        public List<int> GetKeyOccurence(object key)
        {
            if (key is K k && occurences.TryGetValue(k, out List<int> indices))
            {
                return indices;
            }
            return null;
        }

        public void CalculateKeyOccurence()
        {
            occurences.Clear();
            for (int i = 0; i < serialisedList.Count; i++)
            {
                var key = serialisedList[i].Key;
                if (key != null && IsValidKey(key))
                {                    
                    if (!occurences.ContainsKey(key))
                    {
                        occurences[key] = new List<int>();
                    }
                    occurences[key].Add(i);
                }
            }
        }

        public object GetKeyAt(int index)
        {
            if (index >= 0 && index < serialisedList.Count)
            {
                return serialisedList[index].Key;
            }
            return null;
        }

        public bool IsValidKeyAt(int index)
        {            
            return IsValidKey(GetKeyAt(index));
        }

        public bool IsValidKey(object key)
        {
            try
            {
                if (key == null)
                {
                    return false;
                }

                if (key is UnityEngine.Object obj)
                {
                    return obj != null;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
#endif