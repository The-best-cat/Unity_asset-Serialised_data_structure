using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using UnityEngine;

namespace TheBlackCat.SerialisedDS
{
    [System.Serializable]
    public class SerializedPriorityQueue<E, P> : IEnumerable<(E, P)>, IReadOnlyCollection<(E, P)>, ICollection, ISerializationCallbackReceiver
    {
        [SerializeField] private List<HeapNode> serialisedList = new List<HeapNode>();
                
        private List<HeapNode> heap;

        public int Count => heap.Count;
        public bool IsEmpty => heap.Count == 0;
        public IComparer<P> Comparer => comparer ?? Comparer<P>.Default;
        public bool IsSynchronized => false;
        public object SyncRoot => this;

        private IComparer<P> comparer;

        public SerializedPriorityQueue()
        {
            heap = new List<HeapNode>();
        }

        public SerializedPriorityQueue(IEnumerable<(E, P)> collection, IComparer<P> comparer = null)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection), "Collection cannot be null.");
            }

            this.comparer = comparer;
            foreach (var i in collection)
            {
                Enqueue(i.Item1, i.Item2);
            }
            Heapify();
        }

        public SerializedPriorityQueue(int capacity, IComparer<P> comparer = null)
        {
            this.comparer = comparer;
            heap = new List<HeapNode>(capacity);
        }

        public SerializedPriorityQueue(IComparer<P> comparer)
        {
            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer), "Comparer cannot be null.");
            }

            this.comparer = comparer;
            heap = new List<HeapNode>();
        }

        public void Enqueue(E item, P priority)
        {            
            if (priority == null)
            {
                throw new ArgumentNullException(nameof(priority), "Priority cannot be null.");
            }

            heap.Add(new HeapNode(item, priority));
            ShiftUp(heap.Count - 1);
        }

        public E Dequeue()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Queue is empty.");
            }

            E dequeued = heap[0].Element;
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            ShiftDown(0);

            return dequeued;
        }

        public E Peek()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Queue is empty.");
            }
            return heap[0].Element;
        }

        public bool TryDequeue(out E element, out P priority)
        {
            if (IsEmpty)
            {
                element = default;
                priority = default;
                return false;
            }
            priority = heap[0].Priority;
            element = Dequeue();
            return true;
        }

        public bool TryPeek(out E element, out P priority)
        {
            if (IsEmpty)
            {
                element = default;
                priority = default;
                return false;
            }
            element = Peek();
            priority = heap[0].Priority;
            return true;
        }

        public bool ContainsElement(E item)
        {
            return IndexOfElement(item) != -1;
        }

        public bool ContainsElementWithPriority(E item, P priority)
        {
            return IndexOfElementWithPriority(item, priority) != -1;
        }

        public int IndexOfElement(E item)
        {
            if (item == null)
            {
                return -1;
            }

            for (int i = 0; i < heap.Count; i++)
            {
                if (heap[i].Element != null && heap[i].Element.Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }

        public int IndexOfElementWithPriority(E item, P priority)
        {
            if (item == null || priority == null)
            {
                return -1;
            }

            for (int i = 0; i < heap.Count; i++)
            {
                if (heap[i].Element != null && heap[i].Element.Equals(item) && Comparer.Compare(priority, heap[i].Priority) == 0)
                {
                    return i;
                }
            }
            return -1;
        }

        public bool RemoveElement(E item, [MaybeNullWhen(false)] out E removed, [MaybeNullWhen(false)] out P priority)
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

        public void ChangePriority(E item, P priority)
        {
            int index = IndexOfElement(item);
            if (index == -1)
            {
                throw new InvalidOperationException("Element is not found in the queue.");
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
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array), "Array cannot be null.");
            }
            heap.CopyTo(array, arrayIndex);
        }

        public void CopyTo(Array array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array), "Array cannot be null.");
            }
            Array.Copy(heap.ToArray(), 0, array, index, Count);
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
            Heapify();
        }

        public void OnBeforeSerialize()
        {
            serialisedList.Clear();
            foreach (var item in heap)
            {
                serialisedList.Add(item);
            }
        }

        IEnumerator<(E, P)> IEnumerable<(E, P)>.GetEnumerator()
        {
            int i = 0;
            foreach (var node in heap)
            {
                yield return (node.Element, node.Priority);
                i++;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return heap.GetEnumerator();
        }

        [Serializable]
        public struct HeapNode
        {
            public E Element;
            public P Priority;
            public HeapNode(E element, P priority)
            {
                Element = element;
                Priority = priority;
            }
        }
    }
}