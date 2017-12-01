namespace MelloMario.Theming
{
    #region

    using System;
    using MelloMario.Factories;
    using Microsoft.Xna.Framework;

    #endregion

    internal class Player : IPlayer
    {
        // public ISession Session { get; private set; }
        
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
            }
        }

        public void AddLife()
        {
            if (Lifes < Const.LIFES_MAX)
            {
                Lifes += 1;
            }
        }

        public void AddScore(int delta)
        {
            Score += delta;
        }

        public void InitCharacter(
            string characterType,
            IWorld world,
            IListener<IGameObject> scoreListener,
            IListener<ISoundable> soundEffectListener)
        {
            Tuple<IGameObject, ICharacter> pair = GameObjectFactory.Instance.CreateCharacter(
                characterType,
                world,
                this,
                world.GetRespawnPoint(new Point()),
                scoreListener,
                soundEffectListener);
            world.Add(pair.Item1);
            Character = pair.Item2;
            Camera = GameObjectFactory.Instance.CreateCamera();

            Lifes = Const.LIFES_INIT;
            TimeRemain = Const.LEVEL_TIME * 1000;
        }

        public void Spawn(IWorld newWorld, Point newLocation)
        {
            Character.Move(newWorld, newLocation);
            //Session.Move(this);
        }

        public void Reset(
            string type,
            IListener<IGameObject> scoreListener,
            IListener<ISoundable> soundEffectListener)
        {
            IWorld world = Character.CurrentWorld;
            Character.Remove();

            Tuple<IGameObject, ICharacter> pair = GameObjectFactory.Instance.CreateCharacter(
                type,
                world,
                this,
                world.GetRespawnPoint(((IGameObject) Character).Boundary.Center), // TODO: remove type casting
                scoreListener,
                soundEffectListener);
            world.Add(pair.Item1);
            Character = pair.Item2;

                Lifes -= 1;
            if (Lifes < 0)
            {
                Lifes = Const.LIFES_INIT;
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
            if (TimeRemain >0)
            { TimeRemain -= time;
            }
            
            if (Character != null)
            {
                Camera.LookAt(
                    ((IGameObject) Character).Boundary.Location,
                    Character.CurrentWorld.Boundary); // TODO: remove type casting
            }

        }
    }
}
