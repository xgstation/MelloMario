using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.LevelGen;
using System;

namespace MelloMario
{
    //WIP: This is a GameModel class in developing for testing purpose.


    class GameModel2
    {
        private LevelIOJson levelIO;

        private IEnumerable<IController> controllers;

        private IGameWorld previousWorld;
        private IGameCharacter previousCharacter;

        private IGameWorld currentWorld;
        private IGameCharacter currentCharacter;

        public GameModel2(string path)
        {
            levelIO = new LevelIOJson(path, this);
        }

        public void LoadControllers(IEnumerable<IController> newControllers)
        {
            controllers = newControllers;
        }

        public void Bind(GameScript script)
        {
            //script.Bind(controllers, character);
        }

        public void LoadEntities(string index)
        {
            var pair = levelIO.Load(index);
            currentWorld = pair.Item1;
            currentCharacter = pair.Item2;
        }
        public void LoadEntities()
        {
            LoadEntities("Main");
        }
        public void SwitchWorld(string index)
        {
            previousWorld = currentWorld;
            previousCharacter = currentCharacter;
            LoadEntities(index);
        }
        public void Update(GameTime time)
        {
            foreach (IController controller in controllers)
            {
                controller.Update();
            }

            foreach (IGameObject obj in currentWorld.ScanObjects())
            {
                obj.Update(time);
            }

            currentWorld.Update();
        }

        public void Draw(GameTime time, ZIndex zIndex)
        {
            foreach (IGameObject obj in currentWorld.ScanObjects())
            {
                obj.Draw(time, currentCharacter.Viewport, zIndex);
            }
        }
        internal void Quit()
        {

        }
        internal void Reset()
        {
            LoadEntities();
        }
    }
}
