using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.LevelGen;
using System;

namespace MelloMario
{
    class GameModel
    {
        private IEnumerable<IController> controllers;
        private IGameWorld world;
        private IGameCharacter character;
        private LevelIOJson jsonReader;
        private bool isPaused = false;
        private GameScript script;
        public GameModel()
        {
        }
        public bool IsPaused => isPaused;
        public void LoadControllers(IEnumerable<IController> newControllers)
        {
            controllers = newControllers;
        }

        public void Bind(GameScript script)
        {
            if (this.script == null)
            {
                this.script = script;
            }
            script.Bind(controllers, character, this);
        }

        public void LoadEntities(LevelReader reader)
        {
            Tuple<IGameWorld, IGameCharacter> pair = reader.LoadObjects();
            world = pair.Item1;
            character = pair.Item2;
        }
        public void LoadEntities(LevelIOJson jsonReader)
        {
            if (this.jsonReader == null)
            {
                this.jsonReader = jsonReader;
            }
            Tuple<IGameWorld, IGameCharacter> pair = jsonReader.Load("Main");
            world = pair.Item1;
            character = pair.Item2;
        }

        public void Update(GameTime time)
        {
            foreach (IController controller in controllers)
            {
                controller.Update();
            }
            if (!isPaused)
            {
                foreach (IGameObject obj in world.ScanObjects())
                {
                    obj.Update(time);
                }

                world.Update();
            }
        }

        public void Draw(GameTime time, ZIndex zIndex)
        {
            foreach (IGameObject obj in world.ScanObjects())
            {
                obj.Draw(time, character.Viewport, zIndex);
            }
        }

        public void Pause()
        {
            isPaused = !isPaused;
        }

        public void Reset()
        {
            LoadEntities(jsonReader);
            Bind(script);
        }

        public void Quit()
        {
            // TODO
        }
    }
}
