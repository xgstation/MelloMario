using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.Theming;

namespace MelloMario.SplashObjects
{
    class HUD : BaseUIObject
    {
        private GameModel model;

        private string text;
        private ISprite textSprite;
        private ISprite coinSprite;
        private ISprite oneUpSprite;

        protected override void OnUpdate(int time)
        {
            string newText = "MARIO     *" + model.Lives.ToString().PadLeft(2, '0') + "   WORLD    TIME\n"
                + model.Score.ToString().PadLeft(6, '0') + "    *"
                + model.Coins.ToString().PadLeft(2, '0') + "    "
                + "1-1" + "      " + model.Time / 1000; // TODO: get world name from player.CurrentWorld

            if (newText != text)
            {
                text = newText;
                textSprite = SpriteFactory.Instance.CreateTextSprite(text);
            }
        }

        protected override void OnDraw(int time, Rectangle viewport)
        {
            textSprite.Draw(time, new Rectangle(42, 42, 800, 200));
            coinSprite.Draw(time, new Rectangle(255, 74, 26, 30));
            oneUpSprite.Draw(time, new Rectangle(255, 42, 26, 30));
        }

        public HUD(GameModel model)
        {
            this.model = model;
            textSprite = SpriteFactory.Instance.CreateTextSprite("");
            coinSprite = SpriteFactory.Instance.CreateCoinSprite(true);
            oneUpSprite = SpriteFactory.Instance.CreateOneUpMushroomSprite();
        }
    }
}
