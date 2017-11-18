using MelloMario.Theming;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.Sprites
{
    internal class StaticSprite : BaseTextureSprite
    {
        public StaticSprite(Texture2D texture, int x = 0, int y = 0, int width = 2, int height = 2,
            ZIndex zIndex = ZIndex.Item) : base(texture,
            new Rectangle(x * GameConst.TEXTURE_GRID, y * GameConst.TEXTURE_GRID, width * GameConst.TEXTURE_GRID,
                height * GameConst.TEXTURE_GRID), zIndex) { }

        protected override void OnAnimate(int time)
        {
            // Do nothing
        }
    }
}