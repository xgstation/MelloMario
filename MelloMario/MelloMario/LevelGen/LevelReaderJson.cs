using MelloMario.Factories;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Converters;

namespace MelloMario.LevelGen
{

    /*
     documentation/key for level gen

        first line height of level
        second line width of level

        !:Mario

        Blocks:
        F:Floor
        B:Brick
        b:Hidden Brick
        S:Stair
        Q:Question
        q:Hidden Question
        P:Pipe(to the right one block)
        V:Vertical Pipe(to the right one block)

        Entities:
        C:Coin
        1:One Up Mushroom
        R:Fire Flower
        M:Super Mushroom
        T:Star

        Harm:
        G:Goomba
        K:Green Koopa
        D:Red Kappa

        _:Blank

         */

    class LevelReaderJson : IDisposable
    {
        public class BrickConverter : JsonConverter
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
        private const int GRIDSIZE = 32;
        private Point size;
        // Note: Without implementing IDisposable, it may cause resource leak
        //       The code analysis tool generates a warning here
        private StreamReader input;
        //
        private String jsonString;
        private JsonReader jReader;
        private List<JObject> loadList;
        private JObject jMap;
        public LevelReaderJson(String path)
        {
            Stream inStream = new FileStream(path, FileMode.Open);
            input = new StreamReader(inStream);

            String heightAsString = input.ReadLine();
            String widthAsString = input.ReadLine();

            size = new Point(Int32.Parse(widthAsString), Int32.Parse(heightAsString));
            ///
            jsonString = File.ReadAllText(path);
            jMap = JObject.Parse(jsonString);

        }

        public void Dispose()
        {
            ((IDisposable)input).Dispose();
        }
        private void addMatirx(int x, int y, int rows, int columns, GameObjectFactory factory, String type, GameWorld world, SortedSet<Tuple<int,int>> ignoredSet)
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
        public Tuple<IGameWorld, IGameCharacter> LoadObjects()
        {
            IGameObjectFactory factory = GameObjectFactory.Instance;
            IGameWorld world = factory.CreateGameWorld(size);
            IGameCharacter character = null;
            //
            IList<JToken> maps = jMap["Map"].Children().ToList();
            Point mapSize = jMap["Map"]["Main"].Value<Point>("Size");
            foreach (var map in maps)
            {

            }
            //
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

            return new Tuple<IGameWorld, IGameCharacter>(world, character);
        }
    }
}
