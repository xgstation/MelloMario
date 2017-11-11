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
    class QuadTree<T> : ICollection<T> where T : IGameObject
    {
        internal static readonly int MaxObjects = 5;
        //private static readonly Point MaxSize = new Point(1600, 800);
        private IDictionary<T, EncapsulatedQuadTreeObject<T>> dictTtoEncapsulated;
        //private List<QuadTreeNode<T>> roots;
        private QuadTreeNode<T> root;

        public QuadTree(Rectangle fullCoveredArea)
        {
            root = new QuadTreeNode<T>(fullCoveredArea);
            dictTtoEncapsulated = new Dictionary<T, EncapsulatedQuadTreeObject<T>>();
        }

        public Rectangle AreaCovered { get { return root.AreaCovered; } }

        public int Count { get { return dictTtoEncapsulated.Count; } }

        public bool IsReadOnly { get { return false; } }

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
            root.DoMove(dictTtoEncapsulated[item]);
            return true;
        }
        public IEnumerator<T> GetEnumerator()
        {
            return dictTtoEncapsulated.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            if (!dictTtoEncapsulated.ContainsKey(item))
            {
                EncapsulatedQuadTreeObject<T> encapsulated = new EncapsulatedQuadTreeObject<T>(item);
                dictTtoEncapsulated.Add(item, encapsulated);
                root.Insert(encapsulated);
            }
        }

        public void Clear()
        {
            root.Clear();
        }

        public bool Contains(T item)
        {
            return dictTtoEncapsulated.ContainsKey(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            dictTtoEncapsulated.Keys.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            if (Contains(item))
            {
                root.Delete(dictTtoEncapsulated[item]);
                return true;
            }
            return false;
        }
        
    }
}
