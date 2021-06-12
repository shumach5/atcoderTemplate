using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AtCoder.Libraries
{
    public class PriorityQueue<T> : IEnumerable<T>
    {
        private List<T> _list;
        private IComparer _comparer;
        private bool _ascending;

        public int Count => _list.Count;

        PriorityQueue()
        {
            _list = new List<T>();
        }

        public PriorityQueue(bool ascending) : this()
        {
            if (!CheckIfGenericTypeContainsIComparable()) throw new ArgumentException("Please Pass the comparer for this type");
            _ascending = ascending;
        }

        public PriorityQueue(IComparer comparer) : this()
        {
            if (comparer != null) _comparer = comparer;
            else if (comparer == null && !CheckIfGenericTypeContainsIComparable()) throw new ArgumentException("Please Pass the comparer for this type");
        }

        public PriorityQueue(IEnumerable<T> items) : this(items, true) => AddRangeInit(items);

        public PriorityQueue(IEnumerable<T> items, bool ascending) : this(ascending) => AddRangeInit(items);

        public PriorityQueue(IEnumerable<T> items, IComparer comparer) : this(comparer) => AddRangeInit(items);

        bool CheckIfGenericTypeContainsIComparable() => typeof(T).GetInterfaces().Contains(typeof(IComparable));

        private void AddRangeInit(IEnumerable<T> items)
        {
            if (items == null) throw new ArgumentNullException("Initial Collection is null");

            foreach (var item in items)
            {
                Push(item);
            }
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
