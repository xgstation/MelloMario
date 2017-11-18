using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MelloMario.Theming;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    abstract class BaseUIObject : IObject
    {
        protected IPlayer Player;

        protected abstract void OnUpdate(int time);
        protected abstract void OnDraw(int time, SpriteBatch spriteBatch);

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
    }
}
