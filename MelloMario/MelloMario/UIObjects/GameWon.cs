using MelloMario.Factories;
using MelloMario.Theming;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.UIObjects
{
    internal class GameWon : BaseUIObject
    {
        private readonly ISprite splashSprite;
        private readonly ISprite textSprite;
        private readonly ISprite textSprite2;
        private Rectangle splashDestinationRect;
        private Rectangle textDestinationRect;
        private Rectangle text2DestinationRect;

        public GameWon(IPlayer player) : base(player)
        {
            splashSprite = SpriteFactory.Instance.CreateSplashSprite();
            string text = "MARIO        " + "   WORLD    TIME\n" + Player.Score.ToString().PadLeft(6, '0') + "    *" + Player.Coins.ToString().PadLeft(2, '0') + "    " + "1-1" + "      ";
            textSprite = SpriteFactory.Instance.CreateTextSprite(text);
            textSprite2 = SpriteFactory.Instance.CreateTextSprite("You won!\n\nPress R to restart\n\nPress Q to quit");

            splashDestinationRect = new Rectangle(0, 0, Const.SCREEN_WIDTH, Const.SCREEN_HEIGHT);
            textDestinationRect = new Rectangle(10, 10, 80, 80);
            text2DestinationRect = new Rectangle(200, 200, 200, 80);
        }

        protected override void OnUpdate(int time)
        {
            Offset(ref textDestinationRect);
            Offset(ref text2DestinationRect);
            Offset(ref splashDestinationRect);
            UpdateOrigin();
        }

        protected override void OnDraw(int time, SpriteBatch spriteBatch)
        {
            splashSprite.Draw(time, spriteBatch, splashDestinationRect);
            textSprite.Draw(time, spriteBatch, textDestinationRect);
            textSprite2.Draw(time, spriteBatch, text2DestinationRect);
        }
    }
}
