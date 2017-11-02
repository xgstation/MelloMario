using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface IGameModel
    {
        void SwitchWorld(string index);
        void ToggleFullScreen();
        void Pause();
        void Reset();
        void Quit();
        void Update(GameTime time);
        void Draw(GameTime time);
    }
}
