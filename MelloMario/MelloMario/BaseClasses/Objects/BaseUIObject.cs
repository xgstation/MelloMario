using System.Collections.Generic;
using MelloMario.Interfaces;
using Microsoft.Xna.Framework;
using MelloMario.Theming;

namespace MelloMario
{
    abstract class BaseUIObject : IGameObject
    {
        protected IPlayer Player;
        protected IGameCamera Camera;
        protected abstract void OnUpdate(int time);
        protected abstract void OnDraw(int time);

        public Rectangle Boundary
        {
            get
            {
                return new Rectangle(0, 0, GameConst.SCREEN_WIDTH, GameConst.SCREEN_HEIGHT);
            }
        }

        public BaseUIObject(IPlayer player, IGameCamera camera)
        {
            Camera = camera;
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
