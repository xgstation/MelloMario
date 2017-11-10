using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MelloMario.Sprites
{
    class TextSprite : BaseSprite
    {
        private string text;
        private SpriteFont font;
        private Color color;

        protected override void OnDraw(int time, Rectangle destination)
        {
            spriteBatch.DrawString(font, text, destination.Location.ToVector2(), color);
        }

        public TextSprite(SpriteBatch spriteBatch, string text, SpriteFont font, Point size, ZIndex activeZIndex) : base(spriteBatch, size, activeZIndex)
        {
            this.text = text;
            this.font = font;
            color = Color.White;
        }
    }
}
