using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AtCoder.Libraries
{
    public class PriorityQueue<T> : IEnumerable<T> where T : IComparable
    {
        private List<T> _list;
        private IComparer _comparer;
        private bool _ascending = true;

        public int Count => _list.Count;

        public PriorityQueue() 
        {
            _list = new List<T>();
        }

        public PriorityQueue(IComparer comparer) : this() 
        {
            if (comparer != null) _comparer = comparer; 
        }

        public PriorityQueue(IEnumerable<T> items, IComparer comparer = null) : this(comparer)
        {
            if (items != null && items.Count() > 0)
            {
                _list.AddRange(items);
                foreach (var item in items)
                {
                    Push(item);
                }
            }
        }

        public void Push(T item)
        {
            if (item is null)
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
            return _ascending ? item0.CompareTo(item1) : -item0.CompareTo(item1);
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
