using MelloMario.Factories;
using MelloMario.Theming;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.UIObjects
{
    class GameOver : BaseUIObject
    {
        private readonly ISprite splashSprite;
        private readonly ISprite textSprite;
        private readonly ISprite coinSprite;
        private readonly ISprite marioSprite;
        private readonly ISprite gameOverSprite;
        private string Text;

        protected override void OnUpdate(int time)
        {
            //UpdateSprite();
        }

        protected override void OnDraw(int time, SpriteBatch spriteBatch)
        {
            splashSprite.Draw(time, spriteBatch, new Rectangle(0, 0, GameConst.SCREEN_WIDTH, GameConst.SCREEN_HEIGHT));
            textSprite.Draw(time, spriteBatch, new Rectangle(42, 42, 800, 200));
            coinSprite.Draw(time, spriteBatch, new Rectangle(255, 74, 26, 30));

            if (Player.Lifes > 0)
            {
                ISprite text = SpriteFactory.Instance.CreateTextSprite("WORLD");
                text.Draw(time, spriteBatch, new Rectangle(300, 200, 80, 80));
                marioSprite.Draw(time, spriteBatch, new Rectangle(250, 250, 40, 40));
                ISprite Life = SpriteFactory.Instance.CreateTextSprite("*  " + Player.Lifes);
                Life.Draw(time, spriteBatch, new Rectangle(350, 250, 80, 80));
            }
            else
            {
                gameOverSprite.Draw(time, spriteBatch, new Rectangle(400, 250, 80, 80));
            }
        }

        public GameOver(IPlayer player) : base(player)
        {
            splashSprite = SpriteFactory.Instance.CreateSplashSprite();
            coinSprite = SpriteFactory.Instance.CreateCoinSprite(true);
            marioSprite = SpriteFactory.Instance.CreateMarioSprite("Standard", "Standing", "GameOver", "Right");
            gameOverSprite = SpriteFactory.Instance.CreateTextSprite("GAME    OVER");
            Text = "MARIO        " + "   WORLD    TIME\n"
                + Player.Score.ToString().PadLeft(6, '0') + "    *"
                + Player.Coins.ToString().PadLeft(2, '0') + "    "
                + "1-1" + "      ";
            textSprite = SpriteFactory.Instance.CreateTextSprite(Text);
        }
    }
}
