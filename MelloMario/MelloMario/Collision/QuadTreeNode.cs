using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MelloMario.Collision
{
    class QuadTreeNode<T>
    {
        private Rectangle area;
        private IList<T> objects;
        private Func<T, Rectangle> func;
        private QuadTreeNode<T> parent = null;
        private QuadTreeNode<T> topLeft = null;
        private QuadTreeNode<T> topRight = null;
        private QuadTreeNode<T> bottomLeft = null;
        private QuadTreeNode<T> bottomRight = null;


        public QuadTreeNode(Rectangle area, Func<T, Rectangle> func) : this(null, area, func)
        {
        }

        public QuadTreeNode(QuadTreeNode<T> parent, Rectangle area, Func<T, Rectangle> func)
        {
            this.parent = parent;
            this.area = area;
            this.func = func;
        }

        public Rectangle Area
        {
            get
            {
                return area;
            }
        }

        public IList<T> Objects
        {
            get
            {
                return objects;
            }
        }
        public QuadTreeNode<T> Parent
        {
            get
            {
                return parent;
            }
        }
        public QuadTreeNode<T> TopLeft
        {
            get
            {
                return topLeft;
            }
        }
        public QuadTreeNode<T> TopRight
        {
            get
            {
                return topRight;
            }
        }
        public QuadTreeNode<T> BottomLeft
        {
            get
            {
                return BottomLeft;
            }
        }
        public QuadTreeNode<T> BottomRight
        {
            get
            {
                return BottomRight;
            }
        }

        public void Insert(T item)
        {
            
        }
        private void Add(T item)
        {
            if (objects == null)
            {
                objects = new List<T>();
            }
            objects.Add(item);
        }

        private void Remove(T item)
        {
            if (objects != null && objects.Contains(item))
            {
                objects.Remove(item);
            }
        }

        private int Count()
        {
            int count =
                (objects != null ? objects.Count : 0) +
                (topLeft != null ? topLeft.Count() : 0) +
                (topRight != null ? topRight.Count() : 0) +
                (bottomLeft != null ? bottomLeft.Count() : 0) +
                (bottomRight != null ? bottomRight.Count() : 0);
            return count;
        }

        private void Divide()
        {
            Point newSize = new Point(area.Width / 2, area.Height / 2);

            topLeft = new QuadTreeNode<T>(this, new Rectangle(area.Location, newSize), func);
            topRight = new QuadTreeNode<T>(this, new Rectangle(new Point(area.Center.X, area.Top), newSize), func);
            bottomLeft = new QuadTreeNode<T>(this, new Rectangle(new Point(area.Left, area.Bottom), newSize), func);
            bottomRight = new QuadTreeNode<T>(this, new Rectangle(new Point(area.Right, area.Bottom), newSize), func);

            ISet<T> toRemove = new HashSet<T>();
            foreach (var o in objects)
            {
                QuadTreeNode<T> destTree = GetDestTree(o);
                if (destTree != this)
                {
                    destTree.Insert(o);
                    toRemove.Add(o);
                }
            }
            foreach (var o in toRemove)
            {
                Remove(o);
            }
        }

        private QuadTreeNode<T> GetDestTree(T item)
        {
            switch (func(item))
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

        private void Relocate(T item)
        {
            if (area.Contains(func(item)) && topLeft != null)
            {
                QuadTreeNode<T> destTree = GetDestTree(item);
                destTree.Add(item);
                Remove(item);
            }
            else if (parent != null)
            {
                parent.Relocate(item);
            }
        }

        private void Clean()
        {
            if (topLeft != null)
            {
                if (topLeft.Count() != 0 && topRight.Count() != 0 && bottomRight.Count() != 0 &&
                    bottomLeft.Count() != 0)
                {
                    topLeft = null;
                    topRight = null;
                    bottomRight = null;
                    bottomLeft = null;

                    if (parent != null && Count() == 0)
                    {
                        parent.Clean();
                    }
                }
            }
            else if (parent != null && Count() == 0)
            {
                parent.Clean();
            }
        }
    }
}
