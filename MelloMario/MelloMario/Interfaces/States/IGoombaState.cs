using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface IGoombaState
    {
        void Show();
        void Defeat();

        void Update(GameTime time);
    }
}
