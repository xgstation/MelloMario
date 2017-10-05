using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface IGoombaState
    {
        void ChangeToNormal();
        void ChangeToDefeated();

        void Update(GameTime time);
    }
}
