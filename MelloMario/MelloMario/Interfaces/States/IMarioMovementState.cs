using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface IMarioMovementState
    {
        void Crouch();
        void Idle();
        void Jump();
        void Walk();

        void Update(GameTime time);
    }
}
