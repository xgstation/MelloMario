using MelloMario.Factories;
using MelloMario.Theming;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.Sounds;

namespace MelloMario.UIObjects
{
    internal class GameOver : BaseUIObject
    {
        private readonly ISprite coinSprite;
        private readonly ISprite gameOverSprite;
        private readonly ISprite marioSprite;
        private readonly ISprite splashSprite;
        private readonly ISprite textSprite;
        private readonly ISprite textSprite2;
        private ISprite lifeSprite;
        private Rectangle coinDestinationRect;
        private Rectangle gameOverDestinationRect;
        private Rectangle marioDestinationRect;
        private Rectangle splashDestinationRect;
        private Rectangle textDestinationRect;
        private Rectangle text2DestinationRect;
        private Rectangle lifeDestinationRect;

        public GameOver(IPlayer player) : base(player)
        {
            splashSprite = SpriteFactory.Instance.CreateSplashSprite();
            coinSprite = SpriteFactory.Instance.CreateCoinSprite(true);
            marioSprite = SpriteFactory.Instance.CreateMarioSprite("Standard", "Standing", "GameOver", "Right");
            gameOverSprite = SpriteFactory.Instance.CreateTextSprite("GAME    OVER");
            string text = "MARIO        " + "   WORLD    TIME\n" + Player.Score.ToString().PadLeft(6, '0') + "    *" + Player.Coins.ToString().PadLeft(2, '0') + "    " + "1-1" + "      ";
            textSprite = SpriteFactory.Instance.CreateTextSprite(text);
            textSprite2 = SpriteFactory.Instance.CreateTextSprite("WORLD");

            RelativeOrigin = Vector2.Zero;
            coinDestinationRect = new Rectangle(255, 74, 26, 30);
            gameOverDestinationRect = new Rectangle(250, 250, 80, 80);
            marioDestinationRect = new Rectangle(250, 250, 40, 40);
            splashDestinationRect = new Rectangle(0, 0, GameConst.SCREEN_WIDTH, GameConst.SCREEN_HEIGHT);
            textDestinationRect = new Rectangle(42, 42, 800, 200);
            text2DestinationRect = new Rectangle(300, 200, 80, 80);
            lifeDestinationRect = new Rectangle(350, 250, 80, 80);
            OnUpdate(0);
        }

        protected override void OnUpdate(int time)
        {
            Offset(ref textDestinationRect);
            Offset(ref text2DestinationRect);
            Offset(ref gameOverDestinationRect);
            Offset(ref splashDestinationRect);
            Offset(ref coinDestinationRect);
            Offset(ref lifeDestinationRect);
            Offset(ref marioDestinationRect);
            UpdateOrigin();
        }

        protected override void OnDraw(int time, SpriteBatch spriteBatch)
        {
            splashSprite.Draw(time, spriteBatch, splashDestinationRect);
            textSprite.Draw(time, spriteBatch, textDestinationRect);
            coinSprite.Draw(time, spriteBatch, coinDestinationRect);

            if (Player.Lifes > 0)
            {
                textSprite2.Draw(time, spriteBatch, text2DestinationRect);
                marioSprite.Draw(time, spriteBatch, marioDestinationRect);
                lifeSprite = SpriteFactory.Instance.CreateTextSprite("*  " + Player.Lifes);
                lifeSprite.Draw(time, spriteBatch, lifeDestinationRect);
            }
            else
            {
                gameOverSprite.Draw(time, spriteBatch, gameOverDestinationRect);
                SoundController.GameOver.Play();
            }
        }
    }
}
