using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MelloMario.Collision
{
    class QuadTreeNode<T> where T : IGameObject
    {
        #region Private Members

        private const int MAXOBJECTS = 5;
        private Rectangle areaCovered;
        private IList<EncapsulatedQuadTreeObject<T>> objects;

        private QuadTreeNode<T> parent = null;
        private QuadTreeNode<T> topLeft = null;
        private QuadTreeNode<T> topRight = null;
        private QuadTreeNode<T> bottomLeft = null;
        private QuadTreeNode<T> bottomRight = null;
        #endregion


        #region Construtors
        internal QuadTreeNode(Rectangle area) : this(null, area)
        {
        }

        internal QuadTreeNode(QuadTreeNode<T> parent, Rectangle areaCovered)
        {
            this.parent = parent;
            this.areaCovered = areaCovered;
        }

        #endregion


        #region Internal Properties

        internal Rectangle AreaCovered
        {
            get { return areaCovered; }
        }
        
        internal int Count
        {
            get { return CountObjects(); }
        }

        internal bool IsEmpty
        {
            get { return CountObjects() == 0 && !HasSubTree; }
        }

        internal bool HasSubTree
        {
            //Debug.Assert((topLeft != null) ^ (topRight != null) ^ (bottomRight != null) ^ (bottomLeft != null),
            //    "Internal Error: Null-conditions of subtrees are not same!");
            get { return topLeft != null && topRight != null && bottomRight != null && bottomLeft != null; }
        }

        #endregion


        #region Internal Methods

        internal bool IsFit(EncapsulatedQuadTreeObject<T> item)
        {
            return areaCovered.Contains(item.Boundary);
        }

        internal void Insert(EncapsulatedQuadTreeObject<T> item)
        {
            if (!IsFit(item))
            {
                if (parent == null)
                {
                    Debug.WriteLine("Object does not perfectly fit, force insert");
                    Add(item);
                }
                else
                {
                    return;
                }
            }

            if (objects == null || (!HasSubTree && objects.Count < MAXOBJECTS))
            {
                Add(item);
            }
            else
            {
                if (!HasSubTree)
                {
                    Divide();
                }
                else
                {
                    GetDestTree(item).Insert(item);
                }
            }
        }

        internal void Delete(EncapsulatedQuadTreeObject<T> item)
        {
            if (item.Owner == null) return;
            if (item.Owner == this)
            {
                Remove(item);
                Clean();
            }
            else
            {
                item.Owner.Delete(item);
            }
        }

        internal void Clear()
        {
            topLeft?.Clear();
            topRight?.Clear();
            bottomLeft?.Clear();
            bottomRight?.Clear();
            objects?.Clear();
            objects = null;
            topLeft = null;
            topRight = null;
            bottomLeft = null;
            bottomRight = null;
        }

        internal void GetAll(ref ICollection<T> all)
        {
            if (objects != null)
            {
                foreach (var o in objects)
                {
                    all.Add(o.realObj);
                }
            }
            if (HasSubTree)
            {
                topLeft.GetAll(ref all);
                topRight.GetAll(ref all);
                bottomRight.GetAll(ref all);
                bottomLeft.GetAll(ref all);
            }
        }

        internal void GetRanged(Rectangle range, ref ICollection<T> ranged)
        {
            if (ranged == null) return;
            if (range.Contains(areaCovered))
            {
                GetAll(ref ranged);
            }
            else if (range.Intersects(areaCovered))
            {
                if (objects != null)
                {
                    foreach (var o in objects)
                    {
                        if (range.Intersects(o.Boundary))
                        {
                            ranged.Add(o.realObj);
                        }
                    }
                }
                if (HasSubTree)
                {
                    topLeft.GetRanged(range, ref ranged);
                    topRight.GetRanged(range, ref ranged);
                    bottomLeft.GetRanged(range, ref ranged);
                    bottomRight.GetRanged(range, ref ranged);
                }
            }
        }

        internal void DoMove(EncapsulatedQuadTreeObject<T> item)
        {
            if (item.Owner != null)
            {
                item.Owner.Relocate(item);
            }
            else
            {
                Relocate(item);
            }
        }

        #endregion


        #region Private Methods

        private void Add(EncapsulatedQuadTreeObject<T> item)
        {
            if (objects == null)
            {
                objects = new List<EncapsulatedQuadTreeObject<T>>();
            }
            objects.Add(item);
            item.Owner = this;
        }

        private void Remove(EncapsulatedQuadTreeObject<T> item)
        {
            if (objects != null && objects.Contains(item))
            {
                objects.Remove(item);
            }
        }

        private int CountObjects()
        {
            int count = objects?.Count ?? 0;
            if (HasSubTree)
            {
                count += topLeft.Count + topRight.Count + bottomLeft.Count + bottomRight.Count;
            }
            return count;
        }

        private void Divide()
        {
            Point newSize = new Point(areaCovered.Width / 2, areaCovered.Height / 2);

            topLeft = new QuadTreeNode<T>(this, new Rectangle(areaCovered.Location, newSize));
            topRight = new QuadTreeNode<T>(this, new Rectangle(new Point(areaCovered.Center.X, areaCovered.Top), newSize));
            bottomLeft = new QuadTreeNode<T>(this, new Rectangle(new Point(areaCovered.Left, areaCovered.Bottom), newSize));
            bottomRight = new QuadTreeNode<T>(this, new Rectangle(new Point(areaCovered.Right, areaCovered.Bottom), newSize));

            var toBeRelocated = new Stack<EncapsulatedQuadTreeObject<T>>();
            foreach (var o in objects)
            {
                QuadTreeNode<T> destTree = GetDestTree(o);
                if (destTree != this)
                {
                    destTree.Insert(o);
                    toBeRelocated.Push(o);
                }
            }
            foreach (var o in toBeRelocated)
            {
                Remove(o);
            }
        }

        private QuadTreeNode<T> GetDestTree(EncapsulatedQuadTreeObject<T> item)
        {
            switch (item.Boundary)
            {
                case Rectangle rect when topLeft.areaCovered.Contains(rect):
                    return topLeft;
                case Rectangle rect when topRight.areaCovered.Contains(rect):
                    return topRight;
                case Rectangle rect when bottomLeft.areaCovered.Contains(rect):
                    return bottomLeft;
                case Rectangle rect when bottomRight.areaCovered.Contains(rect):
                    return bottomRight;
                default:
                    return this;
            }
        }

        private void Relocate(EncapsulatedQuadTreeObject<T> item)
        {
            if (areaCovered.Contains(item.Boundary))
            {
                if (HasSubTree)
                {
                    QuadTreeNode<T> destNode = GetDestTree(item);
                    if (item.Owner == this) return;
                    var oldOwner = item.Owner;
                    Delete(item);
                    destNode.Insert(item);
                    oldOwner.Clean();
                }
            }
        }

        private void Clean()
        {
            if (HasSubTree)
            {
                if (topLeft.IsEmpty && topRight.IsEmpty && bottomLeft.IsEmpty && bottomRight.IsEmpty)
                {
                    topLeft = null;
                    topRight = null;
                    bottomLeft = null;
                    bottomRight = null;
                }
                if (parent != null && CountObjects() == 0)
                {
                    parent.Clean();
                }
            }
            else
            {
                if (parent != null && CountObjects() == 0)
                {
                    parent.Clean();
                }
            }
        }

        #endregion
    }
}