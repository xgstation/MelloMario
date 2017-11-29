using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.Objects.UserInterfaces
{
    using Microsoft.Xna.Framework.Graphics;

    internal class SplashScreen : BaseUserInterface
    {
        public IScreenState State { get; set; }

        public SplashScreen(IPlayer player) : base(player)
        {
        }

        protected override void OnUpdate(int time)
        {
            throw new NotImplementedException();
        }

        protected override void OnDraw(int time, SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}
