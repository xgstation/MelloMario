namespace MelloMario.Graphics.UserInterfaces
{
    #region

    using System.Text;
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
            StringBuilder sb = new StringBuilder();
            sb.Append("Mario".PadRight(10,' '));
            sb.Append('*');
            sb.Append(lifes.ToString().PadLeft(2, '0'));
            sb.Append("WORLD".PadLeft(8, ' '));
            sb.Append("TIME".PadLeft(8, ' '));
            sb.Append("\n");
            sb.Append(score.ToString().PadLeft(6, '0'));
            sb.Append("*".PadLeft(5, ' '));
            sb.Append(coins.ToString().PadLeft(2, '0').PadRight(6, ' '));
            sb.Append(worldName.PadRight(9, ' '));
            sb.Append(IsSplashing ? "" : (timeRemain / 1000).ToString());
            if (sb.ToString() == text)
            {
                return;
            }
            text = sb.ToString();
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
