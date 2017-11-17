using Microsoft.Xna.Framework;

namespace MelloMario.Collision
{
    class EncapsulatedQuadTreeObject<T> where T : IGameObject
    {
        public EncapsulatedQuadTreeObject(T obj)
        {
            RealObj = obj;
        }

        public Rectangle Boundary => RealObj.Boundary;
        public QuadTreeNode<T> Owner { get; set; }

        public T RealObj { get; }
    }
}
