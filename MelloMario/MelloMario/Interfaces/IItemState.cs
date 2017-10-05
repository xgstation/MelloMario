using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface IItemState
    {
        void ChangeToNormal();
        void ChangeToDefeated();

        void Update(GameTime time);
    }
}
