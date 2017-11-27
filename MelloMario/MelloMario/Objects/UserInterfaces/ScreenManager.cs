using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.Objects.UserInterfaces
{
    using Microsoft.Xna.Framework.Graphics;

    internal class ScreenManager
    {
        public enum State { start, pause, over, won, inGame }

        private BaseUIObject UI;
        private IPlayer player;
        private ICollection<IGameObject> objectsToBeShown;


        public State ScreenState { get; set; }


        public ScreenManager(IPlayer player)
        {
            this.player = player;
        }

        public void Feed(ICollection<IGameObject> set)
        {
            objectsToBeShown = set;
        }
        
        public void Update(int time)
        {
            switch (ScreenState)
            {
                case State.start:
                    UI = new GameStart(player);
                    break;
                case State.pause:
                    UI = new GamePause();
                    break;
                case State.over:
                    UI = new GameOver();
                    break;
                case State.won:
                    UI = new GameWon();
                    break;
                case State.inGame:
                    UI = new HUD();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Draw(int time, SpriteBatch spriteBatch)
        {
            foreach (IGameObject gameObject in objectsToBeShown)
            {
                gameObject.Draw(time, spriteBatch);
            }
        }
    }
}
