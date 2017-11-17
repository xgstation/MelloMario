using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface IPlayer
    {
        IGameWorld World { get; }
        ICharacter Character { get; }

        int Coins { get; }
        int Score { get; }
        int Lifes { get; }
        int TimeRemain { get; }

        void Spawn(IGameWorld newWorld, Point newLocation);
        void AddCoin();
        void AddLife();
        void AddScore(int delta);
        void LevelReset(ICharacter newCharacter);
        void LevelWon();
        void Update(int time);
    }
}
