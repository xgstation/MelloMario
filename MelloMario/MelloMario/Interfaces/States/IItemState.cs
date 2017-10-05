using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface IItemState
    {
        void Show();
        void Collect();

        void Update(GameTime time);
    }
}
