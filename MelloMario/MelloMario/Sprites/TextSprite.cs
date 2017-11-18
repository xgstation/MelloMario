using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MelloMario.Sprites
{
    class TextSprite : BaseSprite
    {
        private string text;
        private float fontSize;
        private SpriteFont font;
        private Color color;

        protected override void OnDraw(int time, SpriteBatch spriteBatch, Rectangle destination)
        {
            spriteBatch.DrawString(
                font,
                text,
                destination.Location.ToVector2(),
                color,
                0f,//rotation
                new Vector2(), //origin
                fontSize / 18f, //scale
                SpriteEffects.None,
                LayerDepth
            );
        }

        public TextSprite(string text, SpriteFont font, Point size, ZIndex zIndex, float fontSize = 18f) : base(size, zIndex)
        {
            this.text = text;
            this.font = font;
            this.fontSize = fontSize;
            color = Color.White;
        }
    }
}
