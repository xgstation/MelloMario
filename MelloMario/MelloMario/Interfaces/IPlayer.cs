using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    internal interface IPlayer
    {
        IGameWorld World { get; }
        ICharacter Character { get; }
        ICamera PlayerCamera { get; }

        int Coins { get; }
        int Score { get; }
        int Lifes { get; }
        int TimeRemain { get; }

        void AddCoin();
        void AddLife();
        void AddScore(int delta);
        void Init(string type, IGameWorld newWorld, IListener listener, ICamera camera);
        void Spawn(IGameWorld newWorld, Point newLocation);
        void Reset(string type, IListener listener);
        void Win();
        void Update(int time);
        void Draw(int time, SpriteBatch spriteBatch);
    }
}