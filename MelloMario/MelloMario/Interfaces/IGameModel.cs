using MelloMario.Sounds;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    interface IGameModel
    {
        SoundController.Songs ThemeMusic { get; }
        void ToggleFullScreen();
        void Pause();
        void Resume();
        void Init();
        void Reset();
        void Quit();
        void Update(int time);
        void Draw(int time, SpriteBatch spriteBatch);
        void ToggleMute();
    }
}
