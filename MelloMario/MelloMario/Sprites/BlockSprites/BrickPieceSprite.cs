using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;

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
                return ZIndex.foreground;
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

            Vector2 origin = destination.Location.ToVector2();
            Vector2[] destinations = new Vector2[4];
            float[] offsetX = new float[4];
            float[] offsetY = new float[4];
            Rectangle[] source = new Rectangle[4];
            
            //LB
            offsetX[0] = -elapsed / 5f + 4f;
            offsetY[0] = elapsed * elapsed / 400f - elapsed + 16f;
            destinations[0] = new Vector2(offsetX[0], offsetY[0]) + origin;
            source[0] = new Rectangle(0, 0, texture.Width / 2, texture.Height / 2);

            //LT
            offsetX[1] = -elapsed / 5f + 4f;
            offsetY[1] = elapsed * elapsed / 360f - elapsed + 32f;
            destinations[1] = new Vector2(offsetX[1], offsetY[1]) + origin;
            source[1] = new Rectangle(0, texture.Height / 2, texture.Width / 2, texture.Height / 2);

            //RB
            offsetX[2] = elapsed / 5f + 12f;
            offsetY[2] = elapsed * elapsed / 400f - elapsed + 16f;
            destinations[2] = new Vector2(offsetX[2], offsetY[2]) + origin;
            source[2] = new Rectangle(texture.Width / 2, 0, texture.Width / 2, texture.Height / 2);

            //RT
            offsetX[3] = elapsed / 5f + 12f;
            offsetY[3] = elapsed * elapsed / 360f - elapsed + 32f;
            destinations[3] = new Vector2(offsetX[3], offsetY[3]) + origin;
            source[3] = new Rectangle(texture.Width / 2, texture.Height / 2, texture.Width / 2, texture.Height / 2);

            var spriteOrigin = new Vector2(8f, 8f);
            var scale = new Vector2(1.25f, 1.25f);
            var rotation = elapsed * 1.5f;
            spriteBatch.Draw(texture, destinations[0], source[0], Color.White, rotation, spriteOrigin, scale, SpriteEffects.None, LayerDepth);
            spriteBatch.Draw(texture, destinations[1], source[1], Color.White, rotation, spriteOrigin, scale, SpriteEffects.None, LayerDepth);
            spriteBatch.Draw(texture, destinations[2], source[2], Color.White, -rotation, spriteOrigin, scale, SpriteEffects.None, LayerDepth);
            spriteBatch.Draw(texture, destinations[3], source[3], Color.White, -rotation, spriteOrigin, scale, SpriteEffects.None, LayerDepth);
        }
    }
}
