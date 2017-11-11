using Microsoft.Xna.Framework;

namespace MelloMario.Collision
{
    class EncapsulatedQuadTreeObject<T> where T : IGameObject
    {
        private T realObject;
        private QuadTreeNode<T> owner;
        public EncapsulatedQuadTreeObject(T obj)
        {
            realObject = obj;
        }
        public Rectangle Boundary { get { return realObject.Boundary; } }
        public QuadTreeNode<T> Owner { get { return owner; } set { owner = value; } }
        public T realObj { get { return realObject; } }
    }
}
