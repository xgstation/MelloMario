using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace MelloMario.Collision
{
    class QuadTreeNode<T> where T : IGameObject
    {
        #region Private Members

        private const int MAXOBJECTS = 10;

        private Rectangle rect;

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

        private QuadTreeNode(QuadTreeNode<T> parent, Rectangle rect)
        {
            this.parent = parent;
            this.rect = rect;
        }

        #endregion


        #region Internal Properties

        internal QuadTreeNode<T> TopLeft => topLeft;

        internal QuadTreeNode<T> TopRight => topRight;

        internal QuadTreeNode<T> BottomLeft => bottomLeft;

        internal QuadTreeNode<T> BottomRight => bottomRight;

        internal QuadTreeNode<T> Parent => parent;


        internal Rectangle Rect => rect;

        internal int Count => CountObjects();

        internal bool IsEmpty => CountObjects() == 0 && !HasSubTree;

        internal bool HasSubTree => topLeft != null;

        #endregion


        #region Internal Methods

        internal bool IsFit(EncapsulatedQuadTreeObject<T> item)
        {
            return rect.Contains(item.Boundary);
        }

        internal void Insert(EncapsulatedQuadTreeObject<T> item)
        {
            if (rect.Contains(item.Boundary))
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

            if (objects == null || (topLeft == null && objects.Count + 1 <= MAXOBJECTS))
            {
                Add(item);
            }
            else
            {
                if (topLeft == null)
                {
                    Divide();
                }
                var dest = GetDestTree(item);
                if (dest == this)
                {
                    Add(item);
                }
                else
                {
                    dest.Insert(item);
                }
            }
        }

        internal void Delete(EncapsulatedQuadTreeObject<T> item, bool clean)
        {
            if (item.Owner != null)
            {
                if (item.Owner == this)
                {
                    Remove(item);
                    if (clean)
                    {
                        Clean();
                    }
                }
                else
                {
                    item.Owner.Delete(item, clean);
                }
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
                foreach (EncapsulatedQuadTreeObject<T> o in objects)
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
            if (ranged == null)
            {
                return;
            }
            if (range.Contains(rect))
            {
                GetAll(ref ranged);
            }
            else if (range.Intersects(rect))
            {
                if (objects != null)
                {
                    foreach (EncapsulatedQuadTreeObject<T> o in objects)
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

        internal void Move(EncapsulatedQuadTreeObject<T> item)
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
            item.Owner = this;
            objects.Add(item);
        }

        private void Remove(EncapsulatedQuadTreeObject<T> item)
        {
            if (objects != null && objects.Contains(item))
            {
                int removeIndex = objects.IndexOf(item);
                if (removeIndex >= 0)
                {
                    objects[removeIndex] = objects[objects.Count - 1];
                    objects.RemoveAt(objects.Count - 1);
                }
            }
        }

        private int CountObjects()
        {
            int count = 0;
            if (objects != null)
            {
                count += objects.Count;
            }
            if (HasSubTree)
            {
                count += topLeft.CountObjects();
                count += topRight.CountObjects();
                count += bottomLeft.CountObjects();
                count += bottomRight.CountObjects();
            }
            return count;
        }

        private void Divide()
        {
            Point newSize = new Point(rect.Width / 2, rect.Height / 2);
            topLeft = new QuadTreeNode<T>(this, new Rectangle(new Point(rect.Left, rect.Top), newSize));
            topRight = new QuadTreeNode<T>(this, new Rectangle(new Point(rect.Center.X, rect.Top), newSize));
            bottomLeft = new QuadTreeNode<T>(this, new Rectangle(new Point(rect.Left, rect.Center.Y), newSize));
            bottomRight = new QuadTreeNode<T>(this, new Rectangle(new Point(rect.Center.X, rect.Center.Y), newSize));

            for (int i = 0; i < objects.Count; i++)
            {
                QuadTreeNode<T> destTree = GetDestTree(objects[i]);

                if (destTree != this)
                {
                    // Insert to the appropriate tree, remove the object, and back up one in the loop
                    destTree.Insert(objects[i]);
                    Remove(objects[i]);
                    i--;
                }
            }
        }

        private QuadTreeNode<T> GetDestTree(EncapsulatedQuadTreeObject<T> item)
        {
            switch (item.Boundary)
            {
                case Rectangle rect when topLeft.rect.Contains(rect):
                    return topLeft;
                case Rectangle rect when topRight.rect.Contains(rect):
                    return topRight;
                case Rectangle rect when bottomLeft.rect.Contains(rect):
                    return bottomLeft;
                case Rectangle rect when bottomRight.rect.Contains(rect):
                    return bottomRight;
                default:
                    return this;
            }
        }

        private void Relocate(EncapsulatedQuadTreeObject<T> item)
        {
            if (rect.Contains(item.Boundary))
            {
                if (HasSubTree)
                {
                    var destNode = GetDestTree(item);
                    if (item.Owner != destNode)
                    {
                        var former = item.Owner;
                        Delete(item, false);
                        destNode.Insert(item);
                        former.Clean();
                    }
                }
            }
            else
            {
                if (parent != null)
                {
                    parent.Relocate(item);
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