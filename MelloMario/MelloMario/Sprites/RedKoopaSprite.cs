using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.Sprites
{
    public class RedKoopaSprite : ISprite
    {
        public Texture2D redKoopa { get; set; }
        public int redRows { get; set; }
        public int redColumns { get; set; }
        private int redFrames;
        private int totalRedFrames;
        private float elapsed;
        private float delay = 250f;
        private Boolean redJumpedOn;

        public RedKoopaSprite(Texture2D red, int redInputRows, int redInputColumns,Boolean redDefeated)
        {

            redKoopa = red;
            redRows = redInputRows;
            redColumns = redInputColumns;
            redFrames = 0;
            totalRedFrames = redRows * redColumns;
            redJumpedOn=redDefeated;
        }
        public void Update(GameTime gameTime)
        {
            if (!redJumpedOn)
            {
                totalRedFrames = 8;
                elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (elapsed >= delay)
                {
                    redFrames++;
                    if (redFrames == totalRedFrames)
                    {
                        redFrames = 0;
                    }
                    elapsed = 0;
                }
            }
            else
            {
                elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (elapsed >= delay)
                {

                    redFrames++;
                    if (redFrames >= totalRedFrames - 2)
                    {
                        redFrames = totalRedFrames - 3;
                    }
                    elapsed = 0;
                }
            }

        }

        public void Draw(SpriteBatch enemySprite, Vector2 location)
        {
            int redWidth = redKoopa.Width / redColumns;
            int redHeight = redKoopa.Height / redRows;
            int rR = redFrames / redColumns;
            int rC = redFrames % redColumns;
            Rectangle redLast = new Rectangle(0, 0, redWidth, redHeight);
            Rectangle redFirst = new Rectangle(redWidth * rC, rR * redHeight, redWidth, redHeight);
            enemySprite.Begin();
            enemySprite.Draw(redKoopa, redLast, redFirst, Color.White);
            enemySprite.End();
        }
    }
}
