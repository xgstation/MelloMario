using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MelloMario.Collision
{
    class QuadTreeNode<T>
    {
        #region Private Members
        private Rectangle area;
        private IList<T> objects;
        private Func<T, Rectangle> funcTtoRec;
        private Func<T, QuadTreeNode<T>> funcTtoParentTree;
        private QuadTreeNode<T> parent = null;
        private QuadTreeNode<T> topLeft = null;
        private QuadTreeNode<T> topRight = null;
        private QuadTreeNode<T> bottomLeft = null;
        private QuadTreeNode<T> bottomRight = null;
        #endregion

        #region Construtors
        public QuadTreeNode(Rectangle area, Func<T, Rectangle> funcTtoRec, Func<T, QuadTreeNode<T>> funcTtoParentTree) :
            this(null, area, funcTtoRec, funcTtoParentTree)
        {
        }

        public QuadTreeNode(QuadTreeNode<T> parent, Rectangle area, Func<T, Rectangle> funcTtoRec,
            Func<T, QuadTreeNode<T>> funcTtoParentTree)
        {
            objects = new List<T>();
            this.parent = parent;
            this.area = area;
            this.funcTtoRec = funcTtoRec;
            this.funcTtoParentTree = funcTtoParentTree;
        }

        #endregion

        #region Public Properties
        public Rectangle Area
        {
            get { return area; }
        }

        public IList<T> Objects
        {
            get { return objects; }
        }

        public QuadTreeNode<T> Parent
        {
            get { return parent; }
        }

        public QuadTreeNode<T> TopLeft
        {
            get { return topLeft; }
        }

        public QuadTreeNode<T> TopRight
        {
            get { return topRight; }
        }

        public QuadTreeNode<T> BottomLeft
        {
            get { return BottomLeft; }
        }

        public QuadTreeNode<T> BottomRight
        {
            get { return BottomRight; }
        }

        #endregion

        #region Public Methods
        public bool IsIn(T item)
        {
            return area.Contains(funcTtoRec(item));
        }

        public IEnumerable<Tuple<T, QuadTreeNode<T>>> Insert(T item)
        {
            if (!IsIn(item))
            {
                Debug.WriteLine("Force Insert! (item does not perfectly fit!)");
                if (parent == null)
                {
                    Add(item);
                    yield return new Tuple<T, QuadTreeNode<T>>(item, this);
                }
            }
            else if (objects == null || HasSubTree() && objects.Count < QuadTree<T>.MaxObjects)
            {
                Add(item);
                yield return new Tuple<T, QuadTreeNode<T>>(item, this);
            }
            else
            {
                if (!HasSubTree())
                {
                    IEnumerable<Tuple<T, QuadTreeNode<T>>> divided = Divide();
                    foreach (Tuple<T, QuadTreeNode<T>> tuple in divided)
                    {
                        yield return tuple;
                    }
                }
                QuadTreeNode<T> destTree = GetDestTree(item);
                if (destTree == this)
                {
                    Add(item);
                    yield return new Tuple<T, QuadTreeNode<T>>(item, this);
                }
                else
                {
                    IEnumerable<Tuple<T, QuadTreeNode<T>>> squeezed = destTree.Insert(item);
                    //TODO: Verify if it needs yield return new Tuple<>(item, destTree);
                    foreach (Tuple<T, QuadTreeNode<T>> tuple in squeezed)
                    {
                        yield return tuple;
                    }
                }
            }

        }

        public bool Delete(T item)
        {
            if (funcTtoParentTree(item) != null)
            {
                if (funcTtoParentTree(item) == this)
                {
                    return Remove(item);
                }
                else
                {
                    return funcTtoParentTree(item).Delete(item);
                }
            }
            return false;
        }

        public void Clear()
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

        public void GetAll(ref ICollection<T> all)
        {
            if (objects != null)
            {
                foreach (T o in objects)
                {
                    all.Add(o);
                }
            }
            topLeft?.GetAll(ref all);
            topRight?.GetAll(ref all);
            bottomRight?.GetAll(ref all);
            bottomLeft?.GetAll(ref all);
        }

        public void GetRanged(Rectangle range, ref ICollection<T> ranged)
        {
            if (ranged == null)
            {
                return;
            }
            if (range.Contains(area))
            {
                GetAll(ref ranged);
            }
            else if (range.Intersects(area))
            {
                if (objects != null)
                {
                    foreach (T o in objects)
                    {
                        if (range.Intersects(funcTtoRec(o)))
                        {
                            ranged.Add(o);
                        }
                    }
                }
                topLeft?.GetRanged(range, ref ranged);
                topRight?.GetRanged(range, ref ranged);
                bottomLeft?.GetRanged(range, ref ranged);
                bottomRight?.GetRanged(range, ref ranged);

            }
        }

        public void DoMove(T item)
        {
            if (funcTtoParentTree(item) != null)
            {
                funcTtoParentTree(item).Relocate(item);
            }
            else
            {
                Relocate(item);
            }
        }
        #endregion

        #region Private Methods
        private void Add(T item)
        {
            if (objects == null)
            {
                objects = new List<T>();
            }
            objects.Add(item);
        }

        private bool Remove(T item)
        {
            if (objects != null && objects.Contains(item))
            {
                objects.Remove(item);
                return true;
            }
            return false;
        }

        private int Count()
        {
            int count =
                (objects?.Count ?? 0) +
                (topLeft?.Count() ?? 0) +
                (topRight?.Count() ?? 0) +
                (bottomLeft?.Count() ?? 0) +
                (bottomRight?.Count() ?? 0);
            return count;
        }

        private IEnumerable<Tuple<T, QuadTreeNode<T>>> Divide()
        {
            Point newSize = new Point(area.Width / 2, area.Height / 2);

            topLeft = new QuadTreeNode<T>(this, new Rectangle(area.Location, newSize), funcTtoRec, funcTtoParentTree);
            topRight = new QuadTreeNode<T>(this, new Rectangle(new Point(area.Center.X, area.Top), newSize), funcTtoRec,
                funcTtoParentTree);
            bottomLeft = new QuadTreeNode<T>(this, new Rectangle(new Point(area.Left, area.Bottom), newSize),
                funcTtoRec, funcTtoParentTree);
            bottomRight = new QuadTreeNode<T>(this, new Rectangle(new Point(area.Right, area.Bottom), newSize),
                funcTtoRec, funcTtoParentTree);

            ISet<T> toRelocate = new HashSet<T>();
            foreach (T o in objects)
            {
                QuadTreeNode<T> destTree = GetDestTree(o);
                if (destTree != this)
                {
                    IEnumerable<Tuple<T, QuadTreeNode<T>>> squeezed = destTree.Insert(o);
                    foreach (Tuple<T, QuadTreeNode<T>> tuple in squeezed)
                    {
                        yield return tuple;
                    }
                    toRelocate.Add(o);
                }
            }
            foreach (T o in toRelocate)
            {
                Remove(o);
            }
        }
        private QuadTreeNode<T> GetDestTree(T item)
        {
            switch (funcTtoRec(item))
            {
                case Rectangle rect when topLeft.area.Contains(rect):
                    return topLeft;
                case Rectangle rect when topRight.area.Contains(rect):
                    return topRight;
                case Rectangle rect when bottomLeft.area.Contains(rect):
                    return bottomLeft;
                case Rectangle rect when bottomRight.area.Contains(rect):
                    return bottomRight;
                default:
                    return this;
            }
        }
        private QuadTreeNode<T> Relocate(T item)
        {
            if (area.Contains(funcTtoRec(item)) && topLeft != null)
            {
                QuadTreeNode<T> destTree = GetDestTree(item);
                destTree.Add(item);
                Remove(item);
                return destTree;
            }
            return parent != null ? parent.Relocate(item) : funcTtoParentTree(item);
        }

        private bool HasSubTree()
        {
            //Debug.Assert((topLeft != null) ^ (topRight != null) ^ (bottomRight != null) ^ (bottomLeft != null),
            //    "Internal Error: Null-conditions of subtrees are not same!");
            return topLeft != null && topRight != null && bottomRight != null && bottomLeft != null;
        }
        #endregion
    }
}