using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    internal abstract class BaseUIObject : IObject
    {
        protected IPlayer Player;
        protected Point RelativeOrigin;

        protected BaseUIObject(IPlayer player)
        {
            // TODO: use another spritebatch
            Player = player;
            RelativeOrigin = player.Camera.Viewport.Location;
        }

        public void Update(int time)
        {
            OnUpdate(time);
        }

        public void Draw(int time, SpriteBatch spriteBatch)
        {
            OnDraw(time, spriteBatch);
        }

        protected void Offset(ref Rectangle rect)
        {
            rect.Offset(Player.Camera.Viewport.Location - RelativeOrigin);
        }

        protected void UpdateOrigin()
        {
            RelativeOrigin = Player.Camera.Viewport.Location;
        }

        protected abstract void OnUpdate(int time);
        protected abstract void OnDraw(int time, SpriteBatch spriteBatch);
    }
}
