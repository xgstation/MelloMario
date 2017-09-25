using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.Sprites
{
   public class GreenKoopaSprite : ISprite
    {

        public Texture2D greenKoopa { get; set; }
        public int greenRows { get; set; }
        public int greenColumns { get; set; }
        private int greenFrames;
        private int totalGreenFrames;
        private float elapsed;
        private float delay = 250f;
        Boolean greenJumpOn;
        Boolean dead;
        Vector2 pos;

        public GreenKoopaSprite(Texture2D green, int greenInputRows, int greenInputColumns, Boolean greenJump, Boolean defeated)
        {
            greenKoopa = green;

            greenRows = greenInputRows;
            greenColumns = greenInputColumns;

            greenFrames = 0;

            totalGreenFrames = greenRows * greenColumns;
            greenJumpOn = greenJump;
            dead = defeated;
            if (greenJumpOn || dead)
            {
                greenFrames = totalGreenFrames - 2;
            }



        }
        public void Update(GameTime gameTime)
        {
            if (!greenJumpOn && !dead)
            {
                totalGreenFrames = 4;
                elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (elapsed >= delay)
                {
                    greenFrames++;
                    if (greenFrames == totalGreenFrames)
                    {
                        greenFrames = 0;
                    }
                    elapsed = 0;
                }
            }

            else
            {

                elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (elapsed >= delay)
                {

                    greenFrames++;
                    if (greenFrames == totalGreenFrames)
                    {
                        if (greenJumpOn)
                        {
                            greenFrames = totalGreenFrames - 1;
                        }
                        else if (dead)
                        {
                            greenFrames = totalGreenFrames;
                        }
                    }
                    elapsed = 0;
                }
            }

        }
        public void Draw(SpriteBatch enemySprite, Vector2 location)
        {
            pos = location;
            int greenWidth = greenKoopa.Width / greenColumns;
            int greenHeight = greenKoopa.Height / greenRows;

            int gR = greenFrames / greenColumns;
            int gC = greenFrames % greenColumns;

            Rectangle greenLast = new Rectangle((int)(pos.X), (int)pos.Y, greenWidth, greenHeight);

            Rectangle greenFirst = new Rectangle(greenWidth * gC, gR * greenHeight, greenWidth, greenHeight);
            


            enemySprite.Draw(greenKoopa, greenLast, greenFirst, Color.White);




        }
    }
}
