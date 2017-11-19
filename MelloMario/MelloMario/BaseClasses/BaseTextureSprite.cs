using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.Sprites
{
    internal abstract class BaseTextureSprite : BaseSprite
    {
        private readonly Texture2D texture;
        private Color color;
        private Rectangle source;

        public BaseTextureSprite(Texture2D texture, Rectangle source, ZIndex zIndex) : base(source.Size, zIndex)
        {
            this.texture = texture;
            this.source = source;
            color = Color.White;
        }

        protected abstract void OnAnimate(int time);

        protected void ChangeSource(Point location)
        {
            source.Location = location;
        }

        protected void ChangeColor(Color newColor)
        {
            color = newColor;
        }

        protected override void OnDraw(int time, SpriteBatch spriteBatch, Rectangle destination)
        {
            OnAnimate(time);
            spriteBatch.Draw(texture, destination, source, color, 0f, //rotation
                new Vector2(), //origin
                SpriteEffects.None, LayerDepth);
        }
    }
}