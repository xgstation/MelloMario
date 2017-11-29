﻿namespace MelloMario
{
    #region
    using System;
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
        event EventHandler<GameState> StateChanged;
        GameState State { get; }
        GameMode Mode { get; }
        void Initialize(GameMode mode);
        void Pause();
        void Resume();
        void Reset();
        void Update(int time);
        IWorld LoadLevel(string index);
        void TransistGameWon();
        void Transist();
    }
}
