namespace MelloMario
{
    #region

    using Microsoft.Xna.Framework.Graphics;

    #endregion

    internal interface IModel
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
        void ToggleMute();
        IWorld LoadLevel(string index);
        void TransistGameWon();
        void Transist();
    }
}
