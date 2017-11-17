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
        void AddScore(int delta);
        void Reset(ICharacter newCharacter);
        void Update(int time);
    }
}
