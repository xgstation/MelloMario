using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface IGameObject
    {
        Rectangle Boundary { get; }

        void Update(int time);
        void Draw(int time, Rectangle viewport, ZIndex zIndex);
    }
}
