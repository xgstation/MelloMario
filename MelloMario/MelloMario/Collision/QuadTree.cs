using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;


namespace MelloMario.Collision
{
    class QuadTree<T> : ICollection<T> where T : IGameObject
    {
        internal static readonly int MaxObjects = 10;
        //private static readonly Point MaxSize = new Point(1600, 800);
        private readonly IDictionary<T, EncapsulatedQuadTreeObject<T>> dictTtoEncapsulated;
        //private List<QuadTreeNode<T>> roots;
        private readonly QuadTreeNode<T> quadTreeRoot;

        private int width;
        private int height;

        public QuadTree(Rectangle fullCoveredArea)
        {
            width = fullCoveredArea.Width;
            height = fullCoveredArea.Height;
            float ratio = height / (float)width;
            if (ratio < 0.8)
            {
                height = width;
            }
            else if (ratio > 1.2)
            {
                width = height;
            }
            quadTreeRoot = new QuadTreeNode<T>(new Rectangle(0, 0, width, height));
            dictTtoEncapsulated = new Dictionary<T, EncapsulatedQuadTreeObject<T>>();
        }

        public Rectangle QuadTreeRect { get { return quadTreeRoot.Rect; } }

        public int Count { get { return dictTtoEncapsulated.Count; } }

        public bool IsReadOnly { get { return false; } }

        public ICollection<T> GetNearby()
        {
            return null;
        }
        public ICollection<T> GetObjects(Rectangle range)
        {
            ICollection<T> ranged = new List<T>();
            quadTreeRoot.GetRanged(range, ref ranged);
            return ranged;
        }

        public ICollection<T> GetObjects()
        {
            ICollection<T> all = new List<T>();
            quadTreeRoot.GetAll(ref all);
            return all;
        }

        public bool Move(T item)
        {
            if (!Contains(item))
            {
                return false;
            }
            quadTreeRoot.Move(dictTtoEncapsulated[item]);
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
                quadTreeRoot.Insert(encapsulated);
            }
        }

        public void Clear()
        {
            dictTtoEncapsulated.Clear();
            quadTreeRoot.Clear();
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
                quadTreeRoot.Delete(dictTtoEncapsulated[item], false);
                dictTtoEncapsulated.Remove(item);
                return true;
            }
            return false;
        }

    }
}
