using MelloMario.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.UIObjects
{
    internal class HUD : BaseUIObject
    {
        private readonly ISprite coinSprite;
        private readonly ISprite oneUpSprite;
        private Rectangle coinDestinationRect;
        private Rectangle oneUpDestinationRect;
        private Rectangle textDestinationRect;
        private string text;
        private ISprite textSprite;

        public HUD(IPlayer player) : base(player)
        {
            textSprite = SpriteFactory.Instance.CreateTextSprite("");
            coinSprite = SpriteFactory.Instance.CreateCoinSprite(true);
            oneUpSprite = SpriteFactory.Instance.CreateOneUpMushroomSprite();
            textDestinationRect = new Rectangle(42, 42, 800, 200);
            coinDestinationRect = new Rectangle(255, 74, 26, 30);
            oneUpDestinationRect = new Rectangle(255, 42, 26, 30);
            OnUpdate(0);
        }

        protected override void OnUpdate(int time)
        {
            Offset(ref coinDestinationRect);
            Offset(ref textDestinationRect);
            Offset(ref oneUpDestinationRect);
            UpdateOrigin();

            string newText = "MARIO     *" + Player.Lifes.ToString().PadLeft(2, '0') + "   WORLD    TIME\n" + Player.Score.ToString().PadLeft(6, '0') + "    *" + Player.Coins.ToString().PadLeft(2, '0') + "    " + "1-1" + "      " + Player.TimeRemain / 1000; // TODO: get World name from player.CurrentWorld

            if (newText != text)
            {
                text = newText;
                textSprite = SpriteFactory.Instance.CreateTextSprite(text);
            }
        }

        protected override void OnDraw(int time, SpriteBatch spriteBatch)
        {
            textSprite.Draw(time, spriteBatch, textDestinationRect);
            coinSprite.Draw(time, spriteBatch, coinDestinationRect);
            oneUpSprite.Draw(time, spriteBatch, oneUpDestinationRect);
        }
    }
}
