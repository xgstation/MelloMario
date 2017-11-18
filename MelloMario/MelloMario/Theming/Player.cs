using Microsoft.Xna.Framework;
using MelloMario.Factories;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.Theming
{
    class Player : IPlayer
    {
        private IGameSession session;
        private ICharacter character;

        private int coins;
        private int score;
        private int lifes;
        private int timeRemain;

        public IGameSession Session
        {
            get
            {
                return session;
            }
        }

        public IGameWorld World
        {
            get
            {
                return character.CurrentWorld;
            }
        }

        public ICharacter Character
        {
            get
            {
                return character;
            }
        }

        public int Coins
        {
            get
            {
                return coins;
            }
        }
        public int Score
        {
            get
            {
                return score;
            }
        }
        public int Lifes
        {
            get
            {
                return lifes;
            }
        }
        public int TimeRemain
        {
            get
            {
                return timeRemain;
            }
        }

        public Player(IGameSession session)
        {
            this.session = session;
        }

        public void AddCoin()
        {
            coins += 1;

            if (coins == GameConst.COINS_FOR_LIFE)
            {
                coins = 0;
                lifes += 1;
            }
        }

        public void AddLife()
        {
            lifes += 1;

            if (lifes > GameConst.LIFES_MAX)
            {
                lifes = GameConst.LIFES_MAX;
            }
        }

        public void AddScore(int delta)
        {
            score += delta;
        }

        public void Init(string type, IGameWorld world, Listener listener)
        {
            character = GameObjectFactory.Instance.CreateGameCharacter(type, world, this, world.GetInitialPoint(), listener);
            session.Add(this);

            lifes = GameConst.LIFES_INIT;
            timeRemain = GameConst.LEVEL_TIME * 1000;
        }

        public void Spawn(IGameWorld newWorld, Point newLocation)
        {
            character.Move(newWorld, newLocation);
            session.Move(this);
        }

        public void Reset(string type, Listener listener)
        {
            character.Remove();
            character = GameObjectFactory.Instance.CreateGameCharacter(type, character.CurrentWorld, this, character.CurrentWorld.GetInitialPoint(), listener);

            lifes -= 1;
            timeRemain = GameConst.LEVEL_TIME * 1000;
        }

        public void Win()
        {
            score += GameConst.SCORE_TIME_MULT * timeRemain / 1000;
            timeRemain = GameConst.LEVEL_TIME * 1000;
        }

        public void Update(int time)
        {
            timeRemain -= time;
        }

        public void Draw(int time, SpriteBatch spriteBatch)
        {
            timeRemain -= time;
        }
    }
}
