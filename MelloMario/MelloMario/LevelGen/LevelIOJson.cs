using MelloMario.Factories;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Converters;
using System.Collections;

namespace MelloMario.LevelGen
{
    //WIP: This is a LevelGen class in developing for testing purpose.

    class LevelIOJson : IDisposable
    {

        // Note: Without implementing IDisposable, it may cause resource leak
        //       The code analysis tool generates a warning here
  

        private const int GRIDSIZE = 32;
        private String path;
        private StreamReader input;
        private StreamWriter output;
        private String levelString;
        private List<JToken> loadList;
        private JToken jLevelToken;
        private IGameWorld[] Scenes;
        private GameModel2 gameModel;
        

        public LevelIOJson(String path, GameModel2 gameModel)
        {
            this.path = path;
            this.gameModel = gameModel;
        }
        public bool CanChangeToWrite()
        {
            return false;
        }
        public Tuple<IGameWorld, IGameCharacter> Load(string index)
        {
            Stream inStream = new FileStream(path, FileMode.Open);
            input = new StreamReader(inStream);
            levelString = File.ReadAllText(path);
            jLevelToken = JToken.Parse(levelString);
            loadList = jLevelToken.Children().ToList();
            IGameObjectFactory factory = GameObjectFactory.Instance;
            IGameCharacter character = null;

            inStream.Close();
            return null;
        }

        public void Dispose()
        {
            ((IDisposable)input).Dispose();
        }
        private void AddMatirx(int x, int y, int rows, int columns, GameObjectFactory factory, String type, GameWorld world, SortedSet<Tuple<int,int>> ignoredSet)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (!isIgnored(i, j, ignoredSet))
                        world.AddObject(factory.CreateGameObject(type, world, new Point(x + j * GRIDSIZE, y + i * GRIDSIZE)));
                }
            }
        }
        private void addSingle(int x, int y, GameObjectFactory factory, String type, GameWorld world)
        {
            world.AddObject(factory.CreateGameObject(type, world, new Point(x * GRIDSIZE, y * GRIDSIZE)));
        }
        private bool isIgnored(int i, int j, SortedSet<Tuple<int,int>> ignoredSet)
        {
            return ignoredSet.Contains(new Tuple<int, int>(i, j));
        }

    }
}
