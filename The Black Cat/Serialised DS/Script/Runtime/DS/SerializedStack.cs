using System.Collections.Generic;
using UnityEngine;

namespace TheBlackCat.SerialisedDS
{
    [System.Serializable]
    public class SerializedStack<T> : Stack<T>, ISerializationCallbackReceiver
    {
        [SerializeField] internal List<T> serialisedList = new List<T>();
        public bool IsEmpty => Count == 0;

        public SerializedStack() : base()
        {
        }

        public SerializedStack(IEnumerable<T> collection) : base(collection)
        {       
        }

        public SerializedStack(int capacity) : base(capacity)
        {            
        }

        public void OnAfterDeserialize()
        {
            Clear();
            foreach (var item in serialisedList)
            {
                Push(item);
            }
        }

        public void OnBeforeSerialize()
        {
            serialisedList.Clear();
            foreach (var item in this)
            {
                serialisedList.Add(item);
            }            
            serialisedList.Reverse();
        }
    }
}
