using MelloMario.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.UIObjects
{
    internal class GameStart : BaseUIObject
    {
        private readonly ISprite startSprite;
        private readonly ISprite textSprite;

        public GameStart(IPlayer player) : base(player)
        {
            startSprite = SpriteFactory.Instance.CreateTitle(ZIndex.Hud);
            textSprite = SpriteFactory.Instance.CreateTextSprite("Mellop");
        }

        protected override void OnUpdate(int time) { }

        protected override void OnDraw(int time, SpriteBatch spriteBatch)
        {
            startSprite.Draw(time, spriteBatch, new Rectangle(100, 100, 352, 176));
            textSprite.Draw(time, spriteBatch, new Rectangle(0, 400, 200, 200));
        }
    }
}