using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.Theming;

namespace MelloMario.SplashObjects
{
    class HUD : IGameObject
    {
        private GameModel model;

        private string text;
        private ISprite textSprite;
        private ISprite coinSprite;
        private ISprite oneUpSprite;
        private ISprite startSprite;

        public Rectangle Boundary
        {
            get
            {
                return new Rectangle(); // TODO
            }
        }

        private void UpdateSprite()
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

        public HUD(GameModel model)
        {
            this.model = model;
            coinSprite = SpriteFactory.Instance.CreateCoinSprite(true);
            oneUpSprite = SpriteFactory.Instance.CreateOneUpMushroomSprite();
            startSprite = SpriteFactory.Instance.CreateTitle(MelloMario.ZIndex.hud);
            UpdateSprite();
        }

        public void Update(int time)
        {
            UpdateSprite();
        }

        public void Draw(int time, Rectangle viewport)
        {
            textSprite.Draw(time, new Rectangle(42, 42, 800, 200));
            coinSprite.Draw(time, new Rectangle(255, 74, 26, 30));
            oneUpSprite.Draw(time, new Rectangle(255, 42, 26, 30));
            startSprite.Draw(time, new Rectangle(100,100,100,100));

        }
    }
}
