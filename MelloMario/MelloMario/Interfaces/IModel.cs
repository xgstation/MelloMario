namespace MelloMario
{
    #region

    #endregion

    internal enum GameState
    {
        gameOver,
        gameWon,
        pause,
        onProgress,
        transist
    }
    public enum GameMode
    {
        normal,
        infinite,
        randomMap
    }
    internal interface IModel
    {
        GameState State { get; }
        GameMode Mode { get; }
        void Pause();
        void Resume();
        void Initialize();
        void Reset();
        void Update(int time);
        IWorld LoadLevel(string index);
        void TransistGameWon();
        void Transist();
    }
}
