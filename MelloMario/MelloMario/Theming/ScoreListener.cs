namespace MelloMario.Theming
{
    #region

    using System;
    using MelloMario.Objects;
    using MelloMario.Objects.Blocks;
    using MelloMario.Objects.Characters;
    using MelloMario.Objects.Items;

    #endregion

    internal class ScoreEventArgs : EventArgs
    {
        public int Points { get; set; }
    }

    internal class ScoreListener : IListener<IGameObject>
    {
        private readonly IModel model;
        private readonly IPlayer player;

        public ScoreListener(IModel model, IPlayer player)
        {
            this.model = model;
            this.player = player;
        }

        public void Subscribe(IGameObject gameObject)
        {
            if (gameObject is BaseCollidableObject collidableObject)
            {
                switch (gameObject)
                {
                    case Coin coin:
                        coin.HandlerCoins += OnCoinCollect;
                        break;
                    case Mario mario:
                        mario.HandlerGameOver += OnGameOver;
                        break;
                    case FlagPole flag:
                        flag.HandlerTimeScore += OnLevelWon;
                        break;
                }

                collidableObject.HandlerPoints += OnPointGain;
                collidableObject.HandlerLives += OnLivesChange;
            }
        }

        private void OnLivesChange(BaseCollidableObject m, ScoreEventArgs e)
        {
            player.AddLife();
        }

        private void OnPointGain(BaseCollidableObject m, ScoreEventArgs e)
        {
            player.AddScore(e.Points);
        }

        private void OnLevelWon(FlagPole m, EventArgs e)
        {
            model.TransistGameWon();
        }

        private void OnGameOver(Mario m, EventArgs e)
        {
            model.TransistOver();
        }

        private void OnCoinCollect(Coin m, EventArgs e)
        {
            player.AddCoin();
        }
    }
}
