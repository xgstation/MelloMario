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

        public GameWon(IPlayer player) : base(player)
        {
            splashSprite = SpriteFactory.Instance.CreateSplashSprite();
            string text = "MARIO        " + "   WORLD    TIME\n" + Player.Score.ToString().PadLeft(6, '0') + "    *" +
                          Player.Coins.ToString().PadLeft(2, '0') + "    " + "1-1" + "      ";
            textSprite = SpriteFactory.Instance.CreateTextSprite(text);
        }

        protected override void OnUpdate(int time) { }

        protected override void OnDraw(int time, SpriteBatch spriteBatch)
        {
            splashSprite.Draw(time, spriteBatch, new Rectangle(0, 0, GameConst.SCREEN_WIDTH, GameConst.SCREEN_HEIGHT));
            textSprite.Draw(time, spriteBatch, new Rectangle(10, 10, 80, 80));
            var textSprite2 = SpriteFactory.Instance.CreateTextSprite("You won!\n\nPress R to restart\n\nPress Q to quit");
            textSprite2.Draw(time, spriteBatch, new Rectangle(200, 200, 200, 80));
        }
    }
}