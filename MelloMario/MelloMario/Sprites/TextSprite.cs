using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.Sprites
{
    internal class TextSprite : BaseSprite
    {
        private readonly Color color;
        private readonly SpriteFont font;
        private readonly float fontSize;
        private readonly string text;

        public TextSprite(string text, SpriteFont font, Point size, ZIndex zIndex, float fontSize = 18f) : base(size, zIndex)
        {
            this.text = text;
            this.font = font;
            this.fontSize = fontSize;
            color = Color.White;
        }

        protected override void OnDraw(int time, SpriteBatch spriteBatch, Rectangle destination)
        {
            spriteBatch.DrawString(font, text, destination.Location.ToVector2(), color, 0f, //rotation
                new Vector2(), //origin
                fontSize / 18f, //scale
                SpriteEffects.None, LayerDepth);
        }
    }
}
