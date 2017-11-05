using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//Work in progress
namespace MelloMario.Sprites
{
    class CompressedSprite : BaseSprite
    {
        private SpriteBatch spriteBatch;
        private ZIndex activeZIndex;
        private Texture2D mergedTexture;
        private Texture2D cellTexture;
        private Point fullSize;
        private RenderTarget2D renderTarget;


        public CompressedSprite(SpriteBatch spriteBatch, Texture2D texture, Point source, Point fullSize, ZIndex activeZIndex, string type) : base(spriteBatch, texture, source, fullSize, activeZIndex)
        {
            this.spriteBatch = spriteBatch;
            this.activeZIndex = activeZIndex;
            var graphicDevice = texture.GraphicsDevice;
            var cellLoc = new Point(0, 0);
            var cellSize = new Point(32, 32);
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
            var sourceRectangle = new Rectangle(cellLoc, cellSize);
            cellTexture = new Texture2D(graphicDevice, sourceRectangle.Width, sourceRectangle.Height);
            var data = new Color[sourceRectangle.Width * sourceRectangle.Height];
            texture.GetData(0, sourceRectangle, data, 0, data.Length);
            cellTexture.SetData(data);
            renderTarget = new RenderTarget2D(graphicDevice,fullSize.X,fullSize.Y);
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
            //Stream s = File.Create("D:\\l.png");
            //renderTarget.SaveAsPng(s,renderTarget.Width,renderTarget.Height);
            //cellTexture.SaveAsPng(s,cellTexture.Width,cellTexture.Height);

        }

        protected override void OnAnimate(GameTime time)
        {

        }

        public override void Draw(GameTime time, Rectangle destination, ZIndex zIndex)
        {
            if (activeZIndex == zIndex)
            {
                OnAnimate(time);
                spriteBatch.Draw(renderTarget, destination,Color.White);
            }
        }
    }
}
