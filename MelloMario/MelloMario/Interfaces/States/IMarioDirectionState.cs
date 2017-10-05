using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface IMarioDirectionState
    {
        void TurnRight();
        void TurnLeft();

        void Update(GameTime time);
    }
}
