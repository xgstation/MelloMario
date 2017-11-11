using Microsoft.Xna.Framework;

namespace MelloMario.MiscObjects
{
    class HUD : IGameObject
    {
        private GameModel model;

        private ISprite textSprite;
        private int timeRemain; //in mileSeconds
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
            string firstLine = "MARIO           WORLD    TIME";
            string secondLine = model.Score.ToString().PadLeft(6, '0') + "    *"
                + model.Coins.ToString().PadLeft(2, '0') + "    "
                + "1-1" + "      " + timeRemain / 1000; // TODO: get world name from player.CurrentWorld
            textSprite = Factories.SpriteFactory.Instance.CreateTextSprite(firstLine + "\n" + secondLine);
        }

        public HUD(GameModel model, int startTime)
        {
            this.model = model;
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
            timeRemain -= time;
        }

        public void Draw(int time, Rectangle viewport, ZIndex zIndex)
        {
            textSprite.Draw(time, new Rectangle(42, 42, 800, 200), ZIndex.hud);
        }
    }
}
