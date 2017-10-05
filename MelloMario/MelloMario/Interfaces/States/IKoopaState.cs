using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface IKoopaState
    {
        void Show();
        void JumpOn();
        void Defeat();

        void Update(GameTime time);
    }
}
