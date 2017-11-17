using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MelloMario.Theming;

namespace MelloMario
{
    abstract class BaseUIObject : IGameObject
    {
        protected IPlayer player;

        protected abstract void OnUpdate(int time);
        protected abstract void OnDraw(int time);

        public Rectangle Boundary
        {
            get
            {
                return new Rectangle(0, 0, GameConst.SCREEN_WIDTH, GameConst.SCREEN_HEIGHT);
            }
        }

        public BaseUIObject(IPlayer player)
        {
            this.player = player;
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
