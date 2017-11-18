using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    internal abstract class BaseUIObject : IObject
    {
        protected IPlayer Player;

        public BaseUIObject(IPlayer player)
        {
            Player = player;
        }

        public void Update(int time)
        {
            OnUpdate(time);
        }

        public void Draw(int time, SpriteBatch spriteBatch)
        {
            OnDraw(time, spriteBatch);
        }

        protected abstract void OnUpdate(int time);
        protected abstract void OnDraw(int time, SpriteBatch spriteBatch);
    }
}