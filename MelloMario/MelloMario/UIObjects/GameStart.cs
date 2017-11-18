using MelloMario.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.UIObjects
{
    class GameStart : BaseUIObject
    {
        private ISprite textSprite;
        private ISprite startSprite;

        protected override void OnUpdate(int time)
        {

        }

        protected override void OnDraw(int time, SpriteBatch spriteBatch)
        {
            startSprite.Draw(time, spriteBatch, new Rectangle(100, 100, 352, 176));
            textSprite.Draw(time, spriteBatch, new Rectangle(0, 400, 200, 200));
        }

        public GameStart(IPlayer player) : base(player)
        {
            startSprite = SpriteFactory.Instance.CreateTitle(ZIndex.hud);
            textSprite = SpriteFactory.Instance.CreateTextSprite("Mellop");
        }
    }
}