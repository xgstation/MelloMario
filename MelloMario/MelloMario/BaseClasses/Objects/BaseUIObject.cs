using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MelloMario.Theming;

namespace MelloMario
{
    abstract class BaseUIObject : IGameObject
    {
        private IEnumerable<ISprite> sprites;

        protected abstract void OnUpdate(int time);
        protected abstract void OnDraw(int time, Rectangle viewport);

        public Rectangle Boundary
        {
            get
            {
                return new Rectangle(0, 0, GameConst.SCREEN_WIDTH, GameConst.SCREEN_HEIGHT);
            }
        }

        public BaseUIObject(IEnumerable<ISprite> sprites)
        {
            this.sprites = sprites;
        }

        public void Update(int time)
        {
            OnUpdate(time);
        }

        public void Draw(int time, Rectangle viewport)
        {
            OnDraw(time, viewport);

            foreach (ISprite sprite in sprites)
            {
                sprite.Draw(time, new Rectangle(Boundary.Location - viewport.Location, Boundary.Size));
            }
        }
    }
}
