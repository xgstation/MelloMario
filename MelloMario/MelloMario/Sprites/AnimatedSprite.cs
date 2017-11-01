using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace MelloMario.Sprites
{
    class AnimatedSprite : BaseSprite
    {
        private int columns;
        private int rows;

        private int width;
        private int height;

        private int frames;
        private int elapsed;
        private int interval;

        private void UpdateSourceRectangle()
        {
            int x = frames % columns;
            int y = frames / columns;

            ChangeSource(new Point(width * x / columns, height * y / rows));
        }

        protected override void OnAnimate(GameTime time)
        {
            elapsed += time.ElapsedGameTime.Milliseconds;

            if (elapsed >= interval)
            {
                UpdateSourceRectangle();
                OnFrame();

                frames += 1;
                if (frames == rows * columns)
                {
                    frames = 0;
                }

                elapsed -= interval;
            }
        }

        protected virtual void OnFrame()
        {
            // nothing by default
        }

        public AnimatedSprite(SpriteBatch spriteBatch, Texture2D texture, int columns, int rows, ZIndex activeZIndex = ZIndex.item, int interval = 100) : base(
            spriteBatch, texture, new Point(), new Point(texture.Width / columns, texture.Height / rows), activeZIndex
        )
        {
            this.columns = columns;
            this.rows = rows;
            this.interval = interval;
            width = texture.Width;
            height = texture.Height;
            frames = 0;
            elapsed = 0;
        }
    }
}
