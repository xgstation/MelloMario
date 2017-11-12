using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MelloMario.Theming;

namespace MelloMario.Sprites
{
    class AnimatedSprite : BaseTextureSprite
    {
        private int columns;
        private int rows;
        private int interval;
        private Rectangle refSource;

        private int frames;
        private int elapsed;

        private void UpdateSourceRectangle()
        {
            int x = frames % columns;
            int y = frames / columns;

            ChangeSource(new Point(refSource.X + x * refSource.Width, refSource.Y + y * refSource.Height));
        }

        protected override void OnAnimate(int time)
        {
            elapsed += time;

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

        public AnimatedSprite(SpriteBatch spriteBatch, Texture2D texture, int columns, int rows, int x, int y, int width, int height, int interval = GameConst.ANIMATION_INTERVAL, ZIndex zIndex = ZIndex.item) : base(
            spriteBatch,
            texture,
            new Rectangle(
                x * GameConst.TEXTURE_GRID, y * GameConst.TEXTURE_GRID,
                width * GameConst.TEXTURE_GRID, height * GameConst.TEXTURE_GRID
            ),
            zIndex
        )
        {
            this.columns = columns;
            this.rows = rows;
            this.interval = interval;

            // note: copied from base constructor
            refSource = new Rectangle(
                x * GameConst.TEXTURE_GRID, y * GameConst.TEXTURE_GRID,
                width * GameConst.TEXTURE_GRID, height * GameConst.TEXTURE_GRID
            );

            frames = 0;
            elapsed = 0;
        }
    }
}
