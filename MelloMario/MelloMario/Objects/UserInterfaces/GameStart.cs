namespace MelloMario.Objects.UserInterfaces
{
    #region

    using MelloMario.Factories;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    internal class GameStart : BaseUIObject
    {
        private readonly ISprite startSprite;
        private readonly ISprite textSprite;
        private readonly Rectangle startDestinationRect;
        private readonly Rectangle textDestinationRect;

        //Start splash has no relation with offset, do not use it!
        public GameStart(Point offset) : base(offset)
        {
            startSprite = SpriteFactory.Instance.CreateTitle(ZIndex.Hud);
            textSprite = SpriteFactory.Instance.CreateTextSprite(
                "Mellop\n" + "Press A to play normal mode.\n" + "Press B to play infinite mode.");
            startDestinationRect = new Rectangle(100, 100, 352, 176);
            textDestinationRect = new Rectangle(0, 400, 200, 200);
        }
        
        protected override void OnUpdate(int time)
        {
        }

        protected override void OnDraw(int time, SpriteBatch spriteBatch)
        {
            startSprite.Draw(time, spriteBatch, startDestinationRect);
            textSprite.Draw(time, spriteBatch, textDestinationRect);
        }
    }
}
