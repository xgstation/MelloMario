namespace MelloMario.Graphics.UserInterfaces
{
    #region

    using MelloMario.Factories;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    internal class GameOver : BaseUserInterface
    {
        private readonly ISprite worldText;
        private readonly ISprite lifesText;
        private readonly ISprite marioIcon;
        private readonly ISprite gameOverText;
        private readonly ISprite gameOverRestartText;
        private readonly ISprite gameOverQuitText;
        private readonly ISprite backGround;

        private readonly Rectangle worldTextDestination;
        private readonly Rectangle lifesTextDestination;
        private readonly Rectangle marioIconDestination;
        private readonly Rectangle gameOverTextDestination;
        private readonly Rectangle gameOverRestartTextDestination;
        private readonly Rectangle gameOverQuitTextDestination;
        private readonly Rectangle backGroundDestination;

        private readonly int lifes;

        public GameOver(int lifes, string worldName)
        {
            this.lifes = lifes;

            worldText = SpriteFactory.Instance.CreateTextSprite("World   " + worldName);
            lifesText = SpriteFactory.Instance.CreateTextSprite("*  " + lifes);
            marioIcon = SpriteFactory.Instance.CreateMarioSprite("Standard", "Standing", "GameOver", "Right");
            gameOverText = SpriteFactory.Instance.CreateTextSprite("GAME    OVER");
            gameOverRestartText = SpriteFactory.Instance.CreateTextSprite("PRESS  Q  TO  QUIT");
            gameOverQuitText = SpriteFactory.Instance.CreateTextSprite("PRESS  R  TO  RESTART");
            backGround = SpriteFactory.Instance.CreateSplashSprite();

            worldTextDestination = new Rectangle(new Point(220, 200), new Point(80, 80));
            lifesTextDestination = new Rectangle(new Point(350, 250), new Point(80, 80));
            marioIconDestination = new Rectangle(new Point(250, 250), new Point(40, 40));
            gameOverTextDestination = new Rectangle(new Point(250, 250), new Point(80, 80));
            gameOverRestartTextDestination = new Rectangle(new Point(250,350), new Point(80, 80));
            gameOverQuitTextDestination = new Rectangle(new Point(250, 450), new Point(80, 80));
            backGroundDestination = new Rectangle(Point.Zero, new Point(Const.SCREEN_WIDTH, Const.SCREEN_HEIGHT));
        }

        protected override void OnUpdate(int time)
        {

        }

        protected override void OnDraw(int time, SpriteBatch spriteBatch)
        {
            backGround.Draw(time, spriteBatch, backGroundDestination);
            if (lifes > 0)
            {
                worldText.Draw(time, spriteBatch, worldTextDestination);
                lifesText.Draw(time, spriteBatch, lifesTextDestination);
                marioIcon.Draw(time, spriteBatch, marioIconDestination);
            }
            else
            {
                gameOverText.Draw(time, spriteBatch, gameOverTextDestination);
                gameOverRestartText.Draw(time, spriteBatch, gameOverRestartTextDestination);
                gameOverQuitText.Draw(time, spriteBatch, gameOverQuitTextDestination);
            }
        }
    }
}
