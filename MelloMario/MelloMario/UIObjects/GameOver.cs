using MelloMario.Factories;
using MelloMario.Theming;
using Microsoft.Xna.Framework;

namespace MelloMario.UIObjects
{
    class GameOver : BaseUIObject
    {
        private GameModel model;
        private ISprite splashSprite;
        private ISprite textSprite;
        private ISprite coinSprite;
        private ISprite marioSprite;
        private ISprite gameOverSprite;
        private string Text;

        protected override void OnUpdate(int time)
        {
            //UpdateSprite();
        }

        protected override void OnDraw(int time, Rectangle viewport)
        {
            splashSprite.Draw(time, new Rectangle(0, 0, GameConst.SCREEN_WIDTH, GameConst.SCREEN_HEIGHT));
            textSprite.Draw(time, new Rectangle(42, 42, 800, 200));
            coinSprite.Draw(time, new Rectangle(255, 74, 26, 30));

            if (GameDatabase.Lifes > 0)
            {
                ISprite text = SpriteFactory.Instance.CreateTextSprite("WORLD");
                text.Draw(time, new Rectangle(300, 200, 80, 80));
                marioSprite.Draw(time, new Rectangle(250, 250, 40, 40));
                ISprite Life = SpriteFactory.Instance.CreateTextSprite("*  " + GameDatabase.Lifes);
                Life.Draw(time, new Rectangle(350, 250, 80, 80));
            }
            else
            {
                gameOverSprite.Draw(time, new Rectangle(400, 250, 80, 80));
            }
        }

        public GameOver(GameModel model)
        {
            this.model = model;
            splashSprite = SpriteFactory.Instance.CreatSplashSprite();
            coinSprite = SpriteFactory.Instance.CreateCoinSprite(true);
            marioSprite = SpriteFactory.Instance.CreateMarioSprite("Standard", "Standing", "GameOver", "Right");
            gameOverSprite = SpriteFactory.Instance.CreateTextSprite("GAME    OVER");
            Text = "MARIO        " + "   WORLD    TIME\n"
                + GameDatabase.Score.ToString().PadLeft(6, '0') + "    *"
                + GameDatabase.Coins.ToString().PadLeft(2, '0') + "    "
                + "1-1" + "      ";
            textSprite = SpriteFactory.Instance.CreateTextSprite(Text);
        }
    }
}
