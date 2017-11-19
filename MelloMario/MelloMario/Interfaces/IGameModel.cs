using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    internal interface IGameModel
    {
        void ToggleFullScreen();
        void Pause();
        void Resume();
        void Init();
        void Reset();
        void Quit();
        void Update(int time);
        void Normal();
        void Draw(int time, SpriteBatch spriteBatch);
        void ToggleMute();
        void Infinite();
    }
}