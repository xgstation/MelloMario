using MelloMario.Factories;
using MelloMario.Theming;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.SplashObjects
{
    class GameWon : IGameObject
    {
        private GameModel model;
        private ISprite splashSprite;
        private ISprite textSprite;
        private ISprite coinSprite;
        private ISprite marioSprite;
        private ISprite gameOverSprite;
        private string Text;

        public Rectangle Boundary
        {
            get
            {
                return new Rectangle();
            }
        }

        public GameWon(GameModel model)
        {
            this.model = model;
            splashSprite = SpriteFactory.Instance.CreatSplashSprite();
            coinSprite = SpriteFactory.Instance.CreateCoinSprite(true);
            marioSprite = SpriteFactory.Instance.CreateMarioSprite("Standard", "Standing", "Normal", "Right");
            Text= "MARIO        " + "   WORLD    TIME\n"
                + model.Score.ToString().PadLeft(6, '0') + "    *"
                + model.Coins.ToString().PadLeft(2, '0') + "    "
                + "1-1" + "      ";
            textSprite = SpriteFactory.Instance.CreateTextSprite(Text);
        }

        private void UpdateSprite()
        {
            //string Text = "MARIO        " + "   WORLD    TIME\n"
               // + model.Score.ToString().PadLeft(6, '0') + "    *"
                //+ model.Coins.ToString().PadLeft(2, '0') + "    "
                //+ "1-1" + "      ";

            //textSprite = SpriteFactory.Instance.CreateTextSprite(Text);
        }

        public void Update(int time)
        {
            //UpdateSprite();
        }

        public void Draw(int time, Rectangle viewport)
        {
            splashSprite.Draw(time, new Rectangle(0, 0, GameConst.SCREEN_WIDTH, GameConst.SCREEN_HEIGHT));
            textSprite.Draw(time, new Rectangle(10, 10, 80, 80));
            ISprite text = SpriteFactory.Instance.CreateTextSprite("You won!\n\nPress R to restart\n\nPress Q to quit");
            text.Draw(time, new Rectangle(200, 200, 200, 80));

        }
    }
}
