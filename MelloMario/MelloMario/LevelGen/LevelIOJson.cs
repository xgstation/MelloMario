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
        protected class CharacterConverter : JsonConverter
        {

            public override bool CanConvert(Type objectType)
            {
                throw new NotImplementedException();
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }
        protected class MapConverter : JsonConverter
        {

            public override bool CanConvert(Type objectType)
            {
                throw new NotImplementedException();
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override bool CanWrite
            {
                get { return false; }
            }
        }
        protected class ScoreConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                throw new NotImplementedException();
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }
        protected class CustomScriptConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                throw new NotImplementedException();
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }


        private const int GRIDSIZE = 32;
        private String path;
        private StreamReader input;
        private StreamWriter output;
        private String levelString;
        private List<JToken> loadList;
        private JToken jLevelToken;
        private IGameWorld[] Scenes;
        private LevelIOMode levelIOMode;

        public enum LevelIOMode { load, save };

        public LevelIOJson(String path, LevelIOMode levelIOMode)
        {
            this.path = path;
            this.levelIOMode = levelIOMode;
            if (levelIOMode is LevelIOMode.load)
            {
                
            }
            else
            {

            }
        }
        public bool CanChangeToWrite()
        {
            return false;
        }
        public void Load(out IEnumerable<IGameWorld> gameworlds, out IEnumerable<IGameCharacter> characters)
        {
            gameworlds = null;
            characters = null;
        }
        public Tuple<IEnumerable<IGameWorld>,IEnumerable<IGameCharacter>> Load()
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
        public Tuple<IGameWorld[], IGameCharacter> LoadObjects()
        {
            /*
            for (int i = 0; i < size.Y; ++i)
            {
                string curLine = input.ReadLine();
                for (int j = 0; j < size.X; ++j)
                {
                    char curChar = curLine.ElementAt(j);
                    switch (curChar)
                    {
                        case '!':
                            Tuple<IGameCharacter, IGameObject> pair = factory.CreateGameCharacter("Mario", world, new Point(j * GRIDSIZE, i * GRIDSIZE));
                            character = pair.Item1;
                            world.AddObject(pair.Item2);
                            break;

                        //blocks
                        case 'F':
                            world.AddObject(factory.CreateGameObject("Floor", world, new Point(j * GRIDSIZE, i * GRIDSIZE)));
                            break;
                        case 'B':
                            world.AddObject(factory.CreateGameObject("Brick", world, new Point(j * GRIDSIZE, i * GRIDSIZE)));
                            break;
                        case 'b':
                            world.AddObject(factory.CreateGameObject("HiddenBrick", world, new Point(j * GRIDSIZE, i * GRIDSIZE)));
                            break;
                        case 'S':
                            world.AddObject(factory.CreateGameObject("Stair", world, new Point(j * GRIDSIZE, i * GRIDSIZE)));
                            break;
                        case 'Q':
                            world.AddObject(factory.CreateGameObject("Question", world, new Point(j * GRIDSIZE, i * GRIDSIZE)));
                            break;
                        case 'q':
                            world.AddObject(factory.CreateGameObject("HiddenQuestion", world, new Point(j * GRIDSIZE, i * GRIDSIZE)));
                            break;
                        case 'P':
                            world.AddObject(factory.CreateGameObject("PipelineLeftIn", world, new Point(j * GRIDSIZE, i * GRIDSIZE)));
                            world.AddObject(factory.CreateGameObject("PipelineRightIn", world, new Point((j + 1) * GRIDSIZE, i * GRIDSIZE)));
                            break;
                        case 'V':
                            world.AddObject(factory.CreateGameObject("PipelineLeft", world, new Point(j * GRIDSIZE, i * GRIDSIZE)));
                            world.AddObject(factory.CreateGameObject("PipelineRight", world, new Point((j + 1) * GRIDSIZE, i * GRIDSIZE)));
                            break;

                        //harm
                        case 'G':
                            world.AddObject(factory.CreateGameObject("Goomba", world, new Point(j * GRIDSIZE, i * GRIDSIZE)));
                            break;
                        case 'K':
                            world.AddObject(factory.CreateGameObject("GreenKoopa", world, new Point(j * GRIDSIZE, i * GRIDSIZE)));
                            break;
                        case 'D':
                            world.AddObject(factory.CreateGameObject("RedKoopa", world, new Point(j * GRIDSIZE, i * GRIDSIZE)));
                            break;

                        //entities
                        case 'C':
                            world.AddObject(factory.CreateGameObject("Coin", world, new Point(j * GRIDSIZE, i * GRIDSIZE)));
                            break;
                        case '1':
                            world.AddObject(factory.CreateGameObject("OneUpMushroom", world, new Point(j * GRIDSIZE, i * GRIDSIZE)));
                            break;
                        case 'R':
                            world.AddObject(factory.CreateGameObject("FireFlower", world, new Point(j * GRIDSIZE, i * GRIDSIZE)));
                            break;
                        case 'M':
                            world.AddObject(factory.CreateGameObject("SuperMushroom", world, new Point(j * GRIDSIZE, i * GRIDSIZE)));
                            break;
                        case 'T':
                            world.AddObject(factory.CreateGameObject("Star", world, new Point(j * GRIDSIZE, i * GRIDSIZE)));
                            break;
                        default:
                            //do nothing, blank space dont add anything to blocks
                            break;
                    }
                }
            }
            **/

            //TODO: change null
            return new Tuple<IGameWorld[], IGameCharacter>(Scenes, null);
        }
        public void SaveObjects(IGameWorld[] scenes, IGameCharacter character) { }
    }
}
