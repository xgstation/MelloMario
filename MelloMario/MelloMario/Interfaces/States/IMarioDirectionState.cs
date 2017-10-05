using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface IMarioDirectionState
    {
        void TurnLeft();
        void TurnRight();

        void Update(GameTime time);
    }
}
