using MelloMario.MarioObjects;
using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface IBlockState
    {
        void Show();
        void Hide();
        void Bump(Mario mario);

        void Update(GameTime time);
    }
}
