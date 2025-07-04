using System.Collections.Generic;
using UnityEngine;

namespace TheBlackCat.SerialisedDS
{
    [System.Serializable]
    public class SerializedStack<T> : ISerializationCallbackReceiver
    {
        [SerializeField] internal List<T> serialisedList = new List<T>();

        private Stack<T> stack;

        public int Count => stack.Count;
        public bool IsEmpty => stack.Count == 0;

        public SerializedStack()
        {
            stack = new Stack<T>();
        }

        public SerializedStack(Stack<T> stack)
        {
            Debug.Assert(stack != null);

            this.stack = new Stack<T>(stack);
        }

        public SerializedStack(SerializedStack<T> serializedStack)
        {
            Debug.Assert(serializedStack != null);

            stack = new Stack<T>(serializedStack.stack);
        }

        public SerializedStack(IEnumerable<T> collection)
        {
            Debug.Assert(collection != null);

            stack = new Stack<T>(collection);            
        }

        public SerializedStack(int capacity)
        {
            stack = new Stack<T>(capacity);
        }

        public void Push(T item)
        {
            stack.Push(item);
        }

        public T Pop()
        {
            if (!IsEmpty)
            {
                return stack.Pop();
            }
            throw new System.InvalidOperationException("Stack is empty.");
        }

        public bool TryPop(out T result)
        {
            if (stack.TryPop(out T item))
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
                return stack.Peek();
            }
            throw new System.InvalidOperationException("Stack is empty.");
        }

        public bool Contains(T item)
        {
            return stack.Contains(item);
        }

        public void Clear()
        {
            stack.Clear();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            stack.CopyTo(array, arrayIndex);
        }

        public T[] ToArray()
        {
            return stack.ToArray();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return stack.GetEnumerator();
        }

        public void OnAfterDeserialize()
        {
            stack.Clear();
            foreach (var item in serialisedList)
            {
                stack.Push(item);
            }
        }

        public void OnBeforeSerialize()
        {
            serialisedList.Clear();
            foreach (var item in stack)
            {
                serialisedList.Add(item);
            }            
            serialisedList.Reverse();
        }
    }
}
