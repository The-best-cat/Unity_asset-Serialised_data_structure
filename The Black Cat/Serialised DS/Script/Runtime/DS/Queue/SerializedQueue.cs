using System;
using System.Collections.Generic;
using UnityEngine;

namespace TheBlackCat.SerialisedDS
{
    [Serializable]
    public class SerializedQueue<T> : ISerializationCallbackReceiver
    {
        [SerializeField] internal List<T> serialisedList = new List<T>();
        
        private Queue<T> queue;
        
        public int Count => queue.Count;
        public bool IsEmpty => queue.Count == 0;

        public SerializedQueue()
        {
            queue = new Queue<T>();
        }

        public SerializedQueue(Queue<T> queue)
        {
            this.queue = new Queue<T>(queue);
        }

        public SerializedQueue(SerializedQueue<T> serializedQueue)
        {
            Debug.Assert(serializedQueue != null);

            queue = new Queue<T>(serializedQueue.queue);
        }

        public SerializedQueue(IEnumerable<T> collection)
        {
            Debug.Assert(collection != null);

            queue = new Queue<T>(collection);
        }

        public SerializedQueue(int capacity)
        {
            queue = new Queue<T>(capacity);
        }

        public void Enqueue(T item)
        {            
            queue.Enqueue(item);
        }

        public T Dequeue()
        {
            if (!IsEmpty)
            {
                return queue.Dequeue();
            }
            throw new InvalidOperationException("Queue is empty.");
        }

        public bool TryDequeue(out T result)
        {
            if (queue.TryDequeue(out T item))
            {
                result = item;
                return true;
            }
            else
            {
                result = default;
                return false;
            }
        }

        public T Peek()
        {
            if (!IsEmpty)
            {
                return queue.Peek();
            }
            throw new InvalidOperationException("Queue is empty.");
        }

        public bool TryPeek(out T result)
        {
            if (queue.TryPeek(out T item))
            {
                result = item;
                return true;
            }
            result = default;
            return false;
        }

        public bool Contains(T item)
        {
            return queue.Contains(item);
        }

        public void Clear()
        {            
            queue.Clear();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            queue.CopyTo(array, arrayIndex);
        }

        public T[] ToArray()
        {
            return queue.ToArray();
        }

        public IEnumerator<T> GetEnumerator()
        {            
            return queue.GetEnumerator();
        }        

        public void OnAfterDeserialize()
        {
            queue.Clear();
            foreach (var item in serialisedList)
            {
                queue.Enqueue(item);
            }
        }

        public void OnBeforeSerialize()
        {
            serialisedList.Clear();
            foreach (var item in queue)
            {
                serialisedList.Add(item);
            }
        }
    }
}