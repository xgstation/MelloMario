using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    internal abstract class BaseUIObject : IObject
    {
        protected IPlayer Player;
        protected Vector2 RelativeOrigin;

        protected BaseUIObject(IPlayer player)
        {
            Player = player;
            RelativeOrigin = player.Camera.Location;
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
            rect.Offset(Player.Camera.Location - RelativeOrigin);
        }

        protected void UpdateOrigin()
        {
            RelativeOrigin = Player.Camera.Location;
        }

        protected abstract void OnUpdate(int time);
        protected abstract void OnDraw(int time, SpriteBatch spriteBatch);
    }
}
