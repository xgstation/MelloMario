using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MelloMario.Sprites
{
    abstract class BaseTextureSprite : BaseSprite
    {
        private Texture2D texture;
        private Rectangle source;
        private Color color;

        protected abstract void OnAnimate(int time);

        protected void ChangeSource(Point location)
        {
            source.Location = location;
        }

        protected void ChangeColor(Color newColor)
        {
            color = newColor;
        }

        protected override void OnDraw(int time, Rectangle destination)
        {
            OnAnimate(time);
            spriteBatch.Draw(
                texture,
                destination,
                source,
                color,
                0f, //rotation
                new Vector2(), //origin
                SpriteEffects.None,
                LayerDepth
            );
        }

        public BaseTextureSprite(SpriteBatch spriteBatch, Texture2D texture, Rectangle source, ZIndex zIndex) : base(spriteBatch, source.Size, zIndex)
        {
            this.texture = texture;
            this.source = source;
            color = Color.White;
        }
    }
}
