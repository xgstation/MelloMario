using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.Theming;

namespace MelloMario.Sprites
{
    class StaticSprite : BaseTextureSprite
    {
        protected override void OnAnimate(int time)
        {
            // Do nothing
        }

        public StaticSprite(SpriteBatch spriteBatch, Texture2D texture, int x = 0, int y = 0, int width = 2, int height = 2, ZIndex zIndex = ZIndex.item) :
            base(
                spriteBatch,
                texture,
                new Rectangle(
                    x * GameConst.TEXTURE_GRID, y * GameConst.TEXTURE_GRID,
                    width * GameConst.TEXTURE_GRID, height * GameConst.TEXTURE_GRID
                ),
                zIndex
            )
        {
        }
    }
}
