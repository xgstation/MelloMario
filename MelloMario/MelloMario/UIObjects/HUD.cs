using MelloMario.Factories;
using MelloMario.Interfaces;
using MelloMario.Theming;
using Microsoft.Xna.Framework;

namespace MelloMario.UIObjects
{
    class HUD : BaseUIObject
    {
        private IGameCamera camera;
        private string text;
        private ISprite textSprite;
        private ISprite coinSprite;
        private ISprite oneUpSprite;

        protected override void OnUpdate(int time)
        {
            string newText = "MARIO     *" + Player.Lifes.ToString().PadLeft(2, '0') + "   WORLD    TIME\n"
                + Player.Score.ToString().PadLeft(6, '0') + "    *"
                + Player.Coins.ToString().PadLeft(2, '0') + "    "
                + "1-1" + "      " + Player.TimeRemain / 1000; // TODO: get World name from player.CurrentWorld

            if (newText != text)
            {
                text = newText;
                textSprite = SpriteFactory.Instance.CreateTextSprite(text);
            }
        }

        protected override void OnDraw(int time)
        {
            var textRec = new Rectangle(42, 42, 800, 200);
            var coinRec = new Rectangle(255, 74, 26, 30);
            var oneUpRec = new Rectangle(255, 42, 26, 30);
            textRec.Offset(camera.Location);
            coinRec.Offset(camera.Location);
            oneUpRec.Offset(camera.Location);
            textSprite.Draw(time, textRec);
            coinSprite.Draw(time, coinRec);
            oneUpSprite.Draw(time, oneUpRec);
        }

        public HUD(IPlayer player, IGameCamera camera) : base(player, camera)
        {
            this.camera = camera;
            textSprite = SpriteFactory.Instance.CreateTextSprite("");
            coinSprite = SpriteFactory.Instance.CreateCoinSprite(true);
            oneUpSprite = SpriteFactory.Instance.CreateOneUpMushroomSprite();
        }
    }
}
