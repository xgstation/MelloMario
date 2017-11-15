using MelloMario.Factories;
using Microsoft.Xna.Framework;

namespace MelloMario.UIObjects
{
    class GameStart : BaseUIObject
    {
        private ISprite textSprite;
        private ISprite startSprite;

        protected override void OnUpdate(int time)
        {

        }

        protected override void OnDraw(int time, Rectangle viewport)
        {
            startSprite.Draw(time, new Rectangle(100, 100, 352, 176));
            textSprite.Draw(time, new Rectangle(0, 400, 200, 200));
        }

        public GameStart()
        {
            startSprite = SpriteFactory.Instance.CreateTitle(MelloMario.ZIndex.hud);
            textSprite = SpriteFactory.Instance.CreateTextSprite("Mellop");
        }
    }
}