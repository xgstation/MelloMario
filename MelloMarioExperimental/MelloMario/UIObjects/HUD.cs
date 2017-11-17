using MelloMario.Factories;
using MelloMario.Theming;
using Microsoft.Xna.Framework;

namespace MelloMario.UIObjects
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
            string newText = "MARIO     *" + GameDatabase.Lifes.ToString().PadLeft(2, '0') + "   WORLD    TIME\n"
                + GameDatabase.Score.ToString().PadLeft(6, '0') + "    *"
                + GameDatabase.Coins.ToString().PadLeft(2, '0') + "    "
                + "1-1" + "      " + GameDatabase.TimeRemain / 1000; // TODO: get world name from player.CurrentWorld

            if (newText != text)
            {
                text = newText;
                textSprite = SpriteFactory.Instance.CreateTextSprite(text);
            }
        }

        protected override void OnDraw(int time)
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
