using System;
using System.Collections.Generic;

namespace AtCoder.Libraries
{
    /// <summary>
    /// Union Find class 
    /// * Adds ranking optimization
    /// * Credits: https://www.hanachiru-blog.com/entry/2020/04/25/120000
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UnionFind<T>
    {
        private Dictionary<T, Node> _nodes;

        public UnionFind(IEnumerable<T> items = null)
        {
            _nodes = new Dictionary<T, Node>();
            if (items != null) foreach (var item in items) Add(item);
        }

        /// <summary>
        /// Adds item
        /// </summary>
        public UnionFind<T> Add(T item)
        {
            _nodes[item] = new Node(item);
            return this;
        }

        /// <summary>
        /// Unites two items
        /// </summary>
        public UnionFind<T> Unite(T x, T y)
        {
            Node.Unite(_nodes[x], _nodes[y]);
            return this;
        }

        /// <summary>
        /// Checks if two items are in the same group
        /// </summary>
        public bool IsSame(T x, T y)
            => _nodes[x].Find() == _nodes[y].Find();

        /// <summary>
        /// Returns thee size of items.
        /// </summary>
        public long Size(T x)
            => _nodes[x].Size;

        class Node
        {
            private int _rank;
            private long _size;
            private Node _parent;

            public long Size => Find()._size;

            public Node(T item)
            {
                _rank = 0;
                _size = 1;
                _parent = this;
            }

            public Node Find()
            {
                if (_parent == this) return this;

                Node parent = _parent.Find();
                _parent = parent;
                return parent;
            }

            public static bool Unite(Node x, Node y)
            {
                var rootX = x.Find();
                var rootY = y.Find();

                if (rootX == rootY) return false;

                //Check ranking
                if (rootX._rank < rootY._rank)
                {
                    rootX._parent = rootY;
                    rootY._rank = Math.Max(rootX._rank + 1, rootY._rank);
                    rootY._size = rootX._size + rootY._size;
                }
                else
                {
                    rootY._parent = rootX;
                    rootX._rank = Math.Max(rootY._rank + 1, rootX._rank);
                    rootX._size = rootX._size + rootY._size;
                }

                return true;
            }
        }
    }
}
