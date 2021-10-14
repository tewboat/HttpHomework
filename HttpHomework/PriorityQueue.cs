using System;
using System.Collections;
using System.Collections.Generic;

namespace HttpHomework
{
    public class PriorityQueue<T> : IEnumerable<T>
    {
        public int MaxCount { get; }
        public int Count => linkedList.Count;
        private Comparer<T> comparer;
        private readonly LinkedList<T> linkedList = new LinkedList<T>();

        public PriorityQueue(int maxCount) : this(maxCount, Comparer<T>.Default)
        {
        }

        public PriorityQueue(int maxCount, Comparer<T> comparer)
        {
            MaxCount = maxCount;
            this.comparer = comparer;
        }

        public void Enqueue(T value)
        {
            if (MaxCount == 0)
                return;
            if (Count == MaxCount)
            {
                if (comparer.Compare(value, linkedList.First.Value) < 0)
                    return;
                linkedList.RemoveFirst();
            }

            if (linkedList.Count == 0)
            {
                linkedList.AddFirst(value);
                return;
            }

            var currentNode = linkedList.First;
            while (currentNode != null && comparer.Compare(value, currentNode.Value) > 0)
                currentNode = currentNode.Next;
            if (currentNode == null)
                linkedList.AddLast(value);
            else
                linkedList.AddBefore(currentNode, value);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return linkedList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}