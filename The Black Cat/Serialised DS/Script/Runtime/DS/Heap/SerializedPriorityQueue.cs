using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace TheBlackCat.SerialisedDS
{
    [System.Serializable]
    public class SerializedPriorityQueue<TElement, TPriority> : ISerializationCallbackReceiver
    {
        [SerializeField] private List<HeapNode> serialisedList = new List<HeapNode>();

        public IComparer<TPriority> Comparer => comparer ?? Comparer<TPriority>.Default;

        private IComparer<TPriority> comparer;
        private List<HeapNode> heap;

        public int Count => heap.Count;
        public bool IsEmpty => heap.Count == 0;

        public SerializedPriorityQueue()
        {
            heap = new List<HeapNode>();
        }

        public SerializedPriorityQueue(IEnumerable<(TElement, TPriority)> collection, IComparer<TPriority> comparer = null)
        {
            Debug.Assert(collection != null);

            this.comparer = comparer;
            foreach (var i in collection)
            {
                Enqueue(i.Item1, i.Item2);
            }
            Heapify();
        }

        public SerializedPriorityQueue(SerializedPriorityQueue<TElement, TPriority> serializedQueue, IComparer<TPriority> comparer = null)
        {
            Debug.Assert(serializedQueue != null);

            this.comparer = comparer;
            heap = new List<HeapNode>(serializedQueue.heap);
            Heapify();
        }

        public SerializedPriorityQueue(int capacity, IComparer<TPriority> comparer = null)
        {
            this.comparer = comparer;
            heap = new List<HeapNode>(capacity);
        }

        public SerializedPriorityQueue(IComparer<TPriority> comparer)
        {
            Debug.Assert(comparer != null);

            this.comparer = comparer;
            heap = new List<HeapNode>();
        }

        public void Enqueue(TElement item, TPriority priority)
        {            
            Debug.Assert(priority != null);

            heap.Add(new HeapNode(item, priority));
            ShiftUp(heap.Count - 1);
        }

        public TElement Dequeue()
        {
            if (IsEmpty)
            {
                throw new System.InvalidOperationException("Queue is empty.");
            }

            TElement dequeued = heap[0].Element;
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            ShiftDown(0);

            return dequeued;
        }

        public TElement Peek()
        {
            if (IsEmpty)
            {
                throw new System.InvalidOperationException("Queue is empty.");
            }
            return heap[0].Element;
        }

        public bool TryDequeue(out TElement result)
        {
            if (IsEmpty)
            {
                result = default;
                return false;
            }
            result = Dequeue();
            return true;
        }

        public bool TryPeek(out TElement result)
        {
            if (IsEmpty)
            {
                result = default;
                return false;
            }
            result = Peek();
            return true;
        }

        public bool ContainsElement(TElement item)
        {
            return IndexOfElement(item) != -1;
        }

        public bool ContainsElementWithPriority(TElement item, TPriority priority)
        {
            return IndexOfElementWithPriority(item, priority) != -1;
        }

        public int IndexOfElement(TElement item)
        {
            if (item == null)
            {
                return -1;
            }

            for (int i = 0; i < heap.Count; i++)
            {
                if (heap[i].Element.Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }

        public int IndexOfElementWithPriority(TElement item, TPriority priority)
        {
            if (item == null || priority == null)
            {
                return -1;
            }

            for (int i = 0; i < heap.Count; i++)
            {
                if (heap[i].Element.Equals(item) && Comparer.Compare(priority, heap[i].Priority) == 0)
                {
                    return i;
                }
            }
            return -1;
        }

        public bool RemoveElement(TElement item, [MaybeNullWhen(false)] out TElement removed, [MaybeNullWhen(false)] out TPriority priority)
        {
            int index = IndexOfElement(item);
            if (index == -1)
            {
                removed = default;
                priority = default;
                return false;
            }

            removed = heap[index].Element;
            priority = heap[index].Priority;
            heap[index] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);

            if (!ShiftDown(index))
            {
                ShiftUp(index);
            }
            return true;
        }

        public void ChangePriority(TElement item, TPriority priority)
        {
            int index = IndexOfElement(item);
            if (index == -1)
            {
                throw new System.InvalidOperationException("Element is not found in the queue.");
            }

            heap[index] = new HeapNode(heap[index].Element, priority);

            if (!ShiftDown(index))
            {
                ShiftUp(index);
            }
        }

        public void Clear()
        {
            heap.Clear();
            serialisedList.Clear();
        }

        private int GetParentIndex(int index)
        {
            return (index - 1) / 2;
        }

        private int GetLeftChildIndex(int index)
        {
            return 2 * index + 1;
        }

        private bool ShiftUp(int index)
        {
            bool shifted = false;
            while (index > 0)
            {
                int parent = GetParentIndex(index);
                if (Comparer.Compare(heap[index].Priority, heap[parent].Priority) < 0)
                {
                    (heap[index], heap[parent]) = (heap[parent], heap[index]);
                    index = parent;
                    shifted = true;
                }
                else
                {
                    break;
                }
            }
            return shifted;
        }

        private bool ShiftDown(int index)
        {
            bool shifted = false;

            while (true)
            {
                int smallest = index;
                int leftChild = GetLeftChildIndex(index);
                int rightChild = leftChild + 1;

                if (leftChild < heap.Count && Comparer.Compare(heap[leftChild].Priority, heap[smallest].Priority) < 0)
                {
                    smallest = leftChild;
                }
                if (rightChild < heap.Count && Comparer.Compare(heap[rightChild].Priority, heap[smallest].Priority) < 0)
                {
                    smallest = rightChild;
                }

                if (smallest != index)
                {
                    (heap[index], heap[smallest]) = (heap[smallest], heap[index]);
                    index = smallest;
                    shifted = true;
                }
                else
                {
                    break;
                }
            }
            return shifted;
        }

        private void Heapify()
        {
            int start = GetParentIndex(heap.Count - 1);
            for (int i = start; i >= 0; i--)
            {
                ShiftDown(i);
            }
        }

        public void CopyTo(HeapNode[] array, int arrayIndex)
        {
            heap.CopyTo(array, arrayIndex);
        }

        public void TrimExcess()
        {
            heap.TrimExcess();
        }

        public HeapNode[] ToArray()
        {
            return heap.ToArray();
        }

        public IEnumerator<HeapNode> GetEnumerator()
        {
            return heap.GetEnumerator();
        }

        public void OnAfterDeserialize()
        {
            heap.Clear();
            foreach (var item in serialisedList)
            {
                heap.Add(item);
            }
        }

        public void OnBeforeSerialize()
        {
            serialisedList.Clear();
            foreach (var item in heap)
            {
                serialisedList.Add(item);
            }
        }

        [System.Serializable]
        public struct HeapNode
        {
            public TElement Element;
            public TPriority Priority;
            public HeapNode(TElement element, TPriority priority)
            {
                Element = element;
                Priority = priority;
            }
        }
    }
}