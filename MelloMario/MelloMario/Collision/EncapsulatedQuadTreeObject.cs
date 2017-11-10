using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MelloMario.Collision
{
    class EncapsulatedQuadTreeObject<T> where T: IGameObject
    {
        private T realObject;
        private QuadTreeNode<T> owner;
        public EncapsulatedQuadTreeObject(T obj)
        {
            this.realObject = obj;
        }
        public Rectangle Boundary { get { return realObject.Boundary; } }
        public QuadTreeNode<T> Owner { get { return owner; } set { owner = value; }}
        public T realObj { get { return realObject; } }
    }
}
