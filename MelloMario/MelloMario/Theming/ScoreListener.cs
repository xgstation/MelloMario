namespace MelloMario.Theming
{
    #region

    using System;
    using MelloMario.Objects;
    using MelloMario.Objects.Blocks;
    using MelloMario.Objects.Characters;
    using MelloMario.Objects.Items;

    #endregion

    [Serializable]
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

        private void OnLivesChange(BaseCollidableObject m, ScoreEventArgs e)
        {
            player.AddLife();
        }

        private void OnPointGain(BaseCollidableObject m, ScoreEventArgs e)
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
