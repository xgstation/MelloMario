namespace MelloMario
{
    interface IGameModel
    {
        void ToggleFullScreen();
        void Pause();
        void Resume();
        void Init();
        void Reset();
        void Quit();
        void Update(int time);
        void Draw(int time);
        void Mute();
    }
}
