using Microsoft.Xna.Framework;

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

        public Player(IGameSession session, ICharacter character)
        {
            this.session = session;
            this.session.Add(this);
            this.character = character;

            lifes = GameConst.INIT_LIFES;
            timeRemain = GameConst.LEVEL_TIME * 1000;
        }

        public void Spawn(IGameWorld newWorld, Point newLocation)
        {
            character.Move(newWorld, newLocation);
            Session.Move(this);

            timeRemain = GameConst.LEVEL_TIME * 1000;
        }

        public void AddCoin()
        {
            coins += 1;

            if (coins == GameConst.COINS_FOR_LIVE)
            {
                coins = 0;
                lifes += 1;
            }
        }

        public void AddScore(int delta)
        {
            score += delta;
        }

        public void Reset(ICharacter newCharacter)
        {
            character.Remove();
            character = newCharacter;

            lifes -= 1;
            timeRemain = GameConst.LEVEL_TIME * 1000;
        }

        public void Update(int time)
        {
            timeRemain -= time;
        }
    }
}
