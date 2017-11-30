namespace MelloMario.Graphics.Sprites
{
    #region

    using System;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    internal class AnimatedSprite : BaseTextureSprite
    {
        private readonly int columns;
        private readonly int interval;
        private readonly int rows;
        private int elapsed;

        private int frames;
        private Rectangle refSource;

        public AnimatedSprite(
            Texture2D texture,
            int columns,
            int rows,
            int x = 0,
            int y = 0,
            int width = 2,
            int height = 2,
            int interval = Const.ANIMATION_INTERVAL,
            ZIndex zIndex = ZIndex.Item) : base(
            texture,
            new Rectangle(
                x * Const.TEXTURE_GRID,
                y * Const.TEXTURE_GRID,
                width * Const.TEXTURE_GRID,
                height * Const.TEXTURE_GRID),
            zIndex)
        {
            this.columns = columns;
            this.rows = rows;
            this.interval = interval;

            // note: copied from base constructor
            refSource = new Rectangle(
                x * Const.TEXTURE_GRID,
                y * Const.TEXTURE_GRID,
                width * Const.TEXTURE_GRID,
                height * Const.TEXTURE_GRID);

            frames = 0;
            elapsed = 0;
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

        private void UpdateSourceRectangle()
        {
            int x = frames % columns;
            int y = frames / columns;

            ChangeSource(new Point(refSource.X + x * refSource.Width, refSource.Y + y * refSource.Height));
        }
    }
}
