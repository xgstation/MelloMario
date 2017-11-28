namespace MelloMario.Theming
{
    #region

    using System;
    using System.Collections.Generic;
    using MelloMario.Factories;
    using MelloMario.Graphics;
    using MelloMario.Sounds;
    using Microsoft.Xna.Framework;

    #endregion

    [Serializable]
    internal class Player : IPlayer
    {
        private readonly GraphicManager graphicsManager;
        private readonly SoundManager soundManager;
        private readonly IEnumerable<IController> controllers;

        public Player(GraphicManager graphicsManager, SoundManager soundManager, IEnumerable<IController> controllers)
        {
            this.graphicsManager = graphicsManager;
            this.soundManager = soundManager;
            this.controllers = controllers;
        }

        public void BindSession(ISession newSession)
        {
            Session = newSession;
        }

        public ISession Session { get; private set; }

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
            IListener<IGameObject> scoreListener)
        {
            Character = GameObjectFactory.Instance.CreateCharacter(
                characterType,
                world,
                this,
                new Point(32, 32),  //world.GetRespawnPoint(new Point()),
                scoreListener,
                soundManager.SoundEffectListener);
            world.Add((IGameObject) Character);
            Camera = GameObjectFactory.Instance.CreateCamera();

            Lifes = Const.LIFES_INIT;
            TimeRemain = Const.LEVEL_TIME * 1000;
        }

        public void Spawn(IWorld newWorld, Point newLocation)
        {
            Character.Move(newWorld, newLocation);
            Session.Move(this);
        }

        public void Reset(string type, IListener<IGameObject> listener)
        {
            Character.Remove();
            Character = GameObjectFactory.Instance.CreateCharacter(
                type,
                Character.CurrentWorld,
                this,
                Character.CurrentWorld.GetRespawnPoint(((IGameObject) Character).Boundary.Location), // TODO: remove type casting
                listener,
                soundManager.SoundEffectListener);

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
                Camera.LookAt(
                    ((IGameObject) Character).Boundary.Location,
                    Character.CurrentWorld.Boundary); // TODO: remove type casting
            }
        }
    }
}
