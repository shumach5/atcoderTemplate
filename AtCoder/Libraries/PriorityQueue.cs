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
        public int Count => _list.Count;

        public PriorityQueue(IComparer comparer) : this(null, comparer) { }

        public PriorityQueue(IEnumerable<T> items, IComparer comparer)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }
            _comparer = comparer;       
            
            _list = new List<T>();
            if (items != null && items.Count() > 0) _list.AddRange(items);
            foreach (var item in items)
            {
                Add(item);
            }
        }

        public void Add(T item)
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
                if (_comparer.Compare(_list[parentIndex], _list[currIndex]) > 0)
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
                if (leftIndex < _list.Count && _comparer.Compare(_list[smallestIndex], _list[leftIndex]) > 0)
                {
                    smallestIndex = leftIndex;
                }

                if (rightIndex < _list.Count && _comparer.Compare(_list[smallestIndex], _list[rightIndex]) > 0)
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
