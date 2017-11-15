using MelloMario.Factories;
using MelloMario.Theming;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.SplashObjects
{
    class GameOver
    {
        private GameModel model;
        private ISprite splashSprite;
        private ISprite textSprite;
        private ISprite coinSprite;
        private ISprite marioSprite;
        private ISprite gameOverSprite;

        public GameOver(GameModel model)
        {
            this.model = model;
            splashSprite = SpriteFactory.Instance.CreatSplashSprite();
            coinSprite = SpriteFactory.Instance.CreateCoinSprite(true);
            marioSprite = SpriteFactory.Instance.CreateMarioSprite("Normal", "Standing", "Normal", "Right");
            gameOverSprite = SpriteFactory.Instance.CreateTextSprite("GAME    OVER");
        }

        private void UpdateSprite()
        {
            string Text = "MARIO        " + "   WORLD    TIME\n"
                + model.Score.ToString().PadLeft(6, '0') + "    *"
                + model.Coins.ToString().PadLeft(2, '0') + "    "
                + "1-1" + "      ";

            textSprite = SpriteFactory.Instance.CreateTextSprite(Text);
        }

        public void Update(int time)
        {
            UpdateSprite();
        }

        public void Draw(int time)
        {
            splashSprite.Draw(time, new Rectangle(0, 0, GameConst.SCREEN_WIDTH, GameConst.SCREEN_HEIGHT));
            textSprite.Draw(time, new Rectangle(42, 42, 800, 200));
            coinSprite.Draw(time, new Rectangle(255, 74, 26, 30));

            if (model.Lives > 0)
            {
                ISprite text = SpriteFactory.Instance.CreateTextSprite("WORLD");
                text.Draw(time, new Rectangle(300, 400, 80, 80));
                marioSprite.Draw(time, new Rectangle(400, 500, 80, 80));
                ISprite Life = SpriteFactory.Instance.CreateTextSprite("*     " + model.Lives.ToString());
                Life.Draw(time, new Rectangle(450, 500, 80, 80));
            }
            else
            {
                gameOverSprite.Draw(time, new Rectangle(400, 500, 80, 80));
            }
        }
    }
}
