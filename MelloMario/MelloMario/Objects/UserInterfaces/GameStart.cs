namespace MelloMario.Objects.UserInterfaces
{
    #region

    using System;
    using System.Diagnostics.Contracts;
    using System.Text;
    using MelloMario.Factories;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    internal class GameStart : BaseUIObject
    {
        private readonly Game1 game;
        private readonly ISprite startSprite;
        private readonly ISprite titleSprite;
        private ISprite menuSprite;
        private readonly Rectangle startDestinationRect;
        private readonly Rectangle titleDestinationRect;
        private readonly Rectangle menuDestinationRect;
        private readonly string[] menuStrings;
        
        public GameStart(Game1 game)
        {
            this.game = game;
            startSprite = SpriteFactory.Instance.CreateTitle(ZIndex.Hud);
            titleSprite = SpriteFactory.Instance.CreateTextSprite(
                "Mellop\n");
            startDestinationRect = new Rectangle(100, 100, 352, 176);
            titleDestinationRect = new Rectangle(0, 400, 200, 200);

            menuStrings = new[]
            {
                "Normal Mode",
                "Infinite Mode",
                "Quit"
            };
            menuSprite = SpriteFactory.Instance.CreateTextSprite(AddCursor(0));
            menuDestinationRect = new Rectangle(0, 450, 200, 200);
        }
        
        protected override void OnUpdate(int time)
        {
            menuSprite = SpriteFactory.Instance.CreateTextSprite(AddCursor((int)game.CurrentSelected));
        }

        protected override void OnDraw(int time, SpriteBatch spriteBatch)
        {
            startSprite.Draw(time, spriteBatch, startDestinationRect);
            titleSprite.Draw(time, spriteBatch, titleDestinationRect);
            menuSprite.Draw(time,spriteBatch,menuDestinationRect);
        }

        private string AddCursor(int index)
        {
            Contract.Assert(index >=0 && index < menuStrings.Length);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < menuStrings.Length; i++)
            {
                if (i == index)
                {
                    sb.Append(">");
                }
                sb.Append(menuStrings[i]);
                if (i == index)
                {
                    sb.Append("<");
                }
                if (i != menuStrings.Length - 1)
                {
                    sb.Append("\n");
                }
            }
            return sb.ToString();
        }
    }
}
