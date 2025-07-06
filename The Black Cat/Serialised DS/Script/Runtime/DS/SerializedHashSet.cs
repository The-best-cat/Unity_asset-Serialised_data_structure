using System.Collections.Generic;
using UnityEngine;
using System;

namespace TheBlackCat.SerialisedDS
{
    [Serializable]
    public class SerializedHashSet<T> : HashSet<T>, ISerializationCallbackReceiver where T : notnull
    {
        [SerializeField] private List<T> serialisedList = new List<T>();

        public bool IsEmpty => Count == 0;

        public new IEqualityComparer<T> Comparer => base.Comparer ?? EqualityComparer<T>.Default;

        public SerializedHashSet() : base() { }

        public SerializedHashSet(IEqualityComparer<T> comparer) : base(comparer) { }

        public SerializedHashSet(int capacity, IEqualityComparer<T> comparer = null) : base(capacity, comparer) 
        {
            serialisedList = new List<T>(capacity);
        }

        public SerializedHashSet(IEnumerable<T> collection, IEqualityComparer<T> comparer = null) : base(collection, comparer)
        {
            serialisedList = new List<T>(collection);
        }

        public new bool Add(T item)
        {
            if (base.Add(item))
            {
                serialisedList.Add(item);
                return true;
            }
            return false;
        }

        public new void Clear()
        {
            base.Clear();
            serialisedList.Clear();
        }

        public new bool Remove(T item)
        {
            if (base.Remove(item))
            {
                serialisedList.Remove(item);
                return true;
            }
            return false;
        }        

        public void OnAfterDeserialize()
        {
            base.Clear();
            foreach (var item in serialisedList)
            {
                base.Add(item);
            }
        }

        public void OnBeforeSerialize()
        {
            serialisedList.Clear();
            foreach (var item in this)
            {
                serialisedList.Add(item);
            }
        }
    }
}