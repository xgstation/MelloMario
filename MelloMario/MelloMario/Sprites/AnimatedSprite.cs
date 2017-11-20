using MelloMario.Theming;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.Sprites
{
    internal class AnimatedSprite : BaseTextureSprite
    {
        private readonly int columns;
        private readonly int interval;
        private readonly int rows;
        private int elapsed;

        private int frames;
        private Rectangle refSource;

        public AnimatedSprite(Texture2D texture, int columns, int rows, int x = 0, int y = 0, int width = 2, int height = 2, int interval = GameConst.ANIMATION_INTERVAL, ZIndex zIndex = ZIndex.Item) : base(texture, new Rectangle(x * GameConst.TEXTURE_GRID, y * GameConst.TEXTURE_GRID, width * GameConst.TEXTURE_GRID, height * GameConst.TEXTURE_GRID), zIndex)
        {
            this.columns = columns;
            this.rows = rows;
            this.interval = interval;

            // note: copied from base constructor
            refSource = new Rectangle(x * GameConst.TEXTURE_GRID, y * GameConst.TEXTURE_GRID, width * GameConst.TEXTURE_GRID, height * GameConst.TEXTURE_GRID);

            frames = 0;
            elapsed = 0;
        }

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
    }
}
