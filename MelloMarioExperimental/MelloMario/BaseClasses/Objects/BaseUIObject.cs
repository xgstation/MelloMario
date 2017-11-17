using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MelloMario.Theming;

namespace MelloMario
{
    abstract class BaseUIObject : IGameObject
    {
        private static readonly object sync = new object();
        protected abstract void OnUpdate(int time);
        protected abstract void OnDraw(int time);

        public Rectangle Boundary
        {
            get
            {
                return new Rectangle(0, 0, GameConst.SCREEN_WIDTH, GameConst.SCREEN_HEIGHT);
            }
        }

        public object Sync => sync;

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
