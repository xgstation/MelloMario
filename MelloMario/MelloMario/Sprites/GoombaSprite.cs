using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Sprite
{
    public class GoombaSprite:ISprite
    {
         public Texture2D goomBa { get; set; }
        public int rows { get; set; }
        public int columns { get; set; }
        private int frames;
        private int totalFrames;
        private float elapsed;
        private float delay = 1000f;
 
        public GoombaSprite(Texture2D pic, int inputRows, int inputColumns)
        {
            goomBa = pic;
            rows = inputRows;
            columns = inputColumns;
            frames = 0;
            totalFrames = rows * columns;

        }

     

        public void Update(GameTime gameTime)
        {
            
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsed >= delay)
            {
                frames++;
                if (frames == totalFrames)
                {
                    frames = 0;
                }
                elapsed = 0;
            }
        
        }

        public void Draw(SpriteBatch enemySprite)
        {
            int width = goomBa.Width / columns;
            int height = goomBa.Height / rows;
            int r = frames / columns;
            int c = frames % columns;
            Rectangle last = new Rectangle(700, 100, width, height);
            Rectangle first = new Rectangle(width * c, r * height, width, height);
            enemySprite.Begin();
           
                enemySprite.Draw(goomBa, last, first, Color.White);
            
            enemySprite.End();
        }
    }
}
