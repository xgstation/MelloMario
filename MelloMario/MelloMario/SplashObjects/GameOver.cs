using MelloMario.Factories;
using MelloMario.Theming;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.SplashObjects
{
    class GameOver
    {
        private GameModel model;

        public GameOver(GameModel model)
        {
            this.model = model;
        }

        public void Draw(int time)
        {
            ISprite Splash = SpriteFactory.Instance.CreatSplashSprite();
            Splash.Draw(time, new Rectangle(0, 0, GameConst.SCREEN_WIDTH, GameConst.SCREEN_HEIGHT));
            ISprite Mario = SpriteFactory.Instance.CreateTextSprite("MARIO");
            Mario.Draw(time, new Rectangle(30, 20, 50, 50));
            ISprite World = SpriteFactory.Instance.CreateTextSprite("WORLD");
            World.Draw(time, new Rectangle(30 + 2 * (GameConst.SCREEN_WIDTH / 4), 20, 50, 50));
            ISprite Time = SpriteFactory.Instance.CreateTextSprite("Time");
            Time.Draw(time, new Rectangle(30 + 3 * (GameConst.SCREEN_WIDTH / 4), 20, 50, 50));
            ISprite S = SpriteFactory.Instance.CreateTextSprite(model.Score.ToString());
            S.Draw(time, new Rectangle(30, 100, 50, 50));
            ISprite Coin = SpriteFactory.Instance.CreateCoinSprite(true);
            Coin.Draw(time, new Rectangle(30 + GameConst.SCREEN_WIDTH / 4, 100, 50, 50));
            ISprite Ctext = SpriteFactory.Instance.CreateTextSprite("*" + Coin.ToString());
            Ctext.Draw(time, new Rectangle(80 + GameConst.SCREEN_WIDTH / 4, 100, 50, 50));
            if (model.Lives > 0)
            {
                ISprite text = SpriteFactory.Instance.CreateTextSprite("WORLD");
                text.Draw(time, new Rectangle(300, 400, 80, 80));
                ISprite marioSprite = SpriteFactory.Instance.CreateMarioSprite("Normal", "Standing", "Normal", "Right");
                marioSprite.Draw(time, new Rectangle(400, 500, 80, 80));
                ISprite Life = SpriteFactory.Instance.CreateTextSprite("*     " + model.Lives.ToString());
                Life.Draw(time, new Rectangle(450, 500, 80, 80));

            }
            else
            {
                ISprite text = SpriteFactory.Instance.CreateTextSprite("GAME    OVER");
                text.Draw(time, new Rectangle(400, 500, 80, 80));
            }
        }
    }
}
