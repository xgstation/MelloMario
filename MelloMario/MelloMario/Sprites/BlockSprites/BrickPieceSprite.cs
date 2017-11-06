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

        public BrickPieceSprite(SpriteBatch spriteBatch, Texture2D texture)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;

            elapsed = 0;
        }

        public void Draw(GameTime time, Rectangle destination, ZIndex zIndex)
        {
            if (zIndex == ZIndex.foreground)
            {
                elapsed += time.ElapsedGameTime.Milliseconds;

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

                spriteBatch.Draw(texture, newDestination, source, Color.White);
            }
        }
    }
}
