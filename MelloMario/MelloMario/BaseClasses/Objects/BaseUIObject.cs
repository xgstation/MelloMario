using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MelloMario.Theming;

namespace MelloMario
{
    abstract class BaseUIObject : IObject
    {
        protected IPlayer Player;

        protected abstract void OnUpdate(int time);
        protected abstract void OnDraw(int time);

        public BaseUIObject(IPlayer player)
        {
            Player = player;
        }

        public void Update(int time)
        {
            OnUpdate(time);
        }

        public void Draw(int time)
        {
            OnDraw(time);
        }
    }
}
