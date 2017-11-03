using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface IGameObject
    {
        Rectangle Boundary { get; }

        void Update(GameTime time);
        void Draw(GameTime time, Rectangle viewport, ZIndex zIndex);
    }
}
