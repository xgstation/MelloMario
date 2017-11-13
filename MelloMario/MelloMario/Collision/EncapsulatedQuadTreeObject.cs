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
        public Rectangle Boundary { get {
            Rectangle b = realObject.Boundary;
            if (b.Location.X < 0)
            {
                b.Location.X = 0;
            }
            if (b.Location.Y < 0)
            {
                b.Location.Y = 0;
            }
            return b;
        } }
        public QuadTreeNode<T> Owner { get { return owner; } set { owner = value; } }
        public T realObj { get { return realObject; } }
    }
}
