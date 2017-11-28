using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.Objects.UserInterfaces
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal class ScreenManager
    {
        public enum State { start, pause, over, won, inGame }

        private State oldState;

        private Point origin;
        private Point offset;
        private BaseUIObject hud;
        private BaseUIObject splash;

        private string worldName;
        private readonly IPlayer player;

        private ICollection<IGameObject> objectsToBeShown;
    
        public State ScreenState { get; set; }

        public ScreenManager(IPlayer player)
        {
            this.player = player;
        }

        public void Initialize()
        {
            origin = player.Camera.Viewport.Location;
            offset = Point.Zero;
            worldName = player.Character.CurrentWorld.ID;
        }

        public void Feed(ICollection<IGameObject> set)
        {
            objectsToBeShown = set;
        }

        public void Update(int time)
        {
            if (oldState != ScreenState)
            {
                OnStateChange();
            }
            offset = player.Camera.Viewport.Location - origin;
            worldName = player.Character.CurrentWorld.ID;
            (hud as HUD)?.OnHUDInfoChange(player.Lifes, player.Score, player.Coins, player.TimeRemain, worldName);
            hud?.Update(time);
            splash?.Update(time);
        }

        public void Draw(int time, SpriteBatch spriteBatch)
        {
            hud?.Draw(time, spriteBatch);
            if (hud != null)
            {
                ((HUD) hud).IsSplashing = ScreenState == State.won || ScreenState == State.over;
            }
            splash?.Draw(time, spriteBatch);
            //if (ScreenState != State.inGame || ScreenState != State.pause)
            //{
            //    return;
            //}
            //foreach (IGameObject gameObject in objectsToBeShown)
            //{
            //    gameObject.Draw(time, spriteBatch);
            //}
            //objectsToBeShown.Clear();
        }

        private void OnStateChange()
        {
            switch (ScreenState)
            {
                case State.start:
                    splash = new GameStart();
                    hud = null;
                    break;
                case State.pause:
                    splash = new GamePause(offset);
                    break;
                case State.over:
                    splash = new GameOver(offset, player.Lifes);
                    break;
                case State.won:
                    splash = new GameWon(offset);
                    break;
                case State.inGame:
                    hud = new HUD(offset);
                    splash = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            oldState = ScreenState;
        }
    }
}
