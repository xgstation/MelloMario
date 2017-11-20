using MelloMario.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.Objects.UserInterfaces
{
    internal class GameStart : BaseUIObject
    {
        private readonly ISprite startSprite;
        private readonly ISprite textSprite;
        private Rectangle startDestinationRect;
        private Rectangle textDestinationRect;

        public GameStart(IPlayer player) : base(player)
        {
            startSprite = SpriteFactory.Instance.CreateTitle(ZIndex.Hud);
            textSprite = SpriteFactory.Instance.CreateTextSprite("Mellop\n" + "Press A to play normal mode.\n" + "Press B to play infinite mode.");
            startDestinationRect = new Rectangle(100, 100, 352, 176);
            textDestinationRect = new Rectangle(0, 400, 200, 200);
        }

        protected override void OnUpdate(int time)
        {
            Offset(ref startDestinationRect);
            Offset(ref textDestinationRect);
            UpdateOrigin();
        }

        protected override void OnDraw(int time, SpriteBatch spriteBatch)
        {
            startSprite.Draw(time, spriteBatch, startDestinationRect);
            textSprite.Draw(time, spriteBatch, textDestinationRect);
        }
    }
}
