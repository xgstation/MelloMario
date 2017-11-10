using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace MelloMario.Collision
{
    class QuadTree<T> : ICollection<T>
    {
        public static readonly int MaxObjects = 9;
        public static readonly Point MaxSize = new Point(1600, 800);

        private IDictionary<T, QuadTreeNode<T>> dictTtoParentTree;
        private IDictionary<T, Point> dictTtoLocation;
        private IDictionary<Point, ISet<T>> dictLocationToT;
        private List<QuadTreeNode<T>> roots;
        private QuadTreeNode<T> root;
        private Func<T, Rectangle> funcTtoRec;

        public QuadTree(Rectangle area, Func<T, Rectangle> funcTtoRec)
        {
            this.funcTtoRec = funcTtoRec;
            dictTtoParentTree = new Dictionary<T, QuadTreeNode<T>>();
            dictLocationToT = new Dictionary<Point, ISet<T>>();
            dictTtoLocation = new Dictionary<T, Point>();
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
            if (!Contains(item))
            {
                return false;
            }
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
            if (item == null)
            {
                return;
            }
            IEnumerable<Tuple<T, QuadTreeNode<T>>> squeezed = root.Insert(item);
            foreach (Tuple<T, QuadTreeNode<T>> tuple in squeezed)
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
            if (!dictTtoLocation.ContainsKey(item))
            {
                dictTtoLocation.Add(item,funcTtoRec(item).Location);
                if (dictLocationToT.ContainsKey(funcTtoRec(item).Location))
                {
                    dictLocationToT[funcTtoRec(item).Location].Add(item);
                }
                else
                {
                    dictLocationToT.Add(funcTtoRec(item).Location, new HashSet<T>{item});
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
