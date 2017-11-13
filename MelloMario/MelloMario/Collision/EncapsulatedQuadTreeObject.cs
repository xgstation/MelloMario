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
        public Rectangle Boundary
        {
            get
            {
                Point location = realObject.Boundary.Location;
                if (location.X < 0)
                {
                    location.X = 0;
                }
                if (location.Y < 0)
                {
                    location.Y = 0;
                }
                return new Rectangle(location, realObject.Boundary.Size);
            }
        }
        public QuadTreeNode<T> Owner { get { return owner; } set { owner = value; } }
        public T realObj { get { return realObject; } }
    }
}
