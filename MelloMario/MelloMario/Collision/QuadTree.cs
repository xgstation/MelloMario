using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace MelloMario.Collision
{
    class QuadTree<T> : ICollection<T>
    {
        private const int MAXOBJECTS = 9;
        private const int MAXDEPTH = 5;

        private readonly IDictionary<T, QuadTreeNode<T>> dictTtoParentTree;
        private readonly QuadTreeNode<T> root;
        private readonly Func<T, Rectangle> funcTtoRec;

        public QuadTree(Rectangle area, Func<T, Rectangle> funcTtoRec)
        {
            this.funcTtoRec = funcTtoRec;
            root = new QuadTreeNode<T>(area, funcTtoRec, t => dictTtoParentTree?[t]);
        }
        public Rectangle Area { get { return root.Area; } }

        public ICollection<T> GetRanged(Rectangle range)
        {
            ICollection<T> ranged = new List<T>();
            root.GetRanged(range, ref ranged);
            return ranged;
        }

        public ICollection<T> GetAll()
        {
            ICollection<T> all = new List<T>();
            root.GetAll(ref all);
            return all;
        }

        public bool DoMove(T item)
        {
            if (!Contains(item)) return false;
            root.DoMove(item);
            return true;
        }
        public IEnumerator<T> GetEnumerator()
        {
            return dictTtoParentTree.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            if (item == null) return;
            var squeezed = root.Insert(item);
            foreach (var tuple in squeezed)
            {
                if (dictTtoParentTree.ContainsKey(tuple.Item1))
                {
                    dictTtoParentTree[tuple.Item1] = tuple.Item2;
                }
                else
                {
                    dictTtoParentTree.Add(tuple.Item1, tuple.Item2);
                }
            }
        }

        public void Clear()
        {
            root.Clear();
        }

        public bool Contains(T item)
        {
            return item != null && dictTtoParentTree.ContainsKey(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            dictTtoParentTree.Keys.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            if (Contains(item))
            {
                root.Delete(item);
                dictTtoParentTree.Remove(item);
                return true;
            }
            return false;
        }

        public int Count { get { return dictTtoParentTree.Count; } }
        public bool IsReadOnly { get { return false; } }
    }
}
