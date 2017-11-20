namespace MelloMario.Graphics.Sprites
{
    using Microsoft.Xna.Framework.Graphics;
    using Theming;

    internal class FlashingAnimatedSprite : AnimatedSprite
    {
        public FlashingAnimatedSprite(Texture2D texture, int columns, int rows, int x = 0, int y = 0, int width = 2,
            int height = 2, int interval = Const.ANIMATION_INTERVAL, ZIndex zIndex = ZIndex.Item) : base(texture,
            columns, rows, x, y, width, height, interval, zIndex) { }

        protected override void OnFrame()
        {
            Toggle();
        }
    }
}