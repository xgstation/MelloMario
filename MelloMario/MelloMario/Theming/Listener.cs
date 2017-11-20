using System;
using MelloMario.Objects.Blocks;
using MelloMario.Objects.Items;
using MelloMario.Objects.Characters;

namespace MelloMario.Theming
{
    internal class GameEventArgs : EventArgs
    {
        public int Points { get; set; }
    }

    internal class Listener : IListener
    {
        private readonly Model model;
        private readonly IPlayer player;

        public Listener(Model model, IPlayer player)
        {
            this.model = model;
            this.player = player;
        }

        public void Subscribe(IGameObject gameObject)
        {
            switch (gameObject)
            {
                case Coin coin:
                    coin.HandlerCoins += OnCoinCollect;
                    break;
                case Mario mario:
                    mario.HandlerGameOver += OnGameOver;
                    break;
                case Flag flag:
                    flag.HandlerTimeScore += OnLevelWon;
                    break;
                case BaseCollidableObject collidableObject:
                    collidableObject.HandlerPoints += OnPointGain;
                    collidableObject.HandlerLives += OnLivesChange;
                    break;
            }
        }

        private void OnLivesChange(BaseCollidableObject m, GameEventArgs e)
        {
            player.AddLife();
        }

        private void OnPointGain(BaseCollidableObject m, GameEventArgs e)
        {
            player.AddScore(e.Points);
        }

        private void OnLevelWon(Flag m, EventArgs e)
        {
            model.TransistGameWon();
        }

        private void OnGameOver(Mario m, EventArgs e)
        {
            model.Transist();
        }

        private void OnCoinCollect(Coin m, EventArgs e)
        {
            player.AddCoin();
        }
    }
}
