namespace MelloMario.Objects.UserInterfaces
{
    #region

    using System;
    using System.Collections.Generic;
    using MelloMario.Factories;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    internal class GameOver : BaseUIObject
    {


        private readonly ISprite lifesText;
        private readonly ISprite marioIcon;
        private readonly ISprite gameOverText;
        private readonly ISprite backGround;

        private readonly Rectangle lifesTextDestination;
        private readonly Rectangle marioIconDestination;
        private readonly Rectangle gameOverTextDestination;
        private readonly Rectangle backGroundDestination;

        private readonly int lifes;
        public GameOver(int lifes)
        {
            this.lifes = lifes;

            lifesText = SpriteFactory.Instance.CreateTextSprite("*  " + lifes);
            marioIcon = SpriteFactory.Instance.CreateMarioSprite("Standard", "Standing", "GameOver", "Right");
            gameOverText = SpriteFactory.Instance.CreateTextSprite("GAME    OVER");
            backGround = SpriteFactory.Instance.CreateSplashSprite();

            lifesTextDestination = new Rectangle(new Point(350, 250), new Point(80, 80));
            marioIconDestination = new Rectangle(new Point(250, 250), new Point(40, 40));
            gameOverTextDestination = new Rectangle(new Point(250, 250), new Point(80, 80));
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
                lifesText.Draw(time,spriteBatch,lifesTextDestination);
                marioIcon.Draw(time,spriteBatch,marioIconDestination);
            }
            else
            {
                gameOverText.Draw(time, spriteBatch, gameOverTextDestination);
            }
        }
    }
}
