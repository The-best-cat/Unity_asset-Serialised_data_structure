using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using UnityEditor;
using System.Diagnostics.CodeAnalysis;

namespace TheBlackCat.SerialisedDS
{
    [Serializable]
    public class OrderedDictionary<K, V> : SerializedDictionary<K, V>, ISerializationCallbackReceiver
    {
        internal readonly List<SerializedKeyValuePair<K, V>> keyValues;

        public new V this[K key] 
        { 
            get
            {
                return base[key];
            }
            set
            {
                base[key] = value;
                keyValues[IndexOf(key)] = new SerializedKeyValuePair<K, V>(key, value);
            }
        }

        public new IEnumerable<K> Keys => keyValues.Select(kvp => kvp.Key);
        public new IEnumerable<V> Values => keyValues.Select(kvp => kvp.Value);     

        public OrderedDictionary() : base()        
        {          
            keyValues = new List<SerializedKeyValuePair<K, V>>();
        }

        public OrderedDictionary(IEqualityComparer<K> comparer) : base(comparer)
        {            
            keyValues = new List<SerializedKeyValuePair<K, V>>();
        }

        public OrderedDictionary(int capacity, IEqualityComparer<K> comparer = null) : base(capacity, comparer)
        {            
            keyValues = new List<SerializedKeyValuePair<K, V>>(capacity);
        }

        public OrderedDictionary(ICollection<KeyValuePair<K, V>> collection, IEqualityComparer<K> comparer = null) : base(collection.Count, comparer)
        {            
            keyValues = new List<SerializedKeyValuePair<K, V>>(collection.Count);
            foreach (var kvp in collection)
            {
                Add(kvp);
            }
        }

        public OrderedDictionary(IDictionary<K, V> dictionary, IEqualityComparer<K> comparer = null) : base(dictionary.Count, comparer)
        {            
            keyValues = new List<SerializedKeyValuePair<K, V>>(dictionary.Count);
            foreach (var kvp in dictionary)
            {
                Add(kvp);
            }
        }

        public OrderedDictionary(IEnumerable<KeyValuePair<K, V>> collection, IEqualityComparer<K> comparer = null) : base(collection.Count(), comparer)
        {            
            keyValues = new List<SerializedKeyValuePair<K, V>>(collection.Count());
            foreach (var kvp in collection)
            {
                Add(kvp);
            }
        }

        public override void Add(K key, V value)
        {
            base.Add(key, value);
            keyValues.Add(new SerializedKeyValuePair<K, V>(key, value));            
        }

        public override void Clear()
        {
            base.Clear();
            keyValues.Clear();            
        }

        public override void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array), "Array cannot be null.");
            }
            if (array.Length < Count)
            {
                throw new ArgumentException("Array is not large enough to hold the elements of the dictionary.", nameof(array));
            }
            if (arrayIndex < 0 || arrayIndex >= array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), "Array index is out of range.");
            }
            if (array.Length - arrayIndex < Count)
            {
                throw new ArgumentException("Not enough space in the array from the specified index to the end of the array.", nameof(array));
            }
            if (array.Rank != 1)
            {
                throw new ArgumentException("Array must be one-dimensional.", nameof(array));
            }

            for (int i = 0; i < keyValues.Count; i++)
            {
                array[arrayIndex + i] = new KeyValuePair<K, V>(keyValues[i].Key, keyValues[i].Value);
            }
        }

        public int IndexOf(K key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key), "Key cannot be null.");
            }
            return keyValues.FindIndex(kvp => Comparer.Equals(kvp.Key, key));
        }

        public new IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            int i = 0;
            foreach (var kvp in keyValues)
            {
                yield return new KeyValuePair<K, V>(kvp.Key, kvp.Value);
                i++;
            }
        }

        public IEnumerator<SerializedKeyValuePair<K, V>> GetSerializedEnumerator()
        {
            return keyValues.GetEnumerator();
        }

        public override bool Remove(K key, [MaybeNullWhen(false)]out V removed)
        {
            if (TryGetValue(key, out V value) && base.Remove(key, out V r))
            {
                removed = r;
                keyValues.Remove(new SerializedKeyValuePair<K, V>(key, value));
                return true;
            }
            removed = default;
            return false;
        }

        public bool RemoveAt(int index)
        {
            if (index < 0 || index >= keyValues.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
            }

            if (base.Remove(keyValues[index].Key))
            {
                keyValues.RemoveAt(index);
                return true;
            }
            return false;
        }

        public override bool TryGetValue(K key, out V result)
        {
            if (base.TryGetValue(key, out V value))
            {
                result = value;
                return true;
            }
            result = default;
            return false;
        }

        public K GetKeyAt(int index)
        {
            if (index < 0 || index >= keyValues.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
            }
            return keyValues[index].Key;
        }

        public V GetValueAt(int index)
        {
            if (index < 0 || index >= keyValues.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
            }
            return keyValues[index].Value;
        }

        public KeyValuePair<K, V> GetAt(int index)
        {
            if (index < 0 || index >= keyValues.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
            }
            return new KeyValuePair<K, V>(keyValues[index].Key, keyValues[index].Value);
        }

        public V SetAt(int index, V value)
        {
            if (index < 0 || index >= keyValues.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
            }
            var key = keyValues[index].Key;
            base[key] = value;
            keyValues[index] = new SerializedKeyValuePair<K, V>(key, value);
            return value;
        }

        public override void OnAfterDeserialize()
        {
            var baseDict = (Dictionary<K, V>)this;
            baseDict.Clear();
            foreach (var kvp in serialisedList)
            {
                if (!ContainsKey(kvp.Key))
                {
                    baseDict.Add(kvp.Key, kvp.Value);
                    keyValues.Add(new SerializedKeyValuePair<K, V>(kvp.Key, kvp.Value));
                }
            }
            KeyOccurenceHelper.CalculateKeyOccurence();
        }

        public override void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            if (UnityEditor.BuildPipeline.isBuildingPlayer)
            {
                serialisedList = serialisedList.GroupBy(kvp => kvp.Key).Select(g => g.First()).ToList();
            }
#else
            serialisedList.Clear();
            foreach (var kvp in keyValues)
            {
                serialisedList.Add(new SerializedKeyValuePair<K, V>(kvp.Key, kvp.Value));
            }
#endif
        }
    }
}