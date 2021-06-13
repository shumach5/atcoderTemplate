using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.Libraries
{
    public class PriorityQueue<T> : IEnumerable<T>
    {
        private List<T> _list = new List<T>();
        private bool _ascending = true;
        private IComparer _comparer;

        public int Count => _list.Count;

        public PriorityQueue()
        {
            if (!CheckIfGenericTypeContainsIComparable()) throw new ArgumentException("Please Pass the comparer for this type");
        }
        bool CheckIfGenericTypeContainsIComparable() => typeof(T).GetInterfaces().Contains(typeof(IComparable));

        public PriorityQueue(bool ascending) : this() => _ascending = ascending;

        public PriorityQueue(IComparer comparer)
        {
            if (comparer == null) throw new ArgumentNullException("comparer is null");
            _comparer = comparer;
        }

        public PriorityQueue(IEnumerable<T> items) : this(items, true){}

        public PriorityQueue(IEnumerable<T> items, bool ascending) : this(ascending) => AddRangeInit(items);

        public PriorityQueue(IEnumerable<T> items, IComparer comparer) : this(comparer) => AddRangeInit(items);

        private void AddRangeInit(IEnumerable<T> items)
        {
            if (items == null) throw new ArgumentNullException("Initial Collection is null");

            foreach (var item in items) Push(item);
        }

        public void Push(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }
            if (_list.Count == 0)
            {
                _list.Add(item);
                return;
            }

            _list.Add(item);

            var currIndex = _list.Count - 1;
            while (currIndex > 0)
            {
                var parentIndex = GetParentIndex(currIndex);

                // parent > curr
                if (Compare(_list[parentIndex], _list[currIndex]) > 0)
                {
                    (_list[parentIndex], _list[currIndex]) = (_list[currIndex], _list[parentIndex]);
                    currIndex = parentIndex;
                }
                else
                {
                    break;
                }
            }
        }

        public T Peek() => _list.FirstOrDefault();

        public T Pop()
        {
            if (_list.Count == 0) return default(T);

            var ans = _list[0];
            _list[0] = _list.Last();
            _list.RemoveAt(_list.Count - 1);

            if (_list.Count == 0) return ans;

            var currIndex = 0;
            while (true)
            {
                var leftIndex = GetLeftIndex(currIndex);
                var rightIndex = GetRightIndex(currIndex);

                int smallestIndex = currIndex;
                if (leftIndex < _list.Count && Compare(_list[smallestIndex], _list[leftIndex]) > 0)
                {
                    smallestIndex = leftIndex;
                }

                if (rightIndex < _list.Count && Compare(_list[smallestIndex], _list[rightIndex]) > 0)
                {
                    smallestIndex = rightIndex;
                }

                if (smallestIndex != currIndex)
                {
                    (_list[currIndex], _list[smallestIndex]) = (_list[smallestIndex], _list[currIndex]);
                    currIndex = smallestIndex;
                }
                else
                {
                    break;
                }
            }

            return ans;
        }

        int Compare(T item0, T item1)
        {
            if (_comparer != null) return _comparer.Compare(item0, item1);
            return _ascending ? ((IComparable)item0).CompareTo(item1) : -((IComparable)item0).CompareTo(item1);
        }

        int GetParentIndex(int index) => (index - 1) / 2;
        int GetLeftIndex(int index) => 2 * (index + 1) - 1;
        int GetRightIndex(int index) => 2 * (index + 1);

        public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
