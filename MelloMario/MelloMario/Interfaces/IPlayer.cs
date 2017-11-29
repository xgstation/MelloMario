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

        void InitCharacter(
            string characterType,
            IWorld newWorld,
            IListener<IGameObject> scoreListener,
            IListener<ISoundable> soundEffectListener);

        void Spawn(IWorld newWorld, Point newLocation);
        void Reset(
            string type,
            IListener<IGameObject> scoreListener,
            IListener<ISoundable> soundEffectListener);
        void Win();
        void Update(int time);
    }
}
