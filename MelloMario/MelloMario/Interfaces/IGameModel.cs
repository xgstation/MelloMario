using MelloMario.Sounds;

namespace MelloMario
{
    interface IGameModel
    {
        SoundController.Songs ThemeMusic { get;}
        void ToggleFullScreen();
        void Pause();
        void Resume();
        void Initialize();
        void Reset();
        void Quit();
        void Update(int time);
        void Draw(int time);
        void ToggleMute();
    }
}
