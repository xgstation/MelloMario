using MelloMario.Factories;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using System.Linq;

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

    class LevelReader : IDisposable
    {
        private const int SCALE = 32;
        private Point size;
        private StreamReader input;

        public LevelReader(String path)
        {
            Stream inStream = new FileStream(path, FileMode.Open);
            input = new StreamReader(inStream);

            String heightAsString = input.ReadLine();
            String widthAsString = input.ReadLine();

            size = new Point(Int32.Parse(widthAsString), Int32.Parse(heightAsString));
        }

        public void Dispose()
        {
            input.Dispose();
        }

        public Tuple<IGameWorld, IGameCharacter> LoadObjects()
        {
            IGameObjectFactory factory = GameObjectFactory.Instance;

            IGameWorld world = factory.CreateGameWorld(size);
            IGameCharacter character = null;

            for (int i = 0; i < size.Y; ++i)
            {
                string curLine = input.ReadLine();
                for (int j = 0; j < size.X; ++j)
                {
                    char curChar = curLine.ElementAt(j);
                    switch (curChar)
                    {
                        case '!':
                            Tuple<IGameCharacter, IGameObject> pair = factory.CreateGameCharacter("Mario", world, new Point(j * SCALE, i * SCALE));
                            character = pair.Item1;
                            world.AddObject(pair.Item2);
                            break;

                        //blocks
                        case 'F':
                            world.AddObject(factory.CreateGameObject("Floor", world, new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'B':
                            world.AddObject(factory.CreateGameObject("Brick", world, new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'b':
                            world.AddObject(factory.CreateGameObject("HiddenBrick", world, new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'S':
                            world.AddObject(factory.CreateGameObject("Stair", world, new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'Q':
                            world.AddObject(factory.CreateGameObject("Question", world, new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'q':
                            world.AddObject(factory.CreateGameObject("HiddenQuestion", world, new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'P':
                            world.AddObject(factory.CreateGameObject("PipelineLeftIn", world, new Point(j * SCALE, i * SCALE)));
                            world.AddObject(factory.CreateGameObject("PipelineRightIn", world, new Point((j + 1) * SCALE, i * SCALE)));
                            break;
                        case 'V':
                            world.AddObject(factory.CreateGameObject("PipelineLeft", world, new Point(j * SCALE, i * SCALE)));
                            world.AddObject(factory.CreateGameObject("PipelineRight", world, new Point((j + 1) * SCALE, i * SCALE)));
                            break;

                        //harm
                        case 'G':
                            world.AddObject(factory.CreateGameObject("Goomba", world, new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'K':
                            world.AddObject(factory.CreateGameObject("GreenKoopa", world, new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'D':
                            world.AddObject(factory.CreateGameObject("RedKoopa", world, new Point(j * SCALE, i * SCALE)));
                            break;

                        //entities
                        case 'C':
                            world.AddObject(factory.CreateGameObject("Coin", world, new Point(j * SCALE, i * SCALE)));
                            break;
                        case '1':
                            world.AddObject(factory.CreateGameObject("OneUpMushroom", world, new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'R':
                            world.AddObject(factory.CreateGameObject("FireFlower", world, new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'M':
                            world.AddObject(factory.CreateGameObject("SuperMushroom", world, new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'T':
                            world.AddObject(factory.CreateGameObject("Star", world, new Point(j * SCALE, i * SCALE)));
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
