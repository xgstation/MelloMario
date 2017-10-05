using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface IKoopaState
    {
        void ChangeToNormal();
        void ChangeToJumpOn();
        void ChangeToDefeated();

        void Update(GameTime time);
    }
}
