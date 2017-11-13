using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.Collision
{
    class QuadTreeNode<T> where T : IGameObject
    {
        #region Private Members

        private const int MAXOBJECTS = 10;

        private Rectangle rect;

        private IList<EncapsulatedQuadTreeObject<T>> objects;

        private readonly QuadTreeNode<T> parent;

        private QuadTreeNode<T> topLeft;
        private QuadTreeNode<T> topRight;
        private QuadTreeNode<T> bottomLeft;
        private QuadTreeNode<T> bottomRight;
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

        internal QuadTreeNode<T> TopLeft
        {
            get
            {
                return topLeft;
            }
        }

        internal QuadTreeNode<T> TopRight
        {
            get
            {
                return topRight;
            }
        }

        internal QuadTreeNode<T> BottomLeft
        {
            get
            {
                return bottomLeft;
            }
        }

        internal QuadTreeNode<T> BottomRight
        {
            get
            {
                return bottomRight;
            }
        }

        internal QuadTreeNode<T> Parent
        {
            get
            {
                return parent;
            }
        }

        internal Rectangle Rect
        {
            get
            {
                return rect;
            }
        }

        internal int Count
        {
            get
            {
                return CountObjects();
            }
        }

        internal bool IsEmpty
        {
            get
            {
                return CountObjects() == 0 && !HasSubTree;
            }
        }

        internal bool HasSubTree
        {
            get
            {
                return topLeft != null;
            }
        }

        #endregion


        #region Internal Methods

        internal bool IsFit(EncapsulatedQuadTreeObject<T> item)
        {
            return rect.Contains(item.Boundary);
        }

        internal void Insert(EncapsulatedQuadTreeObject<T> item)
        {
            if (!rect.Contains(item.Boundary))
            {
                if (parent == null)
                {
                    Debug.WriteLine("Object does not perfectly fit, force insert");
                    Add(item);
                }
                else
                {
                    Debug.WriteLine("Object is not inserted.");
                    return;
                }
            }

            if (objects == null || topLeft == null && objects.Count < MAXOBJECTS)
            {
                Add(item);
                Debug.WriteLine("Object inserted.");
            }
            else
            {
                if (topLeft == null)
                {
                    Divide();
                }
                QuadTreeNode<T> dest = GetDestTree(item);
                if (dest == this)
                {
                    Add(item);
                    Debug.WriteLine("Object inserted.");
                }
                else
                {
                    dest.Insert(item);
                    Debug.WriteLine("Object inserted in subtree.");
                }
            }
        }

        internal void Delete(EncapsulatedQuadTreeObject<T> item, bool clean)
        {
            if (item.Owner == null)
            {
                return;
            }
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

        internal void GetObjects(ref ICollection<T> container)
        {
            if (objects != null)
            {
                foreach (EncapsulatedQuadTreeObject<T> o in objects)
                {
                    container.Add(o.RealObj);
                }
            }
            if (!HasSubTree)
            {
                return;
            }
            topLeft.GetObjects(ref container);
            topRight.GetObjects(ref container);
            bottomRight.GetObjects(ref container);
            bottomLeft.GetObjects(ref container);
        }

        internal void GetObjects(Rectangle range, ref ICollection<T> container)
        {
            if (container == null)
            {
                return;
            }
            if (range.Contains(rect))
            {
                GetObjects(ref container);
            }
            else if (range.Intersects(rect))
            {
                if (objects != null)
                {
                    foreach (EncapsulatedQuadTreeObject<T> o in objects)
                    {
                        if (range.Intersects(o.Boundary))
                        {
                            container.Add(o.RealObj);
                        }
                    }
                }
                if (HasSubTree)
                {
                    topLeft.GetObjects(range, ref container);
                    topRight.GetObjects(range, ref container);
                    bottomLeft.GetObjects(range, ref container);
                    bottomRight.GetObjects(range, ref container);
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
            if (objects == null || !objects.Contains(item))
            {
                return;
            }
            int removeIndex = objects.IndexOf(item);
            if (removeIndex < 0)
            {
                return;
            }
            objects[removeIndex] = objects[objects.Count - 1];
            objects.RemoveAt(objects.Count - 1);
        }

        private int CountObjects()
        {
            int count = 0;
            if (objects != null)
            {
                count += objects.Count;
            }
            if (!HasSubTree)
            {
                return count;
            }
            count += topLeft.CountObjects();
            count += topRight.CountObjects();
            count += bottomLeft.CountObjects();
            count += bottomRight.CountObjects();
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

                if (destTree == this)
                {
                    continue;
                }
                destTree.Insert(objects[i]);
                Remove(objects[i]);
                i--;
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
                if (!HasSubTree)
                {
                    return;
                }
                QuadTreeNode<T> destNode = GetDestTree(item);
                if (item.Owner == destNode)
                {
                    return;
                }
                QuadTreeNode<T> former = item.Owner;
                Delete(item, false);
                destNode.Insert(item);
                former.Clean();
            }
            else
            {
                parent?.Relocate(item);
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

        public void DrawBoundary(SpriteBatch spriteBatch)
        {
            var t = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            t.SetData(new[] { Color.White });
            int bw = 2; // Border width
            var r = rect;
            spriteBatch.Draw(t, new Rectangle(r.Left, r.Top, bw, r.Height), Color.Black); // Left
            spriteBatch.Draw(t, new Rectangle(r.Right, r.Top, bw, r.Height), Color.Black); // Right
            spriteBatch.Draw(t, new Rectangle(r.Left, r.Top, r.Width, bw), Color.Black); // Top
            spriteBatch.Draw(t, new Rectangle(r.Left, r.Bottom, r.Width, bw), Color.Black); // Bottom
            if (topLeft != null)
            {
                topLeft.DrawBoundary(spriteBatch);
                topRight.DrawBoundary(spriteBatch);
                bottomRight.DrawBoundary(spriteBatch);
                bottomLeft.DrawBoundary(spriteBatch);
            }

        }
    }
}