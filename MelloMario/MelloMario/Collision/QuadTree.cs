using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace MelloMario.Collision
{
    class QuadTree<T>
    {
        private const int MAXOBJECTS = 9;
        private const int MAXDEPTH = 5;

        private int level;
        private IList<T> objects;
        private Rectangle boundary;
        private QuadTree<T>[] nodes;
        private Func<T,Rectangle> func;
        public QuadTree(int level, Func<T,Rectangle> func, Rectangle boundary)
        {
            this.level = level;
            this.boundary = boundary;
            objects = new List<T>();
            nodes = new QuadTree<T>[4];
        }

        public void Insert(T obj)
        {

        }
        public void Clear()
        {
            objects.Clear();
            foreach (var quadTree in nodes)
            {
                if(nodes != null)
                    quadTree.Clear();
            }
        }

        private int GetIndex(T obj)
        {
            Rectangle objectBoundary = func(obj);
            int index = -1;
            Point middlePoint = boundary.Center;
            bool topSide = objectBoundary.Bottom < middlePoint.Y && objectBoundary;
            return index;
        }
    }
}
