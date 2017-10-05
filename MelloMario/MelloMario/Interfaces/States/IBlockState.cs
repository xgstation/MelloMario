using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface IBlockState
    {
        void Show();
        void Hide();
        void Bump();
        void Destroy(); // TODO: this should be merged with bump (based on the item a block contain and the power-up state of mario)
        void UseUp(); // TODO: this should be merged with bump (based on the item a block contain and the power-up state of mario)

        void Update(GameTime time);
    }
}
