namespace MelloMario.UserInterfaces
{
    #region

    using MelloMario.Factories;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    internal class HUD : BaseUserInterface
    {
        private readonly ISprite coinSprite;
        private readonly ISprite oneUpSprite;
        private ISprite textSprite;

        private readonly Rectangle coinDestination;
        private readonly Rectangle oneUpDestination;
        private readonly Rectangle textDestination;
        private string text;

        private int lifes;
        private int score;
        private int coins;
        private int timeRemain;
        private string worldName;

        public HUD()
        {
            textSprite = SpriteFactory.Instance.CreateTextSprite("");
            coinSprite = SpriteFactory.Instance.CreateCoinSprite(true);
            oneUpSprite = SpriteFactory.Instance.CreateOneUpMushroomSprite();
            textDestination = new Rectangle(42, 42, 800, 200);
            coinDestination = new Rectangle(255, 74, 26, 30);
            oneUpDestination = new Rectangle(255, 42, 26, 30);
        }

        public bool IsSplashing { get; set; }

        public void OnHUDInfoChange(int newLifes, int newScore, int newCoins, int newTimeRemain, string newWorldName)
        {
            lifes = newLifes;
            score = newScore;
            coins = newCoins;
            timeRemain = newTimeRemain;
            worldName = newWorldName;
        }

        protected override void OnUpdate(int time)
        {
            string newText = "MARIO     *"
                + lifes.ToString().PadLeft(2, '0')
                + "   WORLD    TIME\n"
                + score.ToString().PadLeft(6, '0')
                + "    *"
                + coins.ToString().PadLeft(2, '0')
                + "    "
                + worldName
                + "      "
                + (IsSplashing ? "" : (timeRemain / 1000).ToString());

            if (newText == text)
            {
                return;
            }
            text = newText;
            textSprite = SpriteFactory.Instance.CreateTextSprite(text);
        }

        protected override void OnDraw(int time, SpriteBatch spriteBatch)
        {
            textSprite.Draw(time, spriteBatch, textDestination);
            coinSprite.Draw(time, spriteBatch, coinDestination);
            oneUpSprite.Draw(time, spriteBatch, oneUpDestination);
        }
    }
}
