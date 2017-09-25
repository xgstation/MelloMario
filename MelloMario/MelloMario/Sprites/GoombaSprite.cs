using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MelloMario.Sprites
{
    public class GoombaSprite : ISprite
    {
        public Texture2D goomBa { get; set; }
        public int rows { get; set; }
        public int columns { get; set; }
        private int frames;
        private int totalFrames;
        private float elapsed;
        private float delay = 1000f;
        private Vector2 pos;
        private Boolean dead;
       

        public GoombaSprite(Texture2D pic, int inputRows, int inputColumns, Boolean defeated)
        {
            goomBa = pic;
            rows = inputRows;
            columns = inputColumns;
            frames = 0;
            totalFrames = rows * columns;

            dead = defeated;
            if (dead)
            {
                frames = totalFrames - 1;
            }
           
        }



        public void Update(GameTime gameTime)
        {
            if (!dead)
            {
                totalFrames = 2;
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
            //else if(elapsedFromDeath<500)
            // {
            // elapsedFromDeath += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            //frames = totalFrames-2;
            // frames++;

            //}
            else
            {
                elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (elapsed >= delay)
                {

                    frames++;
                    if (frames == totalFrames)
                    {
                        frames = totalFrames;
                    }
                    elapsed = 0;
                }
            }
        }

        public void Draw(SpriteBatch enemySprite, Vector2 location)
        {
            pos = location;
            int width = goomBa.Width / columns;
            int height = goomBa.Height / rows;
            int r = frames / columns;
            int c = frames % columns;
            Rectangle last = new Rectangle((int)pos.X, (int)pos.Y, width, height);
            Rectangle first = new Rectangle(width * c, r * height, width, height);
           
                enemySprite.Draw(goomBa, last, first, Color.White);
         
   

        }
    }
}
