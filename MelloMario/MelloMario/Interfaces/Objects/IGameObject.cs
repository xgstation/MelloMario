using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    interface IGameObject
    {
        Rectangle Boundary { get; }

        void SetLocation(Point newPoint);
        void SetWorld(IGameWorld newWorld);
        void Update(GameTime time);
        void Draw(GameTime time, Rectangle viewport, ZIndex zIndex);
    }
}
