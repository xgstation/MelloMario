using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Work in progress
namespace MelloMario.Sprites
{
    class CompressedSprite : ISprite
    {
        private SpriteBatch spriteBatch;
        private ZIndex activeZIndex;
        private Texture2D cellTexture;
        private Point fullSize;
        private RenderTarget2D renderTarget;

        public Point PixelSize
        {
            get
            {
                return fullSize;
            }
        }

        public ZIndex ZIndex
        {
            get
            {
                return activeZIndex;
            }
        }

        public CompressedSprite(SpriteBatch spriteBatch, Texture2D texture, Point source, Point fullSize, ZIndex activeZIndex, string type)
        {
            this.spriteBatch = spriteBatch;
            this.activeZIndex = activeZIndex;
            this.fullSize = fullSize;

            GraphicsDevice graphicDevice = texture.GraphicsDevice;
            Point cellLoc = new Point(0, 0);
            Point cellSize = new Point(32, 32);

            switch (type)
            {
                case "Floor":
                    break;
                case "Stair":
                    cellLoc = new Point(0, 32);
                    break;
                case "Brick":
                    cellLoc = new Point(32, 0);
                    break;
                default:
                    //DO NOTHING
                    break;
            }

            Rectangle sourceRectangle = new Rectangle(cellLoc, cellSize);
            cellTexture = new Texture2D(graphicDevice, sourceRectangle.Width, sourceRectangle.Height);

            Color[] data = new Color[sourceRectangle.Width * sourceRectangle.Height];
            texture.GetData(0, sourceRectangle, data, 0, data.Length);
            cellTexture.SetData(data);

            renderTarget = new RenderTarget2D(graphicDevice, fullSize.X, fullSize.Y);
            graphicDevice.SetRenderTarget(renderTarget);
            spriteBatch.Begin();
            for (int x = 0; x < fullSize.X / cellSize.X; x++)
            {
                for (int y = 0; y < fullSize.Y / cellSize.Y; y++)
                {
                    spriteBatch.Draw(cellTexture, new Vector2(x * cellTexture.Width, y * cellTexture.Height), Color.White);
                }
            }
            spriteBatch.End();
            graphicDevice.SetRenderTarget(null);
        }

        public void Draw(int time, Rectangle destination)
        {
            spriteBatch.Draw(renderTarget, destination, Color.White);
        }
    }
}
