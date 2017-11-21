namespace MelloMario
{
    #region

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

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

        void Init(
            string type,
            IWorld newWorld,
            IListener<IGameObject> listener,
            IListener<ISoundable> soundListener,
            ICamera camera);

        void Spawn(IWorld newWorld, Point newLocation);
        void Reset(string type, IListener<IGameObject> listener, IListener<ISoundable> soundListener);
        void Win();
        void Update(int time);
    }
}
