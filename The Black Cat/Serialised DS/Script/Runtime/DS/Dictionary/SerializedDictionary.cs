using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace TheBlackCat.SerialisedDS
{
    [Serializable]
    public class SerializedDictionary<K, V> : Dictionary<K, V>, ISerializationCallbackReceiver where K : notnull
    {
        [SerializeField] protected List<SerializedKeyValuePair<K, V>> serialisedList;

#if UNITY_EDITOR
        internal KeyOccurenceHelper<K, V> KeyOccurenceHelper
        {
            get
            {
                if (keyOccurenceHelper == null)
                {
                    keyOccurenceHelper = new KeyOccurenceHelper<K, V>(serialisedList, Comparer);
                }
                return keyOccurenceHelper;
            }
        }

        private KeyOccurenceHelper<K, V> keyOccurenceHelper;
#endif

        public new V this[K key]
        {
            get
            {
                return base[key];
            }
            set
            {
                base[key] = value;

                bool found = false;
                for (int i = 0; i < serialisedList.Count; i++)
                {
                    if (Comparer.Equals(serialisedList[i].Key, key))
                    {
                        serialisedList[i] = new SerializedKeyValuePair<K, V>(key, value);
                        found = true;                        
                    }
                }

                if (!found)
                {
                    serialisedList.Add(new SerializedKeyValuePair<K, V>(key, value));
                }
            }
        }

        public bool IsEmpty => Count == 0;
        public new IEqualityComparer<K> Comparer => base.Comparer ?? EqualityComparer<K>.Default;

        public SerializedDictionary() : base()        
        {
            serialisedList = new List<SerializedKeyValuePair<K, V>>();
        }

        public SerializedDictionary(IDictionary<K, V> dictionary, IEqualityComparer<K> comparer = null) : base(dictionary, comparer)
        {
            serialisedList = new List<SerializedKeyValuePair<K, V>>(dictionary.Count);
            foreach (var kvp in dictionary)
            {
                serialisedList.Add(new SerializedKeyValuePair<K, V>(kvp.Key, kvp.Value));
            }
        }

        public SerializedDictionary(IEnumerable<KeyValuePair<K, V>> collection, IEqualityComparer<K> comparer = null) : base(collection, comparer)
        {
            serialisedList = new List<SerializedKeyValuePair<K, V>>(collection.Count());
            foreach (var kvp in collection)
            {
                serialisedList.Add(kvp);
            }
        }

        public SerializedDictionary(ICollection<KeyValuePair<K, V>> collection, IEqualityComparer<K> comparer = null) : base(collection, comparer)
        {
            serialisedList = new List<SerializedKeyValuePair<K, V>>(collection.Count);
            foreach (var kvp in collection)
            {
                serialisedList.Add(new SerializedKeyValuePair<K, V>(kvp.Key, kvp.Value));
            }
        }

        public SerializedDictionary(IEqualityComparer<K> comparer) : base(comparer)
        {
            serialisedList = new List<SerializedKeyValuePair<K, V>>();
        }

        public SerializedDictionary(int capacity, IEqualityComparer<K> comparer = null) : base(capacity, comparer)
        {
            serialisedList = new List<SerializedKeyValuePair<K, V>>(capacity);
        }

        public virtual new void Add(K key, V value)
        {
            base.Add(key, value);            
            serialisedList.Add(new SerializedKeyValuePair<K, V>(key, value));
        }

        public void Add(KeyValuePair<K, V> item)
        {
            Add(item.Key, item.Value);
        }

        public virtual new void Clear()
        {
            base.Clear();            
            serialisedList.Clear();
        }

        public virtual bool Contains(KeyValuePair<K, V> item)
        {
            return base.TryGetValue(item.Key, out V value) && item.Value != null && item.Value.Equals(value);
        }

        public virtual new bool ContainsKey(K key)
        {
            return base.ContainsKey(key);
        }

        public virtual new bool ContainsValue(V value)
        {
            return base.ContainsValue(value);
        }

        public virtual void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
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

            for (int i = 0; i < serialisedList.Count; i++)
            {
                array[arrayIndex + i] = new KeyValuePair<K, V>(serialisedList[i].Key, serialisedList[i].Value);
            }
        }

        public virtual new bool Remove(K key, [MaybeNullWhen(false)] out V removed)
        {
            if (TryGetValue(key, out V value))
            {
                removed = value;
                base.Remove(key);                
                serialisedList.Remove(new SerializedKeyValuePair<K, V>(key, value));
                return true;
            }
            removed = default;
            return false;
        }

        public bool Remove(KeyValuePair<K, V> item, [MaybeNullWhen(false)] out V removed)
        {
            bool remove = Remove(item.Key, out V r);
            removed = r;
            return remove;
        }

        public virtual new bool TryGetValue(K key, out V result)
        {
            if (base.TryGetValue(key, out V value))
            {
                result = value;
                return true;
            }
            result = default;
            return false;
        }

        public virtual new bool TryAdd(K key, V value)
        {
            if (!ContainsKey(key))
            {
                Add(key, value);
                return true;
            }
            return false;
        }

        public virtual void OnAfterDeserialize()
        {
            base.Clear();
            foreach (var kvp in serialisedList)
            {
                if (KeyOccurenceHelper.IsValidKey(kvp.Key) && !ContainsKey(kvp.Key))
                {
                    base.Add(kvp.Key, kvp.Value);           
                }
            }
            KeyOccurenceHelper.CalculateKeyOccurence();
        }

        public virtual void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            if (UnityEditor.BuildPipeline.isBuildingPlayer)
            {
                serialisedList = serialisedList.GroupBy(kvp => kvp.Key).Select(g => g.First()).ToList();
            }
#else
            serialisedList.Clear();
            foreach (var kvp in this)
            {
                serialisedList.Add(new SerializedKeyValuePair<K, V>(kvp.Key, kvp.Value));
            }
#endif
        }
    }
}