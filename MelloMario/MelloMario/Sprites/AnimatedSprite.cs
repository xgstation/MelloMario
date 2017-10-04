﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MelloMario.Sprites
{
    class AnimatedSprite : BaseSprite
    {
        private int columns;
        private int rows;
        private double delay;

        private int width;
        private int height;

        private int frames;
        private double elapsed;

        private void UpdateSourceRectangle()
        {
            int x = frames % columns;
            int y = frames / columns;

            ChangeSource(new Point(width * x / columns, height * y / rows));
        }

        protected override void OnAnimate(GameTime time)
        {
            elapsed += time.ElapsedGameTime.TotalMilliseconds;
            if (elapsed >= delay)
            {
                UpdateSourceRectangle();

                frames += 1;
                if (frames == rows * columns)
                {
                    frames = 0;
                }

                elapsed -= delay;
            }
        }

        public AnimatedSprite(Texture2D texture, int columns, int rows, double delay = 250) : base(
            texture, new Point(), new Point(texture.Width / columns, texture.Height / rows)
        )
        {
            this.columns = columns;
            this.rows = rows;
            this.delay = delay;
            frames = 0;
            elapsed = 0;
        }
    }
}
