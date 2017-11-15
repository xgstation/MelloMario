using MelloMario.Factories;
using MelloMario.Theming;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.SplashObjects
{
    class StartScreen: IGameObject
    {
        private GameModel model;
        private ISprite textSprite;
        private ISprite startSprite;

        public Rectangle Boundary
        {
            get
            {
                return new Rectangle();
            }
        }

        public StartScreen(GameModel model)
        {
            this.model = model;
            startSprite = SpriteFactory.Instance.CreateTitle(MelloMario.ZIndex.hud);
        }

        public void Update(int time)
        {

        }

        public void Draw(int time, Rectangle viewport)
        {
            startSprite.Draw(time, new Rectangle(100, 100, 352, 176));
        }
    }
}