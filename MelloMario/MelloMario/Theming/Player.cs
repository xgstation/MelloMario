namespace MelloMario.Theming
{
    #region

    using MelloMario.Factories;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    internal class Player : IPlayer
    {
        public Player(ISession session)
        {
            Session = session;
        }

        public ISession Session { get; }

        public ICharacter Character { get; private set; }

        public ICamera Camera { get; private set; }

        public int Coins { get; private set; }

        public int Score { get; private set; }

        public int Lifes { get; private set; }

        public int TimeRemain { get; private set; }

        public void AddCoin()
        {
            Coins += 1;

            if (Coins == Const.COINS_FOR_LIFE)
            {
                Coins = 0;
                Lifes += 1;

                //TODO:Move this into soundcontroller
                //SoundManager.OneUpCollect.Play();
            }
        }

        public void AddLife()
        {
            Lifes += 1;

            if (Lifes > Const.LIFES_MAX)
            {
                Lifes = Const.LIFES_MAX;
            }
        }

        public void AddScore(int delta)
        {
            Score += delta;
        }

        public void Init(
            string type,
            IWorld world,
            IListener<IGameObject> listener,
            IListener<ISoundable> soundListener,
            ICamera newCamera)
        {
            Character = GameObjectFactory.Instance.CreateGameCharacter(
                type,
                world,
                this,
                world.GetInitialPoint(),
                listener,
                soundListener);
            Session.Add(this);
            Camera = newCamera;

            Lifes = Const.LIFES_INIT;
            TimeRemain = Const.LEVEL_TIME * 1000;
        }

        public void Spawn(IWorld newWorld, Point newLocation)
        {
            Character.Move(newWorld, newLocation);
            Session.Move(this);
        }

        public void Reset(string type, IListener<IGameObject> listener, IListener<ISoundable> soundListener)
        {
            Character.Remove();
            Character = GameObjectFactory.Instance.CreateGameCharacter(
                type,
                Character.CurrentWorld,
                this,
                Character.CurrentWorld.GetInitialPoint(),
                listener,
                soundListener);

            if (Lifes > 0)
            {
                Lifes -= 1;
            }
            TimeRemain = Const.LEVEL_TIME * 1000;
        }

        public void Win()
        {
            Score += Const.SCORE_TIME_MULT * TimeRemain / 1000;
            TimeRemain = Const.LEVEL_TIME * 1000;
        }

        public void Update(int time)
        {
            TimeRemain -= time;
            if (Character != null)
            {
                Camera?.LookAt(
                    ((IGameObject) Character).Boundary.Location,
                    Character.CurrentWorld.Boundary); // TODO: remove type casting
            }
        }

        public void Draw(int time, SpriteBatch spriteBatch)
        {
        }
    }
}
