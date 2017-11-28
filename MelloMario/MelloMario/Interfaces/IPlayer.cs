namespace MelloMario
{
    #region

    using Microsoft.Xna.Framework;

    #endregion

    internal interface IPlayer
    {
        ICharacter Character { get; }
        ICamera Camera { get; }

        int Coins { get; }
        int Score { get; }
        int Lifes { get; }
        int TimeRemain { get; }

        void AddCoin();
        void AddLife();
        void AddScore(int delta);

        void Initialize(
            string type,
            IWorld newWorld,
            IListener<IGameObject> listener,
            IListener<ISoundable> soundListener);

        void Spawn(IWorld newWorld, Point newLocation);
        void Reset(string type, IListener<IGameObject> listener, IListener<ISoundable> soundListener);
        void Win();
        void Update(int time);
    }
}
