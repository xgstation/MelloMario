using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface IGameModel
    {
        bool IsPaused { get; }
        void Pause();
        void Reset();
        void Quit();
        void Update(GameTime time);
        void Draw(GameTime time, ZIndex zIndex);
    }
}
