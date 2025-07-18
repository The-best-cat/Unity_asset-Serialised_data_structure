using System;
using System.Collections.Generic;
using UnityEngine;

namespace TheBlackCat.SerialisedDS
{
    [Serializable]
    public class SerializedQueue<T> : Queue<T>, ISerializationCallbackReceiver
    {
        [SerializeField] internal List<T> serialisedList = new List<T>();               
                
        public bool IsEmpty => Count == 0;

        public SerializedQueue() : base()        
        {            
        }

        public SerializedQueue(IEnumerable<T> collection) : base(collection)
        {
        }

        public SerializedQueue(int capacity) : base(capacity)
        {            
        }

        public void OnAfterDeserialize()
        {
            Clear();
            foreach (var item in serialisedList)
            {
                Enqueue(item);
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