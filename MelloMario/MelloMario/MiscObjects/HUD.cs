using Microsoft.Xna.Framework;
using MelloMario.Factories;
using MelloMario.Theming;

namespace MelloMario.MiscObjects
{
    class HUD : IGameObject
    {
        private GameModel model;

        private ISprite textSprite;
        private ISprite coinSprite;
        private ISprite oneUpSprite;
        private int elapsed;

        public Rectangle Boundary
        {
            get
            {
                return new Rectangle(); // TODO
            }
        }

        private void UpdateSprite()
        {
            string firstLine = "MARIO     *" + model.Lives.ToString().PadLeft(2, '0') + "   WORLD    TIME";
            string secondLine = model.Score.ToString().PadLeft(6, '0') + "    *"
                + model.Coins.ToString().PadLeft(2, '0') + "    "
                + "1-1" + "      " + model.Time / 1000; // TODO: get world name from player.CurrentWorld
            textSprite = SpriteFactory.Instance.CreateTextSprite(firstLine + "\n" + secondLine);
        }

        public HUD(GameModel model)
        {
            this.model = model;
            coinSprite = SpriteFactory.Instance.CreateCoinSprite(true);
            oneUpSprite = SpriteFactory.Instance.CreateOneUpMushroomSprite();
            UpdateSprite();
        }

        public void Update(int time)
        {
            elapsed += time;
            if (elapsed > 50)
            {
                UpdateSprite();
                elapsed = 0;
            }

        }

        public void Draw(int time, Rectangle viewport)
        {
            textSprite.Draw(time, new Rectangle(42, 42, 800, 200));
            coinSprite.Draw(time, new Rectangle(255, 74, 26, 30));
            oneUpSprite.Draw(time, new Rectangle(255, 42, 26, 30));
        }
    }
}
