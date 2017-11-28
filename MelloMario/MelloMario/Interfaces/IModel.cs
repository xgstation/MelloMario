namespace MelloMario
{
    #region

    using Microsoft.Xna.Framework.Graphics;

    #endregion
    internal enum GameState
    {
        Start,
        GameOver,
        GameWon,
        Pause,
        OnProgress,
        Transist
    }
    internal interface IModel
    {
         GameState State { get; }
        IPlayer ActivePlayer { get; }
        bool IsPaused { get; }
        void ToggleFullScreen();
        void Pause();
        void Resume();
        void Initialize();
        void Reset();
        void Quit();
        void Update(int time);
        void Infinite();
        void Normal();
        IWorld LoadLevel(string index);
        void TransistGameWon();
        void Transist();
    }
}
