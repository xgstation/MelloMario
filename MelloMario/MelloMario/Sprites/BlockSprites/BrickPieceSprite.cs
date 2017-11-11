using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.Sprites.BlockSprites
{
    class BrickPieceSprite : ISprite
    {
        private SpriteBatch spriteBatch;
        private Texture2D texture;

        private int elapsed;

        public Point PixelSize
        {
            get
            {
                return new Point(texture.Width, texture.Height);
            }
        }

        public ZIndex ZIndex
        {
            get
            {
                return ZIndex.level;
            }
        }
        protected float LayerDepth
        {
            get
            {
                switch (ZIndex)
                {
                    case ZIndex.hud:
                        return 0.05f;
                    case ZIndex.foreground:
                        return 0.1f;
                    case ZIndex.level:
                        return 0.25f;
                    case ZIndex.item:
                        return 0.4f;
                    case ZIndex.background3:
                        return 0.5f;
                    case ZIndex.background2:
                        return 0.6f;
                    case ZIndex.background1:
                        return 0.85f;
                    case ZIndex.background0:
                        return 0.95f;
                    default:
                        return 1f;
                }
            }
        }
        public BrickPieceSprite(SpriteBatch spriteBatch, Texture2D texture)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;

            elapsed = 0;
        }

        public void Draw(int time, Rectangle destination)
        {
            elapsed += time;

            int offsetX;
            int offsetY;
            Rectangle newDestination;
            Rectangle source;

            offsetX = -elapsed / 5 + 4;
            offsetY = elapsed * elapsed / 400 - elapsed + 16;
            newDestination = new Rectangle(destination.Left + offsetX, destination.Top + offsetY, destination.Width, destination.Height);
            source = new Rectangle(0, 0, texture.Width / 2, texture.Height / 2);
            spriteBatch.Draw(texture, newDestination, source, Color.White);

            offsetX = -elapsed / 5 + 4;
            offsetY = elapsed * elapsed / 360 - elapsed + 32;
            newDestination = new Rectangle(destination.Left + offsetX, destination.Top + offsetY, destination.Width, destination.Height);
            source = new Rectangle(0, texture.Height / 2, texture.Width / 2, texture.Height / 2);
            spriteBatch.Draw(texture, newDestination, source, Color.White);

            offsetX = elapsed / 5 + 12;
            offsetY = elapsed * elapsed / 400 - elapsed + 16;
            newDestination = new Rectangle(destination.Left + offsetX, destination.Top + offsetY, destination.Width, destination.Height);
            source = new Rectangle(texture.Width / 2, 0, texture.Width / 2, texture.Height / 2);
            spriteBatch.Draw(texture, newDestination, source, Color.White);

            offsetX = elapsed / 5 + 12;
            offsetY = elapsed * elapsed / 360 - elapsed + 32;
            newDestination = new Rectangle(destination.Left + offsetX, destination.Top + offsetY, destination.Width, destination.Height);
            source = new Rectangle(texture.Width / 2, texture.Height / 2, texture.Width / 2, texture.Height / 2);

            spriteBatch.Draw(texture, newDestination, source, Color.White,0,new Vector2(),SpriteEffects.None, LayerDepth );
        }
    }
}
