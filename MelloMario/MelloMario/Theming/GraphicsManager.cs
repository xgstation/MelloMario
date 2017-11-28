namespace MelloMario.Theming
{
    #region

    using MelloMario.Objects.UserInterfaces;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    internal class GraphicsManager
    {
        private readonly Game1 game;
        private SpriteBatch spriteBatchUI;
        private SpriteBatch spriteBatchGameObjects;
        private readonly UIManager uiManager;
        private IPlayer player;
        private IModel model;
        public GraphicsManager(Game1 game)
        {
            this.game = game;
            uiManager = new UIManager(game)
            {
                ScreenState = UIManager.State.start
            };
        }

        public void Initialize()
        {
            
        }
        public void BindGraphicsDevice(GraphicsDevice newGraphicsDevice)
        {
            spriteBatchUI = new SpriteBatch(newGraphicsDevice);
            spriteBatchGameObjects = new SpriteBatch(newGraphicsDevice);
        }

        public void BindPlyaer(IPlayer newPlayer)
        {
            player = newPlayer;
        }

        public void BindModel(IModel newModel)
        {
            model = newModel;
        }

        public void Update(int time)
        {
            uiManager.Update(time);
        }

        public void Draw(int time)
        {
            DrawUI(time);
           // DrawGameObjects(time);
        }

        private void DrawUI(int time)
        {
            spriteBatchUI.Begin(SpriteSortMode.BackToFront);
            uiManager.Draw(time, spriteBatchUI);
            spriteBatchUI.End();   
        }

        private void DrawGameObjects(int time)
        {
            spriteBatchGameObjects.Begin(SpriteSortMode.BackToFront);
            if (player == null)
            {
                return;
            }
            foreach (IGameObject obj in player.Character.CurrentWorld.ScanNearby(player.Camera.Viewport))
            {
                obj.Draw(model?.State == GameState.pause ? 0 : time, spriteBatchGameObjects);
            }
            spriteBatchGameObjects.End();
        }

    }
}
