using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MelloMario.LevelGen;
using System;
using MelloMario.Containers;
using MelloMario.Scripts;
using MelloMario.Theming;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    class GameModel : IGameModel
    {
        private Game1 game;
        private IDictionary<string, IGameWorld> worlds;
        private IGameWorld currentWorld;
        private string currentWorldIndex;
        private IEnumerable<IController> controllers;
        private IPlayer player;
        private bool isPaused;

        public GameModel(Game1 game)
        {
            this.game = game;
            worlds = new Dictionary<string, IGameWorld>();
            currentWorldIndex = "Main";
        }

        public void LoadControllers(IEnumerable<IController> newControllers)
        {
            controllers = newControllers;
        }

        public void ToggleFullScreen()
        {
            game.ToggleFullScreen();
        }

        public void Pause()
        {
            isPaused = true;

            new PausedScript().Bind(controllers, this, player);
        }

        public void Resume()
        {
            isPaused = false;

            new PlayingScript().Bind(controllers, this, player);
        }

        public void SwitchWorld(string index)
        {
            
            if (worlds.ContainsKey(index))
            {
                currentWorld = worlds[index];
                currentWorldIndex = index;
            }
            else
            {
                LevelIOJson reader = new LevelIOJson("Content/ExampleLevel.json", game.GraphicsDevice);
                reader.SetModel(this);
                Tuple<IGameWorld, IPlayer> pair = reader.Load(index);
                currentWorldIndex = index;
                currentWorld = pair.Item1;
                worlds.Add(currentWorldIndex, currentWorld);
            }

            player.Spawn(currentWorld);
        }

        public void Reset()
        {
            GameDatabase.Clear();

            LevelIOJson reader = new LevelIOJson("Content/ExampleLevel.json", game.GraphicsDevice);
            reader.SetModel(this);
            Tuple<IGameWorld, IPlayer> pair = reader.Load(currentWorldIndex);
            currentWorld = pair.Item1;
            player = pair.Item2;
            if (!worlds.ContainsKey(currentWorldIndex))
            {
                worlds.Add(currentWorldIndex, currentWorld);
            }
            isPaused = false;
            new PlayingScript().Bind(controllers, this, player);
        }

        public void Quit()
        {
            game.Exit();
        }

        public void Update(int time)
        {
            foreach (IController controller in controllers)
            {
                controller.Update();
            }


            if (!isPaused)
            {
                // reserved for multiplayer
                ISet<IGameObject> updating = new HashSet<IGameObject>();

                foreach (IGameObject obj in currentWorld.ScanNearby(player.Sensing))
                {
                    updating.Add(obj);
                }

                foreach (IGameObject obj in updating)
                {
                    obj.Update(time);
                }

                currentWorld.Update();
            }
        }

        public void Draw(int time)
        {
            foreach (ZIndex zIndex in Enum.GetValues(typeof(ZIndex)))
            {
                foreach (IGameObject obj in currentWorld.ScanNearby(player.Viewport))
                {
                    if (isPaused)
                    {
                        obj.Draw(0, player.Viewport, zIndex);
                    }
                    else
                    {
                        obj.Draw(time, player.Viewport, zIndex);
                    }
                }
            }
        }
    }
}
