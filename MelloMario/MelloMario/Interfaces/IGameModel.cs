using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    internal interface IGameModel
    {
        IPlayer ActivePlayer { get; }
        bool IsPaused { get; }
        void ToggleFullScreen();
        void Pause();
        void Resume();
        void Init();
        void Reset();
        void Quit();
        void Update(int time);
        void Infinite();
        void Normal();
        void Draw(int time, SpriteBatch spriteBatch);
    }
}
