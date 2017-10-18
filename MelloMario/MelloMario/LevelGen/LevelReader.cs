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

    class LevelReader
    {
        private const int SCALE = 32;
        private Point size;
        // Note: Without implementing IDisposable, it may cause resource leak
        //       The code analysis tool generates a warning here
        private StreamReader input;

        public LevelReader(String path)
        {
            Stream inStream = new FileStream(path, FileMode.Open);
            input = new StreamReader(inStream);

            String heightAsString = input.ReadLine();
            String widthAsString = input.ReadLine();

            size = new Point(Int32.Parse(widthAsString), Int32.Parse(heightAsString));
        }

        public Tuple<IGameWorld, IGameCharacter> LoadObjects()
        {
            IGameWorld world = GameObjectFactory.Instance.CreateGameWorld(size);
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
                            Tuple<IGameCharacter, IGameObject> pair = GameObjectFactory.Instance.CreateGameCharacter("Mario", world, new Point(j * SCALE, i * SCALE));
                            character = pair.Item1;
                            world.AddObject(pair.Item2);
                            break;

                        //blocks
                        case 'F':
                            world.AddObject(GameObjectFactory.Instance.CreateGameObject("Floor", world, new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'B':
                            world.AddObject(GameObjectFactory.Instance.CreateGameObject("Brick", world, new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'b':
                            world.AddObject(GameObjectFactory.Instance.CreateGameObject("HiddenBrick", world, new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'S':
                            world.AddObject(GameObjectFactory.Instance.CreateGameObject("Stair", world, new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'Q':
                            world.AddObject(GameObjectFactory.Instance.CreateGameObject("Question", world, new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'q':
                            world.AddObject(GameObjectFactory.Instance.CreateGameObject("HiddenQuestion", world, new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'P':
                            world.AddObject(GameObjectFactory.Instance.CreateGameObject("PipelineLeftIn", world, new Point(j * SCALE, i * SCALE)));
                            world.AddObject(GameObjectFactory.Instance.CreateGameObject("PipelineRightIn", world, new Point((j + 1) * SCALE, i * SCALE)));
                            break;
                        case 'V':
                            world.AddObject(GameObjectFactory.Instance.CreateGameObject("PipelineLeft", world, new Point(j * SCALE, i * SCALE)));
                            world.AddObject(GameObjectFactory.Instance.CreateGameObject("PipelineRight", world, new Point((j + 1) * SCALE, i * SCALE)));
                            break;

                        //harm
                        case 'G':
                            world.AddObject(GameObjectFactory.Instance.CreateGameObject("Goomba", world, new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'K':
                            world.AddObject(GameObjectFactory.Instance.CreateGameObject("GreenKoopa", world, new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'D':
                            world.AddObject(GameObjectFactory.Instance.CreateGameObject("RedKoopa", world, new Point(j * SCALE, i * SCALE)));
                            break;

                        //entities
                        case 'C':
                            world.AddObject(GameObjectFactory.Instance.CreateGameObject("Coin", world, new Point(j * SCALE, i * SCALE)));
                            break;
                        case '1':
                            world.AddObject(GameObjectFactory.Instance.CreateGameObject("OneUpMushroom", world, new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'R':
                            world.AddObject(GameObjectFactory.Instance.CreateGameObject("FireFlower", world, new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'M':
                            world.AddObject(GameObjectFactory.Instance.CreateGameObject("SuperMushroom", world, new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'T':
                            world.AddObject(GameObjectFactory.Instance.CreateGameObject("Star", world, new Point(j * SCALE, i * SCALE)));
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
