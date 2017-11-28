namespace MelloMario.Objects.UserInterfaces
{
    #region

    using MelloMario.Factories;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    internal class GameWon : BaseUIObject
    {
        private readonly ISprite splashSprite;
        private readonly ISprite textSprite;
        private readonly Rectangle splashDestinationRect;
        private readonly Rectangle textDestinationRect;

        public GameWon()
        {
            splashSprite = SpriteFactory.Instance.CreateSplashSprite();
            textSprite = SpriteFactory.Instance.CreateTextSprite("You won!\n\nPress R to restart\n\nPress Q to quit");

            splashDestinationRect = new Rectangle(Point.Zero, new Point(Const.SCREEN_WIDTH, Const.SCREEN_HEIGHT));
            textDestinationRect = new Rectangle(new Point(200, 200), new Point(200, 80));
        }

        protected override void OnUpdate(int time)
        {
            
        }

        protected override void OnDraw(int time, SpriteBatch spriteBatch)
        {
            splashSprite.Draw(time, spriteBatch, splashDestinationRect);
            textSprite.Draw(time, spriteBatch, textDestinationRect);
        }
    }
}
