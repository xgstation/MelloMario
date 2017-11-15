using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace MelloMario.Collision
{
    class QuadTree<T> : ICollection<T> where T : IGameObject
    {
        enum UpdatingMode
        {
            Add,
            Remove,
            Move
        }
        private readonly IDictionary<T, EncapsulatedQuadTreeObject<T>> dictTtoEncapsulated;
        private readonly QuadTreeNode<T> quadTreeRoot;
        private readonly ConcurrentDictionary<T, UpdatingMode> toBeUpdated; 

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
            toBeUpdated = new ConcurrentDictionary<T, UpdatingMode>();
        }

        public Rectangle QuadTreeRect => quadTreeRoot.Rect;

        public int Count => dictTtoEncapsulated.Count;

        public bool IsReadOnly => false;

        public ICollection<T> GetNearby(T origin)
        {
            if (!dictTtoEncapsulated.ContainsKey(origin)) return null;
            var o = dictTtoEncapsulated[origin];
            ICollection<T> list = new List<T>();
            o.Owner?.GetObjects(ref list);
            o.Owner?.Parent?.GetObjects(ref list);
            return list;
        }
        public ICollection<T> GetObjects(Rectangle searchRange)
        {
            ICollection<T> ranged = new List<T>();
            quadTreeRoot.GetObjects(searchRange, ref ranged);
            return ranged;
        }

        public ICollection<T> GetObjects()
        {
            ICollection<T> all = new List<T>();
            quadTreeRoot.GetObjects(ref all);
            return all;
        }

        public bool Move(T item)
        {
            toBeUpdated.AddOrUpdate(
                item,
                UpdatingMode.Move,
                (key, operation) => operation == UpdatingMode.Remove ? operation : UpdatingMode.Move
            );
            return true;
        }
        private bool DoMove(T item)
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

        private void DoAdd(T item)
        {
            if (!dictTtoEncapsulated.ContainsKey(item))
            {
                EncapsulatedQuadTreeObject<T> encapsulated = new EncapsulatedQuadTreeObject<T>(item);
                dictTtoEncapsulated.Add(item, encapsulated);
                quadTreeRoot.Insert(encapsulated);
                Debug.Print(quadTreeRoot.Count.ToString());
            }
        }
        public void Add(T item)
        {
            toBeUpdated.AddOrUpdate(
                item,
                UpdatingMode.Add,
                (key, operation) => operation == UpdatingMode.Remove ? operation : UpdatingMode.Add
            );
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
            toBeUpdated.AddOrUpdate(
                item,
                UpdatingMode.Remove,
                (key, operation) => UpdatingMode.Remove
            );
            return true;
        }
        private bool DoRemove(T item)
        {
            if (Contains(item))
            {
                quadTreeRoot.Delete(dictTtoEncapsulated[item], false);
                dictTtoEncapsulated.Remove(item);
                return true;
            }
            return false;
        }

        public void Update()
        {
            foreach (var pair in toBeUpdated)
            {
                switch (pair.Value)
                {
                    case UpdatingMode.Add:
                        DoAdd(pair.Key);
                        break;
                    case UpdatingMode.Move:
                        DoMove(pair.Key);
                        break;
                    case UpdatingMode.Remove:
                        DoRemove(pair.Key);
                        break;
                }
            }
        }
    }
}
